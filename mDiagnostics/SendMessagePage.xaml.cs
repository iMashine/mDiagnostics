using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Email;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace mDiagnostics
{
    public sealed partial class SendMessagePage : Page
    {
        public SendMessagePage()
        {
            this.InitializeComponent();
            Start();
        }
        
        private async void Start()
        {
            EmailRecipient sendTo = new EmailRecipient()
            {
                Address = "j.mm@bk.ru"
            };

            EmailMessage mail = new EmailMessage();
            mail.Subject = "this is the Subject";
            mail.Body = "HUIHUIHUI";

            //mail.To.Add(sendTo);
            //mail.Bcc.Add(sendTo);
            mail.CC.Add(sendTo);

            await EmailManager.ShowComposeNewEmailAsync(mail);
        }
    }
}
