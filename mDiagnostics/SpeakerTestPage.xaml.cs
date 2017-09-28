using mDiagnostics.Common;
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
using Windows.UI.Xaml.Navigation;

namespace mDiagnostics
{
    public sealed partial class SpeakerTestPage : Page
    {
        private int k = 0, i;
        private Random rnd = new Random();
        private bool completedtest = false;

        public SpeakerTestPage()
        {
            this.InitializeComponent();
            HardwareButtons.BackPressed += delegate
            {
                if (this.Frame != null)
                {
                    this.Frame.Navigate(typeof(MainPage));
                }
            };
        }

        private void rndnmb()
        {
            i = 0;
            i = rnd.Next(1, 10);

            switch (i)
            {
                case 1:
                    audioplayer.Source = new Uri("ms-appx:///Assets/sounds/1.mp3");
                    break;
                case 2:
                    audioplayer.Source = new Uri("ms-appx:///Assets/sounds/2.mp3");
                    break;
                case 3:
                    audioplayer.Source = new Uri("ms-appx:///Assets/sounds/3.mp3");
                    break;
                case 4:
                    audioplayer.Source = new Uri("ms-appx:///Assets/sounds/4.mp3");
                    break;
                case 5:
                    audioplayer.Source = new Uri("ms-appx:///Assets/sounds/5.mp3");
                    break;
                case 6:
                    audioplayer.Source = new Uri("ms-appx:///Assets/sounds/6.mp3");
                    break;
                case 7:
                    audioplayer.Source = new Uri("ms-appx:///Assets/sounds/7.mp3");
                    break;
                case 8:
                    audioplayer.Source = new Uri("ms-appx:///Assets/sounds/8.mp3");
                    break;
                case 9:
                    audioplayer.Source = new Uri("ms-appx:///Assets/sounds/9.mp3");
                    break;
            }
        }

        private void sendler(int numbb)
        {
            switch (check(numbb))
            {
                case 1:
                    rndnmb();
                    break;
                case 2:
                    k = 0;
                    btnenables(2);
                    showmsg(2);
                    break;
                case 0:
                    k = 0;
                    btnenables(2);
                    showmsg(1);
                    break;
            }
        }

        private int check(int value)
        {
            if (i == value)
            {
                k++;
                if (k == 4) return 0;
                return 1;
            }
            else return 2;
        }

        private void showmsg(int numb)
        {
            switch (numb)
            {
                case 1:
                    STPstartbtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    STPmsg.Text = "COMPLETE!";
                    STPmsg.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    completedtest = true;
                    break;
                case 2:
                    STPstartbtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    STPmsg.Text = "FAILED!";
                    STPmsg.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    completedtest = false;
                    break;
            }
        }

        private void btnenables(int code)
        {
            switch (code)
            {
                case 1:
                    STPb1.IsEnabled = true;
                    STPb2.IsEnabled = true;
                    STPb3.IsEnabled = true;
                    STPb4.IsEnabled = true;
                    STPb5.IsEnabled = true;
                    STPb6.IsEnabled = true;
                    STPb7.IsEnabled = true;
                    STPb8.IsEnabled = true;
                    STPb9.IsEnabled = true;
                    break;
                case 2:
                    STPb1.IsEnabled = false;
                    STPb2.IsEnabled = false;
                    STPb3.IsEnabled = false;
                    STPb4.IsEnabled = false;
                    STPb5.IsEnabled = false;
                    STPb6.IsEnabled = false;
                    STPb7.IsEnabled = false;
                    STPb8.IsEnabled = false;
                    STPb9.IsEnabled = false;
                    break;

            }
        }

        #region btnsclicks
        private void startbtn_Click(object sender, RoutedEventArgs e)
        {
            STPstartbtn.IsEnabled = false;
            k = 0;
            btnenables(1);
            rndnmb();
        }

        private void STPb1_Click(object sender, RoutedEventArgs e)
        {
            sendler(1);
        }

        private void STPb2_Click(object sender, RoutedEventArgs e)
        {
            sendler(2);
        }

        private void STPb3_Click(object sender, RoutedEventArgs e)
        {
            sendler(3);
        }

        private void STPb4_Click(object sender, RoutedEventArgs e)
        {
            sendler(4);
        }

        private void STPb5_Click(object sender, RoutedEventArgs e)
        {
            sendler(5);
        }

        private void STPb6_Click(object sender, RoutedEventArgs e)
        {
            sendler(6);
        }

        private void STPb7_Click(object sender, RoutedEventArgs e)
        {
            sendler(7);
        }

        private void STPb8_Click(object sender, RoutedEventArgs e)
        {
            sendler(8);
        }

        private void STPb9_Click(object sender, RoutedEventArgs e)
        {
            sendler(9);
        }
        #endregion

        private void STPtonext_Click(object sender, RoutedEventArgs e)
        {
            SaveStateTest s = new SaveStateTest();
            s.savestate(completedtest, "SpeakerTestState");

            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(MicrophoneTestPage));
            }
        }

        private void STPtorepeat_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(SpeakerTestPage));
            }
        }

        private void STPback_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(MainPage));
            }
        }
    }
}
