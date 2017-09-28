using mDiagnostics.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace mDiagnostics
{
    public sealed partial class ConnectionTestPage : Page
    {
        private bool complitedtest = false;
              
        public ConnectionTestPage()
        {
            this.InitializeComponent();
            
            HardwareButtons.BackPressed += delegate
            {
                this.Frame.Navigate(typeof(CompassTestPage));
            };
        }

        private void CNTPstrbtn_Click(object sender, RoutedEventArgs e)
        {
            CNTPstrbtn.IsEnabled = false;

            Uri sss = new Uri("https://www.google.com");
            asddd.Navigate(sss);

            asddd.NavigationCompleted += delegate
            {
                ConnectionProfile InternetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
                
                if (InternetConnectionProfile != null)
                {
                    DateTime CurrTime = DateTime.Now;
                    TimeSpan TimeDiff = new TimeSpan(0, 1, 0);

                    var LocalUsage = InternetConnectionProfile.GetLocalUsage(CurrTime.Subtract(TimeDiff), CurrTime);

                    if (LocalUsage.BytesSent != null && LocalUsage.BytesReceived != null)
                    {
                        complitedtest = true;
                        showmsg(1);
                    }
                    else
                    {
                        complitedtest = false;
                        showmsg(2);
                    }
                }
            };


            asddd.NavigationFailed += delegate
            {
                ConnectionProfile InternetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();

                if (InternetConnectionProfile != null)
                {
                    DateTime CurrTime = DateTime.Now;
                    TimeSpan TimeDiff = new TimeSpan(0, 1, 0);

                    var LocalUsage = InternetConnectionProfile.GetLocalUsage(CurrTime.Subtract(TimeDiff), CurrTime);

                    if (LocalUsage.BytesSent != null && LocalUsage.BytesReceived != null)
                    {
                        complitedtest = true;
                        showmsg(1);
                    }
                    else
                    {
                        complitedtest = false;
                        showmsg(2);
                    }
                }
            };
        }

        private void showmsg(int numb)
        {
            switch (numb)
            {
                case 1:
                    CNTPstrbtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    CNTPmsg.Text = "COMPLETE!";
                    CNTPmsg.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    break;
                case 2:
                    CNTPstrbtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    CNTPmsg.Text = "FAILED!";
                    CNTPmsg.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    break;
            }
        }

        private void CNTPback_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(CompassTestPage));
            }
        }

        private void CNTPtorepeat_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(ConnectionTestPage));
            }
        }

        private void CNTPtonext_Click(object sender, RoutedEventArgs e)
        {
            SaveStateTest s = new SaveStateTest();
            s.savestate(complitedtest, "ConnectionTestState");

            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(FinalResultsPage));
            }
        }
    }
}
