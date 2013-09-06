using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WebServices.WeatherWebService;
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

namespace WebServices
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

private async void GoButton_Click(object sender, RoutedEventArgs e)
{
    ring.IsActive = true;
    WeatherWebServiceSoapClient proxy = new WeatherWebServiceSoapClient();
    //string[] result = await proxy.getSupportCityAsync(inputZipCode.Text);

    if (inputZipCode.Text.Length == 0)
    {
        inputZipCode.Text = "大理";
    }
    string[] result = await proxy.getWeatherbyCityNameAsync(inputZipCode.Text);
    if (result.Length > 0)
    {
        ring.IsActive = false;

        StringBuilder resultString = new StringBuilder(100);
        foreach (string temp in result)
        {
            resultString.AppendFormat("{0}\n", temp);
        }
        resultDetails.Text = resultString.ToString();
    }
}
    }
}
