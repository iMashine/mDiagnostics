using mDiagnostics.Common;
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
using Windows.UI.Xaml.Input;
using Windows.Phone.UI.Input;

namespace mDiagnostics
{
    public sealed partial class DisplayTestPage : Page
    {
        private int[] arr = new int[6];
        private int k = 0;
        private Random rnd = new Random();
        private bool completedtest = false;

        public DisplayTestPage()
        {
            this.InitializeComponent();
            HardwareButtons.BackPressed += delegate
                {
                    this.Frame.Navigate(typeof(MicrophoneTestPage));
                };
        }

        private void rndnmb()
        {
            int a = 0;
            int i = 0;
            while (i <6)
            {
                a = rnd.Next(10, 99);
                for (int j = i; j >= 0; --j)
                {
                    if (arr[j] == a)
                        break;
                    else if (j == 0)
                    {
                        arr[i] = a;
                        ++i;
                    }
                }
            }

            TextBlock[] btnarr = {
                                  DTPtxt1,
                                  DTPtxt2,
                                  DTPtxt3,
                                  DTPtxt4,
                                  DTPtxt5,
                                  DTPtxt6
                              };

            for (int h = btnarr.Length-1; h >= 0; h--)
            {
                int m = rnd.Next(h);
                TextBlock temp = btnarr[h];
                btnarr[h] = btnarr[m];
                btnarr[m] = temp;
            }

            for (int l = 0; l < 6; l++)
            {
                btnarr[l].Text = arr[l].ToString();
            }

            DTPtxtMIDDLE.Text = arr[k].ToString();
        }

        private void sendler(string numbb)
        {
            switch (check(numbb))
            {
                case 1:
                    break;
                case 2:
                    k = 0;
                    showmsg(2);
                    break;
                case 0:
                    k = 0;
                    showmsg(1);
                    break;
            }
        }

        private int check(string value)
        {
            if (DTPtxtMIDDLE.Text == value)
            {
                k++;
                if (k == 6) return 0;
                else DTPtxtMIDDLE.Text = arr[k].ToString();
                return 1;
            }
            else return 2;
        }

        private void showmsg(int numb)
        {
            switch (numb)
            {
                case 1:   
                    DTPmsg.Text = "COMPLETE!";
                    DTPmsg.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    completedtest = true;
                    break;
                case 2:
                    DTPmsg.Text = "FAILED!";
                    DTPmsg.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    completedtest = false;
                    break;
            }
        }

        private void DTPtonext_Click(object sender, RoutedEventArgs e)
        {
            SaveStateTest s = new SaveStateTest();
            s.savestate(completedtest, "DisplayTestState");

            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(GeolocationTestPage));
            }
        }

        private void DTPstartbtn_Click(object sender, RoutedEventArgs e)
        {
            DTPstartbtn.IsEnabled = false;
            DTPbtns.Visibility = Windows.UI.Xaml.Visibility.Visible;
            DTPtvgrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            DTPbtncontent.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            rndnmb();
        }

        private void DTPtorepeat_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DisplayTestPage));
        }

        private void DTPback_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (this.Frame != null)
            this.Frame.Navigate(typeof(MicrophoneTestPage));
        }

        private void DTPg1_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            sendler(DTPtxt1.Text);
            DTPg1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void DTPg2_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            sendler(DTPtxt2.Text);
            DTPg2.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void DTPg3_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            sendler(DTPtxt3.Text);
            DTPg3.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void DTPg4_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            sendler(DTPtxt4.Text);
            DTPg4.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void DTPg5_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            sendler(DTPtxt5.Text);
            DTPg5.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void DTPg6_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            sendler(DTPtxt6.Text);
            DTPg6.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }
}
