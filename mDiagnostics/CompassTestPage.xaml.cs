using mDiagnostics.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace mDiagnostics
{
    public sealed partial class CompassTestPage : Page
    {
        private Compass _compass;
        private bool complitedtest = false;

        private async void ReadingChanged(object sender, CompassReadingChangedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                CompassReading reading = e.Reading;

                if (reading.HeadingMagneticNorth != null &
                    reading.HeadingTrueNorth != null &
                    reading.HeadingAccuracy != null)
                {
                    showmsg(1);
                }
            });
        }

        private void showmsg(int numb)
        {
            switch (numb)
            {
                case 1:
                    CTPstrbtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    CTPmsg.Text = "COMPLETE!";
                    CTPmsg.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    complitedtest = true;
                    break;
                case 2:
                    CTPstrbtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    CTPmsg.Text = "FAILED!";
                    CTPmsg.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    complitedtest = false;
                    break;
            }
        }

        public CompassTestPage()
        {
            this.InitializeComponent();

            HardwareButtons.BackPressed += delegate
            {
                this.Frame.Navigate(typeof(AccelerometerTestPage));
            };
        }

        private void CTPstrbtn_Click(object sender, RoutedEventArgs e)
        {
            _compass = Compass.GetDefault();

            if (_compass != null)
            {
                uint minReportInterval = _compass.MinimumReportInterval;
                uint reportInterval = minReportInterval > 16 ? minReportInterval : 16;
                _compass.ReportInterval = reportInterval;
                _compass.ReadingChanged += new TypedEventHandler<Compass, CompassReadingChangedEventArgs>(ReadingChanged);
                complitedtest = true;
            }
        }

        private void CTPtorepeat_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(CompassTestPage));
            }
        }

        private void CTPtonext_Click(object sender, RoutedEventArgs e)
        {
            SaveStateTest s = new SaveStateTest();
            s.savestate(complitedtest, "CompassTestState");

            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(ConnectionTestPage));
            }
        }

        private void CTPback_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(AccelerometerTestPage));
            }
        }

    }
}
