using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace AppSetting
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
            // 获取设置实例
            ApplicationDataContainer settingsLocal = ApplicationData.Current.LocalSettings;
            ApplicationDataContainer settingsRoaming = ApplicationData.Current.RoamingSettings;


            // 将数据保存到本地设置中
            settingsLocal.Values["user"] = "BeyondVincent";
            settingsLocal.Values["password"] = "111111";
            // 将数据保存到漫游设置中
            settingsRoaming.Values["user"] = "BeyondVincent";
            settingsRoaming.Values["password"] = "111111";

            // 读取本地设置中的数据
            string user = (string)settingsLocal.Values["user"];
            string password = (string)settingsRoaming.Values["password"];
            // 读取漫游设置中的数据
            user = (string)settingsRoaming.Values["user"];
            password = (string)settingsRoaming.Values["password"];

            // 删除本地数据中的数据
            settingsLocal.Values.Remove("user");
            settingsLocal.Values.Remove("password");
            // 删除漫游数据中的数据
            settingsRoaming.Values.Remove("user");
            settingsRoaming.Values.Remove("password");

            // 在本地设置中创建分类
            settingsLocal.CreateContainer("user", ApplicationDataCreateDisposition.Always);
            settingsLocal.Containers["user"].Values["user"] = "BeyondVincent";
            settingsLocal.Containers["user"].Values["password"] = "111111";

        }
    }
}
