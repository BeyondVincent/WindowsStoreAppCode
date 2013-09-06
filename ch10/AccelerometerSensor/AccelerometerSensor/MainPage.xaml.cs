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

namespace AccelerometerSensor
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

        Accelerometer accelerometer;
        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            accelerometer = Accelerometer.GetDefault();
            if (accelerometer != null)
            {
                accelerometer.ReadingChanged += accelerometer_ReadingChanged;
            }
            else
            {
                MessageDialog dialog = new MessageDialog("没有找到加速度计！");
                await dialog.ShowAsync();
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            accelerometer.ReadingChanged -= accelerometer_ReadingChanged;
        }


        async void accelerometer_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            XValue.Text = args.Reading.AccelerationX.ToString();
                            YValue.Text = args.Reading.AccelerationY.ToString();
                            ZValue.Text = args.Reading.AccelerationZ.ToString();
                        });
        }

    }
}
