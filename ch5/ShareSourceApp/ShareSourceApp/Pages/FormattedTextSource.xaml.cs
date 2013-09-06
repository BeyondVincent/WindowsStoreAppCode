using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class FormattedTextSource : Page
    {
        public FormattedTextSource()
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

        void dtm_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            string HTMLSource = "<strong>这是粗体</strong>这不是粗体  <a href='http://beyondvincent.com'>访问破船</a>";
            string HTMLTitle = "Windows商店应用开发技能";
            string HTMLDescription = "介绍一下共享功能";  //This is an optional value.

            DataPackage data = args.Request.Data;
            data.Properties.Title = HTMLTitle;
            data.Properties.Description = HTMLDescription;
            data.SetHtmlFormat(HtmlFormatHelper.CreateHtmlFormat(HTMLSource));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
