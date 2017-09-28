using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace mDiagnostics
{
    public sealed partial class FinalResultsPage : Page
    {
        public FinalResultsPage()
        {
            this.InitializeComponent();

            HardwareButtons.BackPressed += delegate
            {
                this.Frame.Navigate(typeof(MainPage));
            };

            load_state(FRP__SpeakerTest1, FRP__SpeakerTest2, "SpeakerTestState"); // 1

            load_state(FRP__MicrophoneTest1, FRP__MicrophoneTest2, "MicrophoneTestState"); // 2

            load_state(FRP__DisplayTest1, FRP__DisplayTest2, "DisplayTestState"); // 3

            load_state(FRP__GeolocationTest1, FRP__GeolocationTest2, "GeolacationTestState"); // 4

            load_state(FRP__AccelerometerTest1, FRP__AccelerometerTest2, "AccelerometerTestState"); // 5

            load_state(FRP__CompassTest1, FRP__CompassTest2, "CompassTestState"); // 6

            load_state(FRP__ConnectionTest1, FRP__ConnectionTest2, "ConnectionTestState"); // 7
        }

        private void load_state(TextBlock tv, Image img, string nametest)
        {
            Windows.Storage.ApplicationDataContainer roamingSettings =
              Windows.Storage.ApplicationData.Current.RoamingSettings;

            String temp;
            if (roamingSettings.Values.ContainsKey(nametest))
            {
                temp = roamingSettings.Values[nametest].ToString();
                roamingSettings.Values[nametest] = null;
                if (temp == "тест пройден") load_icon(img, 0);
                else load_icon(img, 1);

                tv.Text += " " + temp;
            }
            else
            {
                temp = "тест не пройден";
                load_icon(img, 1);

                tv.Text += " " + temp;
            }
        }

        private void load_icon(Image temp, int code)
        {
            if (code == 0)
            {
                Image img = temp as Image;
                BitmapImage bitmapImage = new BitmapImage();
                img.Width = bitmapImage.DecodePixelWidth = 30;
                bitmapImage.UriSource = new Uri(img.BaseUri, "Assets/btns/complete.png");
                temp.Source = bitmapImage;
            }
            if (code == 1)
            {
                Image img = temp as Image;
                BitmapImage bitmapImage = new BitmapImage();
                img.Width = bitmapImage.DecodePixelWidth = 30;
                bitmapImage.UriSource = new Uri(img.BaseUri, "Assets/btns/failed.png");
                temp.Source = bitmapImage;
            }
            if (code == 2)
            {
                Image img = temp as Image;
                BitmapImage bitmapImage = new BitmapImage();
                img.Width = bitmapImage.DecodePixelWidth = 30;
                bitmapImage.UriSource = new Uri(img.BaseUri, "Assets/btns/nothing.png");
                temp.Source = bitmapImage;
            }
        }
    }
}
