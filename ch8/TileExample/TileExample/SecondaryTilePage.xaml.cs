using NotificationsExtensions.TileContent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.StartScreen;
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
    public sealed partial class SecondaryTilePage : Page
    {
        public SecondaryTilePage()
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

        async private void pinToStartScreen(object sender, RoutedEventArgs e)
        {
          SecondaryTile  secondaryTile = new SecondaryTile(
                                            "tileId", 
                                            "SecondTile",
                                            "Secondary Tile",
                                            "target=SecondaryTilePage", 
                                            TileOptions.ShowNameOnWideLogo,
                                            new Uri("ms-appx:///Assets/secondaryTile.png"),
                                            new Uri("ms-appx:///Assets/secondaryTileWide.png"));
                        await secondaryTile.RequestCreateAsync();
        }

        private void updateSecondaryTile(object sender, RoutedEventArgs e)
        {
            if (SecondaryTile.Exists("tileId"))
            {
                ITileSquareImage squareContent = TileContentFactory.CreateTileSquareImage();

                squareContent.Image.Src = "http://img1.gtimg.com/4/460/46005/4600509_980x1200_292.jpg";
                squareContent.Image.Alt = "Web image";

                ITileWideImageAndText01 tileContent = TileContentFactory.CreateTileWideImageAndText01();

                tileContent.TextCaptionWrap.Text = "高清：撑杆跳伊辛巴耶娃4米70无缘奥运三连冠";

                tileContent.Image.Src = "http://img1.gtimg.com/4/460/46005/4600509_980x1200_292.jpg";
                tileContent.Image.Alt = "Web image";

                tileContent.SquareContent = squareContent;
                
                TileUpdateManager.CreateTileUpdaterForSecondaryTile("tileId").Update(tileContent.CreateNotification());
            }
        }
    }
}
