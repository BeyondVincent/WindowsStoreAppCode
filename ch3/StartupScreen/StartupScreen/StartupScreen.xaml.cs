using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace StartupScreen
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class StartupScreen : Page
    {
        public SplashScreen splashScreen;
        public Rect splashImage;

        public StartupScreen(SplashScreen splashscreen, bool loadState)
        {
            this.InitializeComponent();

            splashScreen = splashscreen;
            splashImage = splashScreen.ImageLocation;

            splashScreen.Dismissed += new TypedEventHandler<SplashScreen, Object>(splashScreen_Dismissed);

            PositionAdvertisement();
        }

        /// <summary>
        /// 定位UI界面中的两个控件
        /// </summary>
        private void PositionAdvertisement()
        {
            SplashScreenImage.SetValue(Canvas.TopProperty, splashImage.Y);
            SplashScreenImage.SetValue(Canvas.LeftProperty, splashImage.X);
            SplashScreenImage.Height = splashImage.Height;
            SplashScreenImage.Width = splashImage.Width;
            SplashScreenImage.Visibility = Visibility.Visible;

            progressRing.SetValue(Canvas.TopProperty, splashImage.Y+100);
            progressRing.SetValue(Canvas.LeftProperty, splashImage.X);

        } 

        void splashScreen_Dismissed(SplashScreen sender, object args)
        {
            DelayTimer();
        }
        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。Parameter
        /// 属性通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DelayTimer();
        }

        /// <summary>
        /// 延迟定时器
        /// </summary>
        private void DelayTimer()
        {
            //3秒钟之后切换到MainPage页面
            ThreadPoolTimer tptimer = ThreadPoolTimer.CreateTimer(async (timer) =>
            {
                await Dispatcher.RunAsync(
                    CoreDispatcherPriority.High, () =>
                    {
                        Frame frame = new Frame();
                        frame.Navigate(typeof(MainPage));
                        Window.Current.Content = frame;
                    });
            }, TimeSpan.FromMilliseconds(3000));
        }
    }
}
