using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace CaptureImage
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

        async private void captureImage(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI camera = new CameraCaptureUI();
            camera.PhotoSettings.CroppedAspectRatio = new Size(16, 9);
            StorageFile photo = await camera.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (photo != null)
            {
                BitmapImage bmp = new BitmapImage();
                IRandomAccessStream stream = await photo.OpenAsync(FileAccessMode.Read);
                bmp.SetSource(stream);
                myImage.Source = bmp;
                myImage.Visibility = Visibility.Visible;
            }
        }

        async private void captureVideo(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI videocamera = new CameraCaptureUI();
            videocamera.VideoSettings.Format = CameraCaptureUIVideoFormat.Mp4;
            videocamera.VideoSettings.AllowTrimming = true;
            videocamera.VideoSettings.MaxDurationInSeconds = 30;
            videocamera.VideoSettings.MaxResolution = CameraCaptureUIMaxVideoResolution.HighestAvailable;
            StorageFile video = await videocamera.CaptureFileAsync(CameraCaptureUIMode.Video);
            if (video != null)
            {
                IRandomAccessStream stream = await video.OpenAsync(FileAccessMode.Read);
                myVideo.SetSource(stream, "video/mp4");
                myVideo.Visibility = Visibility.Visible;
            }
        }
    }
}
