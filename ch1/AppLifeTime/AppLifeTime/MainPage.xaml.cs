using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace AppLifeTime
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
protected override void OnNavigatedTo(NavigationEventArgs e)
{
    Application.Current.Suspending += Current_Suspending;
    Application.Current.Resuming += Current_Resuming;
}

void Current_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
{
    ApplicationData.Current.LocalSettings.Values["customTextValue"] = MyText.Text;
}

void Current_Resuming(object sender, object e)
{
    Message.Text = "已经恢复. Suspended于" + ApplicationData.Current.LocalSettings.Values["suspendedDateTime"];
    MyText.Text = ApplicationData.Current.LocalSettings.Values["customTextValue"].ToString();
}

protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
{
    Application.Current.Resuming -= Current_Resuming;
    Application.Current.Suspending += Current_Suspending;
}
    }
}
