using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace ShareTargetApp
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

        ShareOperation share;
        string title;
        string description;
        string text;
        string customData;
        string customDataFormat = "http://schema.org/Person";
        string formattedText;
        Uri uri;
        IReadOnlyList<IStorageItem> storageItems;
        IRandomAccessStreamReference bitmapImage;
        IRandomAccessStreamReference thumbImage;


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            share = e.Parameter as ShareOperation;

            await Task.Factory.StartNew(async () =>
            {
                title = share.Data.Properties.Title;
                description = share.Data.Properties.Description;
                thumbImage = share.Data.Properties.Thumbnail;

                //如果共享的数据是带格式的文本
                if (share.Data.Contains(StandardDataFormats.Html))
                {
                    formattedText = await share.Data.GetHtmlFormatAsync();
                }
                //如果共享的是一个URI
                if (share.Data.Contains(StandardDataFormats.Uri))
                {
                    uri = await share.Data.GetUriAsync();
                }
                //如果共享的是纯文本
                if (share.Data.Contains(StandardDataFormats.Text))
                {
                    text = await share.Data.GetTextAsync();
                }
                //如果共享的是文件
                if (share.Data.Contains(StandardDataFormats.StorageItems))
                {
                    storageItems = await share.Data.GetStorageItemsAsync();
                }
                //如果共享的是自定义数据
                if (share.Data.Contains(customDataFormat))
                {
                    customData = await share.Data.GetTextAsync(customDataFormat);
                }
                //如果贡献的是图片
                if (share.Data.Contains(StandardDataFormats.Bitmap))
                {
                    bitmapImage = await share.Data.GetBitmapAsync();
                }

//回到主线程中显示出共享的数据
await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
{
    TitleBox.Text = title;
    DescriptionBox.Text = description;

    if (text != null)
        UnformattedTextBox.Text = text;
    if (uri != null)
    {
        UriButton.Content = uri.ToString();
        UriButton.NavigateUri = uri;
    }

    if (formattedText != null)
        HTMLTextBox.NavigateToString(HtmlFormatHelper.GetStaticFragment(formattedText));

    if (bitmapImage != null)
    {
        IRandomAccessStreamWithContentType bitmapStream = await this.bitmapImage.OpenReadAsync();
        BitmapImage bi = new BitmapImage();
        bi.SetSource(bitmapStream);
        WholeImage.Source = bi;

        bitmapStream = await this.thumbImage.OpenReadAsync();
        bi = new BitmapImage();
        bi.SetSource(bitmapStream);
        ThumbImage.Source = bi;
    }

    if (customData != null)
    {
        StringBuilder receivedStrings = new StringBuilder();
        JsonObject customObject = JsonObject.Parse(customData);
        if (customObject.ContainsKey("type"))
        {
            if (customObject["type"].GetString() == "http://schema.org/Person")
            {
                receivedStrings.AppendLine("Type: " + customObject["type"].Stringify());
                JsonObject properties = customObject["properties"].GetObject();
                receivedStrings.AppendLine("Image: " + properties["image"].Stringify());
                receivedStrings.AppendLine("Name: " + properties["name"].Stringify());
                receivedStrings.AppendLine("Affiliation: " + properties["affiliation"].Stringify());
                receivedStrings.AppendLine("Birth Date: " + properties["birthDate"].Stringify());
                receivedStrings.AppendLine("Job Title: " + properties["jobTitle"].Stringify());
                receivedStrings.AppendLine("Nationality: " + properties["Nationality"].Stringify());
                receivedStrings.AppendLine("Gender: " + properties["gender"].Stringify());
            }
            CustomDataBox.Text = receivedStrings.ToString();
        }
    }
});
            });
        }
    }
}
