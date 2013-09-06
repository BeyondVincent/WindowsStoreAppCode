using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace FileSaveFromPicker
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
            FileSaveFromPicker();
        }

        async public void FileSaveFromPicker()
        {
            FileSavePicker saver = new FileSavePicker();
            saver.SuggestedStartLocation = PickerLocationId.Desktop;
            saver.FileTypeChoices.Add("文本文件", new List<string>() { ".txt" });
            saver.FileTypeChoices.Add("微软Excel", new List<string>() { ".xls" });
            saver.FileTypeChoices.Add("图片", new List<string>() { ".png", ".jpg", ".bmp" });
            saver.SuggestedFileName = "测试文件";
            StorageFile file = await saver.PickSaveFileAsync();

            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);
                await FileIO.WriteTextAsync(file, "测试数据");
                FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
            }
        }
    }
}
