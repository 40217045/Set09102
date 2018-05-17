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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _40217045_CW1
{
    /// <summary>
    /// Interaction logic for NewMessage.xaml
    /// </summary>
    public partial class NewMessage : Window
    {
        string msgType = "";
        string user = "";
        List<Sms> SmsList = new List<Sms>();
        List<Tweet> TweetList = new List<Tweet>();
        List<Email> EmailList = new List<Email>();
        public NewMessage(string messageID, string messageType, string username)
        {
            user = username;
            //LoadUser(user);
            LoadLists(user);
            InitializeComponent();
            msgType = messageType;
            if (msgType == "SMS")
            {
                Title = "Send New SMS";
                lblSmsMessageID.Content = messageID;
                cvsSms.Visibility = Visibility.Visible;
                cvsTweet.Visibility = Visibility.Hidden;
                cvsEmail.Visibility = Visibility.Hidden;
            }
            else if (msgType == "Email")
            {
                Title = "Send New Email";
                lblMessageID.Content = messageID;
                cvsEmail.Visibility = Visibility.Visible;
                cvsTweet.Visibility = Visibility.Hidden;
                cvsSms.Visibility = Visibility.Hidden;
            }
            else if (msgType== "Tweet")
            {
                Title = "Send New Tweet";
                lblTweetMessageID.Content = messageID;
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

        private void LoadUser(string user)
        {
            throw new NotImplementedException();
        }

        private void LoadLists(string user)
        {
            try
            {
                string FileLocSms = @"Resources\" + user + "-sms.json";
                string jsonSms = File.ReadAllText(FileLocSms);
                SmsList = JsonConvert.DeserializeObject<List<Sms>>(jsonSms);

                string FileLocTweet = @"Resources\" + user + "-tweet.json";
                string jsonTweet = File.ReadAllText(FileLocTweet);
                TweetList = JsonConvert.DeserializeObject<List<Tweet>>(jsonTweet);

                string FileLocEmail = @"Resources\" + user + "-email.json";
                string jsonEmail = File.ReadAllText(FileLocEmail);
                EmailList = JsonConvert.DeserializeObject<List<Email>>(jsonEmail);

            }
            catch (Exception)
            {

               
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

        private void btnSendTweet_Click(object sender, RoutedEventArgs e)
        {
            newTweet();
            SaveTweet(user);
            MessageBox.Show("Tweet Sent");
            this.Close();
        }

        private void SaveTweet(string user)
        {
            string FileLoc = @"Resources\" + user + "-Tweet.json"; //filename where data will be stored
            File.WriteAllText(FileLoc, JsonConvert.SerializeObject(TweetList));
            Console.WriteLine("All data saved to " + FileLoc);
        }

        private void newTweet()
        {
            Tweet T = new Tweet();
            T.Message = txtTweet.Text;
            
            T.TweetID = lblTweetMessageID.Content.ToString();
            T.From = "@"+user;
            TweetList.Add(T);
        }

        private void btnSendSms_Click(object sender, RoutedEventArgs e)
        {
            newSms();
            SaveSMS(user);
            MessageBox.Show("Sms Sent to "+txtTo.Text );
            this.Close();
        }

        private void newSms()
        {
            double PhoneNumber;
            try
            {
                PhoneNumber = double.Parse(txtTo.Text);
            }
            catch (Exception)
            {

                throw;
            }

            Sms S = new Sms();
            S.Message = txtSms.Text;
            S.To = PhoneNumber;
            S.SmsID = lblSmsMessageID.Content.ToString();
            S.From = 0123456789;
            SmsList.Add(S);
        }

        private void SaveSMS(string user)
        {
            string FileLoc = @"Resources\"+ user + "-sms.json"; //filename where data will be stored
            File.WriteAllText(FileLoc, JsonConvert.SerializeObject(SmsList));
            Console.WriteLine("All data saved to " + FileLoc);
        }
    }
}
