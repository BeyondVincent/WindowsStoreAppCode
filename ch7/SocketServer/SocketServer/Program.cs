using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketServer
{
    /// <summary>
    /// Socket Demo 服务器端
    /// </summary>
    class Program
    {
        private static AutoResetEvent _flipFlop = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            //创建socket，使用的是TCP协议，如果用UDP协议，则要用SocketType.Dgram类型的Socket
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);

            //创建终结点EndPoint  取当前主机的ip
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            //把ip和端口转化为IPEndpoint实例,端口号取8888
            IPEndPoint localEP = new IPEndPoint(ipHostInfo.AddressList[1], 3721);

            Console.WriteLine("本地IP地址和端口分别是 : {0}", localEP);

            try
            {
                //绑定EndPoint对像（8888端口和ip地址）
                listener.Bind(localEP);
                //开始监听
                listener.Listen(10);
                //一直循环接收客户端的消息
                while (true)
                {
                    Console.WriteLine("正在等待Windows商店应用程序客户端的连接...");
                    //开始接收下一个连接
                    listener.BeginAccept(AcceptCallback, listener);
                    //开始线程等待
                    _flipFlop.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// 接收返回事件处理
        /// </summary>
        /// <param name="ar"></param>
        private static void AcceptCallback(IAsyncResult ar)
        {
            Socket listener = (Socket)ar.AsyncState;
            Socket socket = listener.EndAccept(ar);

            Console.WriteLine("已连接到Windows Phone客户端");

            _flipFlop.Set();

            //开始接收，传递StateObject对象过去
            var state = new StateObject();
            state.Socket = socket;
            socket.BeginReceive(state.Buffer,
                0,
                StateObject.BufferSize,
                0,
                ReceiveCallback,
                state);
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket socket = state.Socket;


            // 读取客户端socket发送过来的数据
            int read = socket.EndReceive(ar);

            // 如果成功读取了客户端socket发送过来的数据
            if (read > 0)
            {
                //获取客户端的消息，转化为字符串格式
                string chunk = Encoding.UTF8.GetString(state.Buffer, 0, read);
                state.StringBuilder.Append(chunk);

                if (state.StringBuilder.Length > 0)
                {
                    string result = state.StringBuilder.ToString();
                    Console.WriteLine("收到到客户端传过来的消息: {0}", result);
                    //发送数据信息给客户端
                    Send(socket, "你好");
                }
            }
        }


        /// <summary>
        /// 返回客户端数据
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="data"></param>
        private static void Send(Socket handler, String data)
        {

            //将消息内容转化为发送的byte[]格式
            byte[] byteData = Encoding.UTF8.GetBytes(data);

            //发送消息到Windows Phone客户端
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                // 完成发送消息到Windows Phone客户端
                int bytesSent = handler.EndSend(ar);
                if (bytesSent > 0)
                {
                    Console.WriteLine("信息发送成功");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }

    public class StateObject
    {
        public Socket Socket;
        public StringBuilder StringBuilder = new StringBuilder();
        public const int BufferSize = 100;
        public byte[] Buffer = new byte[BufferSize];
        public int TotalSize;
    }
}
