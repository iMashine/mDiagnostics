using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;
using mDiagnostics.Common;
using Windows.Phone.UI.Input;


namespace mDiagnostics
{
    public sealed partial class GeolocationTestPage : Page
    {
        Geolocator geo = null;
        public bool completedtest = false;

        public GeolocationTestPage()
        {
            this.InitializeComponent();

            HardwareButtons.BackPressed += delegate
            {
                this.Frame.Navigate(typeof(DisplayTestPage));
            };
        }

        private async void GTPstrbtn_Click(object sender, RoutedEventArgs e)
        {
            GTPstrbtn.IsEnabled = false;
            if (geo == null)
                geo = new Geolocator();

            Geoposition pos = await geo.GetGeopositionAsync();


            if (geo != null)
            {
                completedtest = true;
                showmsg(1);
                geo = null;
            }
            else
            {
                showmsg(0);
            }
        }

        private void showmsg(int numb)
        {
            switch (numb)
            {
                case 1:
                    GTPstrbtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    GTPmsg.Text = "COMPLETE!";
                    GTPmsg.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    completedtest = true;
                    break;
                case 2:
                    GTPstrbtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    GTPmsg.Text = "FAILED!";
                    GTPmsg.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    completedtest = false;
                    break;
            }
        }

        private void GTPtonext_Click(object sender, RoutedEventArgs e)
        {
            SaveStateTest s = new SaveStateTest();
            s.savestate(completedtest, "GeolacationTestState");

            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(AccelerometerTestPage));
            }
        }

        private void GTPback_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(DisplayTestPage));
            }
        }

        private void GTPtorepeat_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(GeolocationTestPage));
            }
        }
    }
}

