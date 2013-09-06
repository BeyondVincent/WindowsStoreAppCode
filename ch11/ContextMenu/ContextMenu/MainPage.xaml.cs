using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace ContextMenu
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

       async  private void ShowImagePopupMenu(object sender, RightTappedRoutedEventArgs e)
        {
            PopupMenu menu = new PopupMenu();
            menu.Commands.Add(new UICommand("分享到", async (command) =>
            {
                MessageDialog md = new MessageDialog(command.Label);
                await md.ShowAsync();
            }));
            menu.Commands.Add(new UICommand("另存为", async (command) =>
            {
                MessageDialog md = new MessageDialog(command.Label);
                await md.ShowAsync();
            }));
            menu.Commands.Add(new UICommand("编辑", async (command) =>
            {
                MessageDialog md = new MessageDialog(command.Label);
                await md.ShowAsync();
            }));
            menu.Commands.Add(new UICommandSeparator());
            menu.Commands.Add(new UICommand("打印", async (command) =>
            {
                MessageDialog md = new MessageDialog(command.Label);
                await md.ShowAsync();
            }));
            menu.Commands.Add(new UICommand("全屏", async (command) =>
            {
                MessageDialog md = new MessageDialog(command.Label);
                await md.ShowAsync();
            }));
            var chosenCommand = await menu.ShowForSelectionAsync(GetElementRect((FrameworkElement)sender));
        }

public static Rect GetElementRect(FrameworkElement element)
{
    GeneralTransform buttonTransform = element.TransformToVisual(null);
    Point point = buttonTransform.TransformPoint(new Point());
    return new Rect(point, new Size(element.ActualWidth, element.ActualHeight));
}
    }
}
