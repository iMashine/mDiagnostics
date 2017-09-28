using mDiagnostics.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Phone.UI.Input;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Capture;
using Windows.Media.Devices;
using Windows.Storage;
using Windows.Media.MediaProperties;
using Windows.Media;
using Stream = System.IO.Stream;

namespace mDiagnostics
{
    public sealed partial class MicrophoneTestPage : Page
    {
        public bool completedtest = false;

        private MediaCapture _mediaCaptureManager;
        private StorageFile _recordStorageFile;

        public MicrophoneTestPage()
        {
            this.InitializeComponent();
            HardwareButtons.BackPressed += delegate
            {
                this.Frame.Navigate(typeof(SpeakerTestPage));
            };

            InitializeAudioRecording();
        }

        private void MTPtonext_Click(object sender, RoutedEventArgs e)
        {
            SaveStateTest s = new SaveStateTest();
            s.savestate(completedtest, "MicrophoneTestState");

            if (this.Frame != null)
                this.Frame.Navigate(typeof(DisplayTestPage));
        }

        private void MTPback_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (this.Frame != null)
                this.Frame.Navigate(typeof(SpeakerTestPage));
        }

        private void MTPstrbtn_Click(object sender, RoutedEventArgs e)
        {
            MTPstrbtn.IsEnabled = false;
            CaptureAudio();
        }

        private async void InitializeAudioRecording()
        {
            _mediaCaptureManager = new MediaCapture();
            var settings = new MediaCaptureInitializationSettings();
            settings.StreamingCaptureMode = StreamingCaptureMode.Audio;
            settings.MediaCategory = MediaCategory.Other;
            settings.AudioProcessing = AudioProcessing.Default;

            await _mediaCaptureManager.InitializeAsync(settings);
        }

        private async void CaptureAudio()
        {
            try
            {
                String fileName = "AUDIO_FILE_NAME";

                _recordStorageFile = await KnownFolders.VideosLibrary.CreateFileAsync(fileName, CreationCollisionOption.GenerateUniqueName);

                MediaEncodingProfile recordProfile = MediaEncodingProfile.CreateM4a(AudioEncodingQuality.Medium);

                await _mediaCaptureManager.StartRecordToStorageFileAsync(recordProfile, this._recordStorageFile);

                DispatcherTimer dt = new DispatcherTimer();
                dt.Interval = new TimeSpan(0, 0, 3);
                dt.Tick += delegate
                {
                    StopCapture();
                    dt.Stop();
                };
                dt.Start();
            }
            catch (Exception e)
            {
            }
        }

        private async void StopCapture()
        {
            await _mediaCaptureManager.StopRecordAsync();
            CheckedArr();
        }

        private async void CheckedArr()
        {
            Stream dest = null;
            StorageFile aaa = _recordStorageFile;
            _recordStorageFile = null;
            byte[] buffer = new byte[80000];

            using (Stream source = await aaa.OpenStreamForReadAsync())
            {       
                int bytesRead;
                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0 && source.Length < 1)
                {
                    dest.Write(buffer, 1, bytesRead);
                }
            }

            MicrophoneVolume(buffer);
            aaa = null;
        }

        private void MicrophoneVolume(byte[] data)
        {
            double k = 0;
            ushort byte1 = 0;
            ushort byte2 = 0;
            int volume = 250;

            for (int i = 0; i < data.Length - 1; i += 2)
            {
                byte1 = data[i];
                byte2 = data[i + 1];

                if (byte1 >= volume)
                    k++;
                if (byte2 >= volume)
                    k++;
            }

            if (k >= 1300)
            {
                completedtest = true;
                showmsg();
            }
            else
            {
                completedtest = false;
                showmsg();
            }

        }

        private void showmsg()
        {
            if (completedtest)
            {
                MTPmsg.Text = "COMPLETE!";
                MTPmsg.Visibility = Windows.UI.Xaml.Visibility.Visible;
                MTPstrbtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                MTPmsg.Text = "FAILED!";
                MTPmsg.Visibility = Windows.UI.Xaml.Visibility.Visible;
                MTPstrbtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private void MTPtorepeat_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(MicrophoneTestPage));
            }
        }
    }
}
