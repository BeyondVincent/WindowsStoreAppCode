using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace SocketClient
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。Parameter
        /// 属性通常用于配置页。</param>
        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // 创建一个新的socket实例，并绑定到一个本地端口上
            DatagramSocket udpSocket = new DatagramSocket();
            await udpSocket.BindServiceNameAsync("3721");

            // 打开一个连接到远程主机上
            HostName remoteHost = new HostName("192.168.1.100");
            await udpSocket.ConnectAsync(remoteHost, "3721");

            // 将一个字符串以UDP数据包形式发送到远程主机上
            DataWriter udpWriter = new DataWriter(udpSocket.OutputStream);
            udpWriter.WriteString("这里是破船之家");
            await udpWriter.StoreAsync();
        }
    }
}
