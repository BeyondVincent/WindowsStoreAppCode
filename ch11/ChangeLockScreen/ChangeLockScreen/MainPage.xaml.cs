using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.System.UserProfile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace ChangeLockScreen
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

        private void getUri_LockScreen_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = LockScreen.OriginalImageFile;
            if (uri != null)
            {
                screenImageUri.Text = uri.ToString();
            }
        }

        private void getStream_LockScreen_Click(object sender, RoutedEventArgs e)
        {
            IRandomAccessStream randomAccessStream = LockScreen.GetImageStream();
            BitmapImage image = new BitmapImage();
            image.SetSource(randomAccessStream);
            imageview.Source = image;
        }

        private async void setStream_LockScreen_Click(object sender, RoutedEventArgs e)
        {
            // 从网络中下载图片
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://beyondvincent.com/wp-content/uploads/2013/06/change_lock_screen.png");
            HttpRequestMessage request = new HttpRequestMessage();
            HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            InMemoryRandomAccessStream randomAccessStream = new InMemoryRandomAccessStream();
            DataWriter writer = new DataWriter(randomAccessStream.GetOutputStreamAt(0));
            writer.WriteBytes(await response.Content.ReadAsByteArrayAsync());
            await writer.StoreAsync();

            // 将下载的图片显示到界面中
            BitmapImage image = new BitmapImage();
            image.SetSource(randomAccessStream);
            imageview.Source = image;

            // 将下载的图片设置为锁屏背景图片
            await LockScreen.SetImageStreamAsync(randomAccessStream);
        }

        private async void setFile_LockScreen_Click(object sender, RoutedEventArgs e)
        {
            var imagePicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
                FileTypeFilter = { ".jpg", ".jpeg", ".png", ".bmp" },
            };

            var MyImage = await imagePicker.PickSingleFileAsync();
            if (MyImage != null)
            {
                await LockScreen.SetImageFileAsync(MyImage);
            }
        }
    }
}
