using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

namespace HttpClientExample
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

        HttpClient httpClient;
        async private void getText(object sender, RoutedEventArgs e)
        {
            BTgetText.IsEnabled = false;
            BTcancleGet.IsEnabled = true;
            resultBox.Text = "请求中";
            ring.IsActive = true;
            try
            {
                httpClient = new HttpClient();
                string resourceAddress = getUrl.Text;
                HttpResponseMessage response = await httpClient.GetAsync(resourceAddress);
                resultBox.Text = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException)
            {
                resultBox.Text = "http请求异常";
            }
            catch (TaskCanceledException)
            {
                resultBox.Text = "请求被取消";
            }
            finally
            {
            }
            BTgetText.IsEnabled = true;
            BTcancleGet.IsEnabled = false;
            ring.IsActive = false;
        }

        async private void postText(object sender, RoutedEventArgs e)
        {
            BTpostText.IsEnabled = false;
            BTcanclePost.IsEnabled = true;
            resultBox.Text = "请求中";
            ring.IsActive = true;
            try
            {
                httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(baseAddress.Text);
                HttpResponseMessage response = await httpClient.PostAsync(requestUrl.Text, new StringContent(postBox.Text));
                resultBox.Text = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException)
            {
                resultBox.Text = "http请求异常";
            }
            catch (TaskCanceledException)
            {
                resultBox.Text = "请求被取消";
            }
            finally
            {
            }
            BTpostText.IsEnabled = true;
            BTcanclePost.IsEnabled = false;
            ring.IsActive = false;
        }

        private void cancle(object sender, RoutedEventArgs e)
        {
            httpClient.CancelPendingRequests();

            BTgetText.IsEnabled = true;
            BTpostText.IsEnabled = true;
        }

        async private void downloadImage(object sender, RoutedEventArgs e)
        {
            BTdownloadImage.IsEnabled = false;
            ring.IsActive = true;
            try
            {
                httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(baseAddress.Text);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "1.jpg");
                HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                await showImage(response);
                await saveImage(response);
            }
            catch (HttpRequestException)
            {
                resultBox.Text = "http请求异常";
            }
            catch (TaskCanceledException)
            {
                resultBox.Text = "请求被取消";
            }
            finally
            {
            }
            
            BTdownloadImage.IsEnabled = true;
            ring.IsActive = false;

        }

        // 显示图片
        async Task<int> showImage(HttpResponseMessage response)
        {
            InMemoryRandomAccessStream randomAccessStream = new InMemoryRandomAccessStream();
            DataWriter writer = new DataWriter(randomAccessStream.GetOutputStreamAt(0));
            writer.WriteBytes(await response.Content.ReadAsByteArrayAsync());
            await writer.StoreAsync();
            BitmapImage image = new BitmapImage();
            image.SetSource(randomAccessStream);
            imageview.Source = image;

            return 0;
        }

        // 保存图片
        async Task<int> saveImage(HttpResponseMessage response)
        {
            long tick = DateTime.Now.Ticks;

            string filename = tick.ToString();
            var imageFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename + ".png", CreationCollisionOption.ReplaceExisting);
            var fs = await imageFile.OpenAsync(FileAccessMode.ReadWrite);
            DataWriter writer = new DataWriter(fs.GetOutputStreamAt(0));
            writer.WriteBytes(await response.Content.ReadAsByteArrayAsync());
            await writer.StoreAsync();
            writer.DetachStream();
            await fs.FlushAsync();

            return 0;
        }
    }
}
