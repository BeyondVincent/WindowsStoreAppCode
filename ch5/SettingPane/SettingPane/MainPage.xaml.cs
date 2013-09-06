using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace SettingPane
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            // 注册CommandsRequested事件
            SettingsPane.GetForCurrentView().CommandsRequested += BlankPage_CommandsRequested;
        }

        Popup _settingsPopup;
void BlankPage_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
{
    // 新建一个命令
    SettingsCommand cmd = new SettingsCommand("login", "登录", (x) =>
    {
        // 新建一个Popup，并将其宽度设置为346，高度与屏幕一致
        _settingsPopup = new Popup();
        _settingsPopup.Width = 346;
        _settingsPopup.Height = Window.Current.Bounds.Height;
        _settingsPopup.IsLightDismissEnabled = true;

        // 新建一个页面,并设置该页面的相关属性（大小，位置）
        LoginPane mypane = new LoginPane();
        mypane.Height = Window.Current.Bounds.Height;
        mypane.Width = 346;
        _settingsPopup.Child = mypane;
        _settingsPopup.SetValue(Canvas.LeftProperty, Window.Current.Bounds.Width - 346);
        _settingsPopup.IsOpen = true;
    });

    args.Request.ApplicationCommands.Add(cmd);

    SettingsCommand cmd1 = new SettingsCommand("logout", "注销", (x) =>
    {
    });

    args.Request.ApplicationCommands.Add(cmd1);
}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // 显示出设置面板
            SettingsPane.Show();
        }
    }
}
