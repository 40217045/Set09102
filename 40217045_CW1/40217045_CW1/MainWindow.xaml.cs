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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _40217045_CW1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Color SelectedColour = (Color)ColorConverter.ConvertFromString("#4E98FE");
        Color UnselectedColour = (Color)ColorConverter.ConvertFromString("#F0F0F0");
        string messageID = "";
        public MainWindow()
        {
            InitializeComponent();
            txtMessageID.MaxLength = 10;
            //MessageIDSelection();
        }

        private void MessageIDSelection()
        {
            messageID = txtMessageID.Text.ToUpper();
            
            if (messageID.StartsWith("S"))
            {
                MessageBox.Show("SMS messageID = " + messageID);
            }
            else if (messageID.StartsWith("E"))
            {
                MessageBox.Show("Email messageID = " + messageID);
            }
            else if (messageID.StartsWith("T"))
            {
                MessageBox.Show("Tweet messageID = " + messageID);
            }
            else
            {
                MessageBox.Show("MessageID = " + messageID + " is not a valid MessageID");
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (cvsInbox.Visibility == Visibility.Hidden)
            {
                BtnInbox.Background = new SolidColorBrush(SelectedColour);
                BtnSend.Background = new SolidColorBrush(UnselectedColour);
                BtnReports.Background = new SolidColorBrush(UnselectedColour);
                cvsInbox.Visibility = Visibility.Visible;
                cvsSend.Visibility = Visibility.Hidden;
                cvsReports.Visibility = Visibility.Hidden;
            }
            else
            {
                BtnInbox.Background = new SolidColorBrush(UnselectedColour);
                BtnSend.Background = new SolidColorBrush(UnselectedColour);
                BtnReports.Background = new SolidColorBrush(UnselectedColour);
                cvsInbox.Visibility = Visibility.Hidden;

            }
            //MessageBox.Show("Inbox", "Inbox Canvas", MessageBoxButton.OK);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (cvsSend.Visibility == Visibility.Hidden)
            {
                BtnInbox.Background = new SolidColorBrush(UnselectedColour);
                BtnSend.Background = new SolidColorBrush(SelectedColour);
                BtnReports.Background = new SolidColorBrush(UnselectedColour);
                cvsInbox.Visibility = Visibility.Hidden;
                cvsSend.Visibility = Visibility.Visible;
                cvsReports.Visibility = Visibility.Hidden;
            }
            else
            {
                BtnInbox.Background = new SolidColorBrush(UnselectedColour);
                BtnSend.Background = new SolidColorBrush(UnselectedColour);
                cvsSend.Visibility = Visibility.Hidden;
            }
            //MessageBox.Show("Send", "Send Canvas", MessageBoxButton.OK);
        }

        private void BtnReports_Click(object sender, RoutedEventArgs e)
        {
            if (cvsReports.Visibility == Visibility.Hidden)
            {
                BtnReports.Background = new SolidColorBrush(SelectedColour);
                BtnInbox.Background = new SolidColorBrush(UnselectedColour);
                BtnSend.Background = new SolidColorBrush(UnselectedColour);
                cvsReports.Visibility = Visibility.Visible;
                cvsInbox.Visibility = Visibility.Hidden;
                cvsSend.Visibility = Visibility.Hidden;
            }
            else
            {
                BtnInbox.Background = new SolidColorBrush(UnselectedColour);
                BtnSend.Background = new SolidColorBrush(UnselectedColour);
                BtnReports.Background = new SolidColorBrush(UnselectedColour);
                cvsReports.Visibility = Visibility.Hidden;
            }
        }
        private void btnSms_Click(object sender, RoutedEventArgs e)
        {
            if (cvsSms.Visibility == Visibility.Hidden)
            {
                btnSms.Background = new SolidColorBrush(SelectedColour);
                btnEmail.Background = new SolidColorBrush(UnselectedColour);
                btnTweet.Background = new SolidColorBrush(UnselectedColour);
                cvsSms.Visibility = Visibility.Visible;
                cvsEmail.Visibility = Visibility.Hidden;
                cvsTweet.Visibility = Visibility.Hidden;
            }
            else
            {
                btnSms.Background = new SolidColorBrush(UnselectedColour);
                btnEmail.Background = new SolidColorBrush(UnselectedColour);
                btnTweet.Background = new SolidColorBrush(UnselectedColour);
                cvsSms.Visibility = Visibility.Hidden;
            }
        }

        private void btnEmail_Click(object sender, RoutedEventArgs e)
        {
            if (cvsEmail.Visibility == Visibility.Hidden)
            {
                btnSms.Background = new SolidColorBrush(UnselectedColour);
                btnEmail.Background = new SolidColorBrush(SelectedColour);
                btnTweet.Background = new SolidColorBrush(UnselectedColour);
                cvsEmail.Visibility = Visibility.Visible;
                cvsSms.Visibility = Visibility.Hidden;
                cvsTweet.Visibility = Visibility.Hidden;
            }
            else
            {
                btnSms.Background = new SolidColorBrush(UnselectedColour);
                btnEmail.Background = new SolidColorBrush(UnselectedColour);
                btnTweet.Background = new SolidColorBrush(UnselectedColour);
                cvsEmail.Visibility = Visibility.Hidden;
            }
        }

        private void btnTweet_Click(object sender, RoutedEventArgs e)
        {
            if (cvsTweet.Visibility == Visibility.Hidden)
            {
                btnSms.Background = new SolidColorBrush(UnselectedColour);
                btnEmail.Background = new SolidColorBrush(UnselectedColour);
                btnTweet.Background = new SolidColorBrush(SelectedColour);
                cvsTweet.Visibility = Visibility.Visible;
                cvsSms.Visibility = Visibility.Hidden;
                cvsEmail.Visibility = Visibility.Hidden;
            }
            else
            {
                btnSms.Background = new SolidColorBrush(UnselectedColour);
                btnEmail.Background = new SolidColorBrush(UnselectedColour);
                btnTweet.Background = new SolidColorBrush(UnselectedColour);
                cvsTweet.Visibility = Visibility.Hidden;
            }
        }

        private void BtnMessageID_Click(object sender, RoutedEventArgs e)
        {
            MessageIDSelection();
        }
    }
}
