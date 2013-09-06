using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace GridView
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
    List<Student> students = new List<Student>();
    students.Add(new Student { Name = "张三", TeamName = "体育", Height = 160, Weight = 55, Number = 01 });
    students.Add(new Student { Name = "小明", TeamName = "英语", Height = 172, Weight = 70, Number = 02 });
    students.Add(new Student { Name = "李四", TeamName = "娱乐", Height = 166, Weight = 60, Number = 03 });
    students.Add(new Student { Name = "小二", TeamName = "数学", Height = 180, Weight = 66, Number = 04 });
    students.Add(new Student { Name = "老五", TeamName = "国语", Height = 159, Weight = 50, Number = 05 });

    mainGridView.ItemsSource = students;
}
    }
}

