using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _40217045_CW1
{
    /// <summary>
    /// Interaction logic for NewMessage.xaml
    /// </summary>
    public partial class NewMessage : Window
    {
        string msgType = "";
        public NewMessage(string messageID, string messageType)
        {
            InitializeComponent();
            msgType = messageType;
            if (msgType == "SMS")
            {
                Title = "Send New SMS";
                cvsSms.Visibility = Visibility.Visible;
                cvsTweet.Visibility = Visibility.Hidden;
                cvsEmail.Visibility = Visibility.Hidden;
            }
            else if (msgType == "Email")
            {
                Title = "Send New Email";
                cvsEmail.Visibility = Visibility.Visible;
                cvsTweet.Visibility = Visibility.Hidden;
                cvsSms.Visibility = Visibility.Hidden;
            }
            else if (msgType== "Tweet")
            {
                Title = "Send New Tweet";
                cvsTweet.Visibility = Visibility.Visible;
                cvsEmail.Visibility = Visibility.Hidden;
                cvsSms.Visibility = Visibility.Hidden;
            }
            else
            {
                cvsSms.Visibility = Visibility.Hidden;
                cvsEmail.Visibility = Visibility.Hidden;
                cvsTweet.Visibility = Visibility.Hidden;
            }
        }

        private void txtRecipient_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnSendEmail_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancelTweet_Click(object sender, RoutedEventArgs e)
        {
            txtTweet.Clear();
        }

        private void btnCancelSms_Click(object sender, RoutedEventArgs e)
        {
            txtSms.Clear();
        }
    }
}
