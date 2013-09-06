using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace CustomizeButton
{
    class TileButton : Button
    {
        // 翻转效果
        PlaneProjection projection;

        // 移动、缩放、旋转等转换
        CompositeTransform transform;

        // 按下时，倾斜的角度
        int angle = 10;

        // 拖动多远时，开始移动空间
        double dragoffx = 5;
        double dragoffy = 5;

        // 构造函数 做一些初始化工作
        public TileButton()
        {
            projection = new PlaneProjection();
            Projection = projection;
            transform = new CompositeTransform();
            RenderTransform = transform;

            ManipulationMode = ManipulationModes.Rotate | ManipulationModes.Scale | ManipulationModes.TranslateX | ManipulationModes.TranslateY;
        }

        /*
         * 按下时的事件处理，两个步骤
         * 1、首先判断当前按下去的位置在控件的那个区域（控件共分为9个区域，详见PressPointLocation定义）
         * 2、根据按下去的位置，对控件做不同的效果
         */
        protected override void OnPointerPressed(Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            // 获得当前区域位置
            PressPointLocation location = GetPointLocation(e.GetCurrentPoint(this).Position);


            // 开始对控件做效果处理

            
            if (location == (PressPointLocation.Left | PressPointLocation.YCenter))
            {// 左中
                projection.RotationY = angle;
                projection.CenterOfRotationX = 1;
            }
            else if (location == (PressPointLocation.Left | PressPointLocation.Top))
            {// 左上
                projection.RotationX = -angle;
                projection.RotationY = angle;
                projection.CenterOfRotationX = 1;
                projection.CenterOfRotationY = 1;

            }
            else if (location == (PressPointLocation.Top | PressPointLocation.XCenter))
            {// 上中
                projection.RotationX = -angle * 2;
                projection.CenterOfRotationY = 1;
            }
            else if (location == (PressPointLocation.Right | PressPointLocation.Top))
            {// 右上
                projection.RotationY = projection.RotationX = -angle;
                projection.CenterOfRotationX = 0;
                projection.CenterOfRotationY = 1;
            }
            else if (location == (PressPointLocation.Right | PressPointLocation.YCenter))
            {// 右中
                projection.RotationY = -angle;
                projection.CenterOfRotationX = 0;
            }
            else if (location == (PressPointLocation.Right | PressPointLocation.Bottom))
            {// 右下
                projection.RotationX = angle;
                projection.RotationY = -angle;

                projection.CenterOfRotationX = 0;
                projection.CenterOfRotationY = 0;
            }
            else if (location == (PressPointLocation.Bottom | PressPointLocation.XCenter))
            {// 下中
                projection.RotationX = angle * 2;

                projection.CenterOfRotationY = 0;
            }
            else if (location == (PressPointLocation.Left | PressPointLocation.Bottom))
            {// 左下
                projection.RotationY = projection.RotationX = angle;

                projection.CenterOfRotationX = 1;
                projection.CenterOfRotationY = 1;
            }
            else if (location == (PressPointLocation.XCenter | PressPointLocation.YCenter))
            {// 正中
                CompositeTransform transform = RenderTransform as CompositeTransform;

                transform.CenterX = ActualWidth/2;
                transform.CenterY = ActualHeight/2;
                transform.ScaleX = 0.9;
                transform.ScaleY = 0.9;
            }
        }

        /*
         * 按下放开时的事件处理
         * 主要是做一些还原处理（旋转角度归零、缩放还原、透明度设置等）
         */
        protected override void OnPointerReleased(Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            projection.RotationX = 0;
            projection.RotationY = 0;

            CompositeTransform transform = RenderTransform as CompositeTransform;

            transform.CenterX = ActualWidth / 2;
            transform.CenterY = ActualHeight / 2;
            transform.ScaleX = 1;
            transform.ScaleY = 1;

            Opacity = 1;

            dragoffx = 0;
            dragoffy = 0;
        }

        /*
         * 有移动等操作时的事件处理
         * 主要包括移动、放大、透明等
         */
        protected override void OnManipulationDelta(ManipulationDeltaRoutedEventArgs e)
        {
            dragoffx += e.Delta.Translation.X;
            dragoffy += e.Delta.Translation.Y;

            if (-10 <= dragoffx && dragoffx <= 10 && -10 <= dragoffy && dragoffy <= 10)
                return;

            transform.CenterX = ActualWidth / 2;
            transform.CenterY = ActualHeight / 2;
            transform.ScaleX = 1.1;
            transform.ScaleY = 1.1;

            Opacity = 0.5;

            projection.RotationX = 0;
            projection.RotationY = 0;

            transform.TranslateX += e.Delta.Translation.X;
            transform.TranslateY += e.Delta.Translation.Y;

            transform.ScaleX *= e.Delta.Scale;
            transform.ScaleY *= e.Delta.Scale;

            transform.Rotation += e.Delta.Rotation;

            base.OnManipulationDelta(e);
        }

        /*
         * 获取控件所在的区域算法
         */
        PressPointLocation GetPointLocation(Point point)
        {
            PressPointLocation location = PressPointLocation.None;

            double tempwidth = ActualWidth / 3;
            if (point.X < tempwidth)
            {
                location |= PressPointLocation.Left;
            }
            else if (point.X > tempwidth * 2)
            {
                location |= PressPointLocation.Right;
            }
            else
            {
                location |= PressPointLocation.XCenter;
            }

            double tempheight = ActualHeight / 3;
            if (point.Y < tempheight)
            {
                location |= PressPointLocation.Top;
            }
            else if (point.Y > tempheight * 2)
            {
                location |= PressPointLocation.Bottom;
            }
            else
            {
                location |= PressPointLocation.YCenter;
            }
            Debug.WriteLine(location.ToString());
            return location;
        }
    }

    public enum PressPointLocation
    {
        None = 0,
        Left = 1,
        Top = 2,
        Right = 4,
        Bottom = 8,
        XCenter = 16,
        YCenter = 32
    }
}
