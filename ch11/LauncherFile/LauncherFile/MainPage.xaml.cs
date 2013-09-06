using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace LauncherFile
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
        }

        async private void DefaultLaunch(object sender, RoutedEventArgs e)
        {
            // 获取要打开的文件
            var file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync("Gof.pdf");

            // 打开文件
            var success = await Windows.System.Launcher.LaunchFileAsync(file);
        }

        async private void DisplayApplicationPicker(object sender, RoutedEventArgs e)
        {
            // 获取要打开的文件
            var file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync("Gof.pdf");

            // 设置显示程序列表
            var options = new Windows.System.LauncherOptions();
            options.DisplayApplicationPicker = true;

            // 打开文件 
            bool success = await Windows.System.Launcher.LaunchFileAsync(file, options);

        }

        async private void OpenURL(object sender, RoutedEventArgs e)
        {
            // 实例化一个Uri对象  
            var uri = new Uri("http://BeyondVincent.com");

            // 打开Uri
            var success = await Windows.System.Launcher.LaunchUriAsync(uri);
        }
    }
}
