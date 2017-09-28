using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

namespace mDiagnostics
{
    public sealed partial class ServiceCentersPage : Page
    {

        Geolocator geo = null;

        public ServiceCentersPage()
        {
            this.InitializeComponent();

            HardwareButtons.BackPressed += delegate
            {
                this.Frame.Navigate(typeof(MainPage));
            };
        }

        protected async Task<string> GetJSON(Geoposition pos)
        {
            var client = new HttpClient();
            var result = await client.GetStringAsync(new Uri("https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" 
                + "54.990046, 82.901880"//Convert.ToDouble(pos.Coordinate.Latitude).ToString("000.000000") + "," 
                //+ Convert.ToDouble(pos.Coordinate.Longitude).ToString("000.000000") 
                + "&radius=1000&key=AIzaSyDIwqufckpA4eZL4Ac-jsrFbuOIXRl5kww&keyword=ремонт телефонов"));
            return result;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (geo == null)
                geo = new Geolocator();

            Geoposition pos = await geo.GetGeopositionAsync();

            sss.Center = new Geopoint(new BasicGeoposition()
                {
                    Latitude = 54.989995,
                    Longitude = 82.901572
            });

            JObject obj = JObject.Parse(await GetJSON(pos));
            JArray jarr = (JArray)obj["results"];
            for (int i = 0; i < jarr.Count; i++ )
            {
                MapIcon icon = new MapIcon();
                JObject geom = (JObject)jarr.ElementAt(i)["geometry"];
                JObject loc = (JObject)geom["location"];            
                icon.Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = Convert.ToDouble(Convert.ToDouble(loc["lat"]).ToString("000.000000")),
                    Longitude = Convert.ToDouble(Convert.ToDouble(loc["lng"]).ToString("000.000000"))
                });
                icon.NormalizedAnchorPoint = new Point(0.5, 1.0);
                icon.Title = (string)jarr[i]["name"];
                icon.Visible = true;
                //icon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/othericons/mapiconsmall.png"));
                sss.MapElements.Add(icon);
            }

                sss.ZoomLevel = 12;
            sss.LandmarksVisible = true;
        }
    }
}
