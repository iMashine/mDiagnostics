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
using Windows.UI.Core;
using Windows.Devices.Sensors;
using mDiagnostics.Common;
using Windows.Phone.UI.Input;

namespace mDiagnostics
{
    public sealed partial class AccelerometerTestPage : Page
    {
        private Accelerometer _accelerometer;
        private bool complitedtest = false;
        private bool 
            x1 = false, 
            x2 = false,
            y1 = false,
            y2 = false, 
            z1 = false,
            z2 = false;

        private async void ReadingChanged(object sender, AccelerometerReadingChangedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                AccelerometerReading reading = e.Reading;

                if (reading.AccelerationX > 0.9)
                    x1 = true;
                if (reading.AccelerationX < -0.9)
                    x2 = true;
                if (reading.AccelerationY > 0.9)
                    y1 = true;
                if (reading.AccelerationY < -0.9)
                    y2 = true;
                if (reading.AccelerationZ > 0.9)
                    z1 = true;
                if (reading.AccelerationZ < -0.9)
                    z2 = true;

                if (x1 && x2 && y1 && y2 && z1 && z2)
                {
                    showmsg(1);
                    _accelerometer = null;
                    
                }
            });
        }

        private void showmsg(int numb)
        {
            switch (numb)
            {
                case 1:
                    ATPstrbtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    ATPmsg.Text = "COMPLETE!";
                    ATPmsg.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    complitedtest = true;
                    break;
                case 2:
                    ATPstrbtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    ATPmsg.Text = "FAILED!";
                    ATPmsg.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    complitedtest = false;
                    break;
            }
        }

        public AccelerometerTestPage()
        {
            this.InitializeComponent();

            HardwareButtons.BackPressed += delegate
            {
                this.Frame.Navigate(typeof(GeolocationTestPage));
            };
        }

        private void ATPstrbtn_Click(object sender, RoutedEventArgs e)
        {
            ATPstrbtn.IsEnabled = false;

            _accelerometer = Accelerometer.GetDefault();

            if (_accelerometer != null)
            {
                uint minReportInterval = _accelerometer.MinimumReportInterval;
                uint reportInterval = minReportInterval > 16 ? minReportInterval : 16;
                _accelerometer.ReportInterval = reportInterval;

                _accelerometer.ReadingChanged += new TypedEventHandler<Accelerometer, AccelerometerReadingChangedEventArgs>(ReadingChanged);
            }
        }

        private void ATPtorepeat_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(AccelerometerTestPage));
            }
        }

        private void ATPtonext_Click(object sender, RoutedEventArgs e)
        {
            SaveStateTest s = new SaveStateTest();
            s.savestate(complitedtest, "AccelerometerTestState");

            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(CompassTestPage));
            }
        }

        private void ATPback_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(GeolocationTestPage));
            }
        }

    }
}
