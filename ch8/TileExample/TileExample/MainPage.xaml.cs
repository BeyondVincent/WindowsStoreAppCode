using NotificationsExtensions.BadgeContent;
using NotificationsExtensions.TileContent;
using NotificationsExtensions.ToastContent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace TileExample
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

        private void SendTileText(object sender, RoutedEventArgs e)
        {
            ITileSquareText04 squareContent = TileContentFactory.CreateTileSquareText04();
            squareContent.TextBodyWrap.Text = "你有三条未读短信！";

            ITileWideText03 tileContent = TileContentFactory.CreateTileWideText03();
            tileContent.TextHeadingWrap.Text = "你有三条未读短信！";

            tileContent.SquareContent = squareContent;

            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileContent.CreateNotification());
        }

        private void SendTilePic(object sender, RoutedEventArgs e)
        {
            ITileSquareImage squareContent = TileContentFactory.CreateTileSquareImage();

            squareContent.Image.Src = "http://img1.gtimg.com/4/460/46005/4600509_980x1200_292.jpg";
            squareContent.Image.Alt = "Web image";

            ITileWideImageAndText01 tileContent = TileContentFactory.CreateTileWideImageAndText01();

            tileContent.TextCaptionWrap.Text = "高清：撑杆跳伊辛巴耶娃4米70无缘奥运三连冠";

            tileContent.Image.Src = "http://img1.gtimg.com/4/460/46005/4600509_980x1200_292.jpg";
            tileContent.Image.Alt = "Web image";

            tileContent.SquareContent = squareContent;

            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileContent.CreateNotification());
        }

        private void ClearTile(object sender, RoutedEventArgs e)
        {
            TileUpdateManager.CreateTileUpdaterForApplication().Clear();
        }

        private void goSecondaryTile(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SecondaryTilePage));
        }

        private void showToast(object sender, RoutedEventArgs e)
        {
            IToastImageAndText01 toastContent = ToastContentFactory.CreateToastImageAndText01();
            toastContent.Launch = "toast通知=>SecondaryTilePage";
            toastContent.TextBodyWrap.Text = "恭喜你，中500W！";
            toastContent.Image.Src = "/Assets/bv_logo.png";

            ToastNotificationManager.CreateToastNotifier().Show(toastContent.CreateNotification());
        }

        private void showBadgeWithNumber(object sender, RoutedEventArgs e)
        {
            BadgeNumericNotificationContent badgeContent = new BadgeNumericNotificationContent(11);
            BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(badgeContent.CreateNotification());  
        }

        private void showBadgeWithGlyph(object sender, RoutedEventArgs e)
        {
            BadgeGlyphNotificationContent badgeContent = new BadgeGlyphNotificationContent(GlyphValue.Playing);
            BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(badgeContent.CreateNotification()); 
        }
    }
}
