using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking;
using Windows.Networking.Connectivity;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace NetworkDetect
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            NetworkInformation.NetworkStatusChanged += (object sener) =>
            {
                showStatus();
            };
        }

        async void showStatus()
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (!IsConnectedToInternet())
                {
                    // 网络不可以访问
                    textblockstate.Text = "网络不可用";
                }
                else
                {
                    // 网络可以访问
                    textblockstate.Text = "网络可用";
                }
            });
        }

        private bool IsConnectedToInternet()
        {
            bool connected = false;

            ConnectionProfile cp = NetworkInformation.GetInternetConnectionProfile();

            if (cp != null)
            {
                NetworkConnectivityLevel cl = cp.GetNetworkConnectivityLevel();

                connected = cl == NetworkConnectivityLevel.InternetAccess;
            }

            return connected;
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。Parameter
        /// 属性通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            showStatus();

            GetIPAddress();
        }

        private void GetIPAddress()
        {

            foreach (HostName hostName in NetworkInformation.GetHostNames())
            {
                if (hostName.IPInformation != null)
                {
                    Debug.WriteLine("IPType:");
                    if (hostName.Type == HostNameType.Ipv4)
                    {
                        Debug.WriteLine("Ipv4");
                    }
                    else if (hostName.Type == HostNameType.Ipv6)
                    {
                        Debug.WriteLine("Ipv6");
                    }
                    else if (hostName.Type == HostNameType.Bluetooth)
                    {
                        Debug.WriteLine("Bluetooth");
                    }
                    else if (hostName.Type == HostNameType.DomainName)
                    {
                        Debug.WriteLine("DomainName");
                    }
                    Debug.WriteLine(hostName.CanonicalName);
                }
            }
        }
    }
}
