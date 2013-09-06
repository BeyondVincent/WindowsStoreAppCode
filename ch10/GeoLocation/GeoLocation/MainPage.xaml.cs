using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace GeoLocation
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



        Geolocator geoLocation;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            geoLocation = new Geolocator();
            geoLocation.PositionChanged += geoLocation_PositionChanged;
            geoLocation.StatusChanged += geoLocation_StatusChanged;
            //GetLocationDataOnce();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            geoLocation.PositionChanged -= geoLocation_PositionChanged;
            geoLocation.StatusChanged -= geoLocation_StatusChanged;
        }

        async void geoLocation_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                StatusValue.Text = args.Status.ToString();
            });
        }

    async void geoLocation_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
    {
        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        {
            Geoposition position = args.Position;

            showData(position);

            geoCoding(position.Coordinate.Latitude, position.Coordinate.Longitude);
        }
        );
    }

        private void showData(Geoposition position)
        {
            LatitudeValue.Text = position.Coordinate.Latitude.ToString();
            LongitudeValue.Text = position.Coordinate.Longitude.ToString();
            AccuracyValue.Text = position.Coordinate.Accuracy.ToString();

            TimestampValue.Text = position.Coordinate.Timestamp.ToString();

            if (position.Coordinate.Altitude != null)
                AltitudeValue.Text = position.Coordinate.Altitude.ToString()
                                        + "(+- " + position.Coordinate.AltitudeAccuracy.ToString() + ")";
            if (position.Coordinate.Heading != null)
                HeadingValue.Text = position.Coordinate.Heading.ToString();
            if (position.Coordinate.Speed != null)
                SpeedValue.Text = position.Coordinate.Speed.ToString();
        }

        async void geoCoding(double latitude, double longitude)
        {
            GeoCodingValue.Text = "反向地理编码请求中";
            ring.IsActive = true;
            try
            {
                HttpClient httpClient = new HttpClient();
                string url = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&language=zh-CN&sensor=true", latitude.ToString(), longitude.ToString());
                HttpResponseMessage response = await httpClient.GetAsync(url);
                GeoCodingValue.Text = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException)
            {
                GeoCodingValue.Text = "http请求异常";
            }
            catch (TaskCanceledException)
            {
                GeoCodingValue.Text = "请求被取消";
            }
            finally
            {
            }
            ring.IsActive = false;
        }

        async private void GetLocationDataOnce()
        {
            Geolocator geoLocation = new Geolocator();
            Geoposition position = await geoLocation.GetGeopositionAsync();

            showData(position);

            geoCoding(position.Coordinate.Latitude, position.Coordinate.Longitude);
        } 

    }
}
