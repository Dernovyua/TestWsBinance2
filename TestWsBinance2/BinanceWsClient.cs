
using Newtonsoft.Json;
using SourceEts.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Adapter.CryptoMarket.CryptoBinance
{
    public delegate void StockHandler();
    public delegate void TradesHandler(ITick data);
    public delegate void TickerHandler(ISecurity data);

    public class BinanceWsClient
    {
        private string _name = "Binance";
        private string _url = @"wss://stream.binance.com:9443/ws";
        private string _urlFut = @"wss://fstream.binance.com:9443/ws";
        private ClientWebSocket _socket;
        private CancellationTokenSource _cts;
        private static readonly Object _lock = new Object();
        BackgroundWorker woker = new BackgroundWorker();
        DispatcherTimer _dsTimer = new DispatcherTimer();

        public event StockHandler StockHandler;
        public event TradesHandler TradesHandler;
        public event TickerHandler TickerHandler;
        //private Dictionary<string, DepthData> _lastBook = new Dictionary<string, DepthData>();

        private Thread _acceptThread;
        public string TypeAccount;

        public BinanceWsClient()
        {
            try
            {
                _socket = new ClientWebSocket();
                _cts = new CancellationTokenSource();

                woker.DoWork += Work;

                _dsTimer.Interval = TimeSpan.FromMilliseconds(200);
                _dsTimer.Start();
                _dsTimer.Tick += (_dsTimer_Tick);

                _acceptThread = new Thread(OpenStream);
                _acceptThread.IsBackground = true;
                _acceptThread.Start();


                //OpenStream();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка " + ex.Message, "Ошибка");
            }

        }
        private void Work(object sender, DoWorkEventArgs e)
        {
            UpdateTicks();
        }

        public void _dsTimer_Tick(Object sender, EventArgs args)
        {
            if (!woker.IsBusy)
                woker.RunWorkerAsync();
        }

        public void OpenStream()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

            Task.Factory.StartNew(
            () =>
            {
                var rcvBytes = new byte[128];
                var rcvBuffer = new ArraySegment<byte>(rcvBytes);
                var fullMessage = "";

                while (true)
                {
                    try
                    {
                        if (_socket.State != WebSocketState.Open)
                        {

                            _socket = new ClientWebSocket();
                            Uri uri = new Uri(_url);

                            _socket.ConnectAsync(uri, _cts.Token).Wait();

                            if (_socket.State != WebSocketState.Open)
                            {
                                Thread.Sleep(1000);
                                continue;
                            }
                        }

                        Task<WebSocketReceiveResult> rcvTask = null;

                        while (true)
                        {
                            if (rcvTask != null && !rcvTask.IsCompleted)
                            {
                                rcvTask.Wait(500);
                            }
                            else
                            {
                                rcvTask = null;
                                rcvTask = _socket.ReceiveAsync(rcvBuffer, _cts.Token);
                                if (!rcvTask.IsCompleted)
                                    continue;
                            }

                            WebSocketReceiveResult rcvResult = rcvTask.Result;
                            byte[] msgBytes = rcvBuffer.Skip(rcvBuffer.Offset).Take(rcvResult.Count).ToArray();
                            string rcvMsg = Encoding.UTF8.GetString(msgBytes);
                            fullMessage += rcvMsg;
                            if (rcvResult.EndOfMessage)
                            {
                                //_derebit.BaseMon.Raise_OnSomething(_derebit.NameExchange + " " + "[WS IN] " + fullMessage);

                                //Debug.WriteLine("[WS IN] " + fullMessage);
                                var msg = fullMessage;
                                if (!String.IsNullOrEmpty(msg) && !update)
                                {
                                    Task.Factory.StartNew(() => ProcessMessage(msg));
                                }
                                fullMessage = "";
                                break;
                            }
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        //Debug.WriteLine(ex);
                        Thread.Sleep(1000);
                        continue;
                    }
                }
            }, _cts.Token, TaskCreationOptions.PreferFairness, TaskScheduler.Default);
        }

        private void ProcessMessage(string jsonString)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject<dynamic>(jsonString);

                if (obj.e != null)
                {
                    string msgType = obj.e.ToString();

                    switch (msgType)
                    {
                        case "aggTrade":
                            ProcessTradesMessage(obj);
                            break;
                        case "24hrTicker":
                            ProcessTickerMessage(obj);
                            break;
                        case "depthUpdate":
                            ProcessBookMessage(obj);
                            break;
                            //case "arr":
                            //    ProcessBookMessage(obj);
                            //    break;
                    }
                }
            }
            catch (JsonReaderException ex)
            {
                //Debug.WriteLine($"Ошибка обработки  ws сообщения: {ex}");
            }
            catch (Exception ex)
            {
                //Debug.WriteLine($"Ошибка обработки  ws сообщения: {ex}");
            }
        }

        private void ProcessTickerMessage(dynamic obj)
        {

        }

        private void ProcessTradesMessage(dynamic obj)
        {
            //return;


        }

        /// <summary>
        /// Обрабатываем тики в отдельном потоке
        /// </summary>
        private void UpdateTicks()
        {
            
        }

        #region Стакан
        bool update = false;
        private void ProcessBookMessage(dynamic obj)
        {
            
        }



        #endregion

        public void SubscribeStock(string symbol)
        {
            //need to get book snapshot at first
            //lock (_lastBook)
            //{
            //    _lastBook[symbol.ToUpper()] = _public.GetDepth(symbol);
            //    _lastBook[symbol.ToUpper()].Symbol = symbol;
            //}
            //then subscribe to diff depth stream
            SendCommand(MakeSubscribeCommand($"{symbol}@depth"));//@100ms
        }

        public void SubscribeTrades(string symbol)
        {
            SendCommand(MakeSubscribeCommand($"{symbol}@aggTrade"));
        }

        public void SubscribeTicker(string symbol)
        {
            SendCommand(MakeSubscribeCommand($"{symbol.ToLower()}@ticker"));
            //SendCommand($"{{\"method\": \"SUBSCRIBE\",\"params\":[{symbol}],\"id\": 1}}");
        }

        public void SubscribeAll(string subscribe)
        {
            //SendCommand(MakeSubscribeCommand($"{symbol.ToLower()}@ticker"));
            SendCommand($"{{\"method\": \"SUBSCRIBE\",\"params\":[{subscribe}],\"id\": 1}}");
        }

        //public void SubscribeAllTicker()
        //{
        //    SendCommand(MakeSubscribeCommand($"!ticker@arr"));
        //}

        string MakeSubscribeCommand(string channel)
        {
            return $"{{\"method\": \"SUBSCRIBE\",\"params\":[\"{channel}\"],\"id\": 1}}";
        }



        public void SendCommand(string message)
        {
            if (message == "{\"method\": \"SUBSCRIBE\",\"params\":[\"btcusdt@depth10@1000ms\"],\"id\": 1}")
            { }

            Task.Factory.StartNew(
                () =>
                {
                    while (_socket.State != WebSocketState.Open)
                    {
                        Thread.Sleep(100);
                    }

                    //_binance.BaseMon.Raise_OnSomething(_binance.NameExchange + " " + "[WS OUT] " + message);
                    //Debug.WriteLine("[WS OUT] " + message);
                    byte[] sendBytes = Encoding.UTF8.GetBytes(message);
                    var sendBuffer = new ArraySegment<byte>(sendBytes);
                    lock (_lock)
                    {
                        _socket.SendAsync(sendBuffer, WebSocketMessageType.Text, endOfMessage: true, cancellationToken: _cts.Token).Wait();
                    }
                });
        }
    }
}
