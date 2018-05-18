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
using System.IO;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text.RegularExpressions;

namespace _40217045_CW1
{
    /// <summary>
    /// Interaction logic for NewMessage.xaml
    /// </summary>
    public partial class NewMessage : Window
    {
        string msgType = "";
        string user = "";
        string username = "";
        string name = "";
        double MyPhoneNo;
        string twitterhandle = "";
        string email = "";



        // Lists for storing data these will be read from when the window is opened and written to after a message is sent
        List<Sms> SmsList = new List<Sms>();
        List<Tweet> TweetList = new List<Tweet>();
        List<Email> EmailList = new List<Email>();
        List<User> UserList = new List<User>();

        // arrays for storing text speak 
        private string[] AbbrevArray = new string[] { };
        private string[] PhraseArray = new string[] { };

        public NewMessage(string messageID, string messageType, string username)
        {
            user = username;
            LoadUser(user);// loads lists of users
            LoadLists(user);//Loads lists of sent messages 
            LoadTextWord(); //Loads method for text speak
            InitializeComponent();
            msgType = messageType;
            //selects what canvas to display
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
            else if (msgType == "Tweet")
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

            foreach (User U in UserList)
            {
                if (user == U.Username)
                {
                    username = U.Username;
                    name = U.Name;
                    email = U.EmailAdd;
                    twitterhandle = U.Twitter;
                    MyPhoneNo = U.Phonenumber;
                }
            }
        }

        private void LoadLists(string user)
        {
            try
            {
                //loads sms messages from user
                string FileLocSms = @"Resources\" + user + "-sms.json";
                string jsonSms = File.ReadAllText(FileLocSms);
                SmsList = JsonConvert.DeserializeObject<List<Sms>>(jsonSms);
                //loads tweets from user
                string FileLocTweet = @"Resources\" + user + "-tweet.json";
                string jsonTweet = File.ReadAllText(FileLocTweet);
                TweetList = JsonConvert.DeserializeObject<List<Tweet>>(jsonTweet);
                //loads emails from user
                string FileLocEmail = @"Resources\" + user + "-email.json";
                string jsonEmail = File.ReadAllText(FileLocEmail);
                EmailList = JsonConvert.DeserializeObject<List<Email>>(jsonEmail);
                //Loads users from file
                string FileLocUser = @"Resources\Users.json";
                string jsonUser = File.ReadAllText(FileLocUser);
                UserList = JsonConvert.DeserializeObject<List<User>>(jsonUser);
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
            newEmail();
            SaveEmail(user);
            MessageBox.Show("Email Sent");
            this.Close();
        }

        private void SaveEmail(string user)
        {
            string FileLoc = @"Resources\" + user + "-Email.json"; //filename where data will be stored
            File.WriteAllText(FileLoc, JsonConvert.SerializeObject(EmailList));
            Console.WriteLine("All data saved to " + FileLoc);
        }

        private void newEmail()
        {
            Email E = new Email();
            E.MessageID = lblMessageID.Content.ToString();
            E.Sender = email;
            E.Recipient = txtRecipient.Text;
            E.Subject = txtSubject.Text;
            E.Message = txtEmail.Text;

            EmailList.Add(E);
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
            T.From = twitterhandle;
            TweetList.Add(T);
        }

        private void btnSendSms_Click(object sender, RoutedEventArgs e)
        {
            newSms();
            SaveSMS(user);
            MessageBox.Show("Sms Sent to " + txtTo.Text);
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
            S.From = MyPhoneNo;
            SmsList.Add(S);
        }

        private void SaveSMS(string user)
        {
            string FileLoc = @"Resources\" + user + "-sms.json"; //filename where data will be stored
            File.WriteAllText(FileLoc, JsonConvert.SerializeObject(SmsList));
            Console.WriteLine("All data saved to " + FileLoc);
        }



        private void LoadTextWord()
        {
            try
            {
                List<string> Abbreviations = new List<string>();
                List<string> Phrases = new List<string>();
                string filename = @"Resources/textwords.csv";
                StreamReader reader = new StreamReader(File.OpenRead(filename));
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    Abbreviations.Add(values[0]);
                    Phrases.Add(values[1]);

                }
                
                AbbrevArray = Abbreviations.ToArray();
                PhraseArray = Phrases.ToArray();

            }

            catch (Exception e)
            {

                Console.WriteLine("Error: " + e);
            }

        }

        private void ConvertTextWord()
        {

            string text = " " + txtMessage.Text + " ";



            int total = AbbrevArray.Count();
            for (int i = 0; i < total; i++)
            {
                string str = " " + AbbrevArray[i] + " ";
                
                if (text.Contains(str))
                {
                    string filename = @"Resources/textwords.csv";
                    StreamReader reader = new StreamReader(File.OpenRead(filename));
                    string Cleaned = Regex.Replace(text, str, str + " <" + PhraseArray[i] + "> ");
                    text = Cleaned;

                }
                else if (text.Contains(str.ToLower()) && str.ToLower() != "at")
                {
                    string filename = @"Resources/textwords.csv";
                    StreamReader reader = new StreamReader(File.OpenRead(filename));
                    string Cleaned = Regex.Replace(text, str.ToLower(), str.ToLower() + " <" + PhraseArray[i] + "> ");
                    text = Cleaned;

                }


            }
            Tweet = text;
        }

    }
}
