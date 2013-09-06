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

namespace SemanticZoomApp
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
            List<Student> elements = new List<Student>();
            elements.Add(new Student { Number = 001, Category = "1班", Name = "张三", Symbol = "男", State = "在校" });
            elements.Add(new Student { Number = 002, Category = "1班", Name = "小李", Symbol = "男", State = "在校" });
            elements.Add(new Student { Number = 003, Category = "1班", Name = "小王", Symbol = "女", State = "校外" });
            elements.Add(new Student { Number = 004, Category = "1班", Name = "小周", Symbol = "女", State = "校外" });
            elements.Add(new Student { Number = 006, Category = "1班", Name = "老赵", Symbol = "女", State = "在校" });
            elements.Add(new Student { Number = 007, Category = "2班", Name = "小三", Symbol = "男", State = "在校" });
            elements.Add(new Student { Number = 008, Category = "2班", Name = "老大", Symbol = "男", State = "在校" });
            elements.Add(new Student { Number = 009, Category = "2班", Name = "王二", Symbol = "男", State = "在校" });
            elements.Add(new Student { Number = 010, Category = "2班", Name = "陈总", Symbol = "女", State = "校外" });
            elements.Add(new Student { Number = 011, Category = "3班", Name = "小刘", Symbol = "女", State = "在校" });
            elements.Add(new Student { Number = 012, Category = "3班", Name = "小张", Symbol = "女", State = "在校" });
            elements.Add(new Student { Number = 013, Category = "3班", Name = "小朱", Symbol = "男", State = "校外" });
            elements.Add(new Student { Number = 014, Category = "3班", Name = "二哥", Symbol = "男", State = "在校" });
            elements.Add(new Student { Number = 015, Category = "3班", Name = "大刘", Symbol = "女", State = "在校" });
            elements.Add(new Student { Number = 016, Category = "3班", Name = "老五", Symbol = "女", State = "在校" });

            StudentData.Source = elements;
            CategoryData.Source = from el in elements group el by el.Category into grp orderby grp.Key select grp;

        }
    }
}
