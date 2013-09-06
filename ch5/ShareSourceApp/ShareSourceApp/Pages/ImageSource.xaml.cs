using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
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
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace ShareSourceApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ImageSource : Page
    {
        public ImageSource()
        {
            this.InitializeComponent();
        }

        DataTransferManager dtm;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            dtm = DataTransferManager.GetForCurrentView();
            dtm.DataRequested += dtm_DataRequested;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            dtm.DataRequested -= dtm_DataRequested;
        }

        async void dtm_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            string FileTitle = "Windows商店应用开发技能";
            string FileDescription = "介绍一下共享功能";  //This is an optional value.

            DataPackage data = args.Request.Data;
            data.Properties.Title = FileTitle;
            data.Properties.Description = FileDescription;

            DataRequestDeferral waiter = args.Request.GetDeferral();

            try
            {
                StorageFile image = await Package.Current.InstalledLocation.GetFileAsync("Assets\\1.jpg");
                data.Properties.Thumbnail = RandomAccessStreamReference.CreateFromFile(image);
                data.SetBitmap(RandomAccessStreamReference.CreateFromFile(image));

                List<IStorageItem> files = new List<IStorageItem>();
                files.Add(image);
                data.SetStorageItems(files);
            }
            finally
            {
                waiter.Complete();
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
