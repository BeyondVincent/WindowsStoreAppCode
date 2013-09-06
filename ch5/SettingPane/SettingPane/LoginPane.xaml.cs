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

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234236 上提供

namespace SettingPane
{
    public sealed partial class LoginPane : UserControl
    {
        public LoginPane()
        {
            this.InitializeComponent();
        }

private void MySettingsBackClicked(object sender, RoutedEventArgs e)
{
    if (this.Parent.GetType() == typeof(Popup))
    {
        ((Popup)this.Parent).IsOpen = false;
    }
    SettingsPane.Show();
}
    }
}
