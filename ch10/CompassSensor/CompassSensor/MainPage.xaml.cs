using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace CompassSensor
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

        Compass c;
        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            c = Compass.GetDefault();
            if (c != null)
            {
                c.ReadingChanged += c_ReadingChanged;
            }
            else
            {
                MessageDialog dialog = new MessageDialog("没有找到罗盘！");
                await dialog.ShowAsync();
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            c.ReadingChanged -= c_ReadingChanged;
        }

        async void c_ReadingChanged(Compass sender, CompassReadingChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                MagneticNorth.Text = args.Reading.HeadingMagneticNorth.ToString();
                if (args.Reading.HeadingTrueNorth != null)
                {
                    TrueNorth.Text = args.Reading.HeadingTrueNorth.ToString();
                }
            });
        }
    }
}
