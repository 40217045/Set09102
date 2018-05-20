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

        string Message;



        // Lists for storing data these will be read from when the window is opened and written to after a message is sent
        List<Sms> SmsList = new List<Sms>();
        List<Tweet> TweetList = new List<Tweet>();
        List<Email> EmailList = new List<Email>();
        List<User> UserList = new List<User>();

        // arrays for storing text speak 
        private string[] TextspeakArray = new string[] { };
        private string[] expandedArray = new string[] { };

        //List to store Incedents, Mentions and hashtags
        List<string> Hashtag = new List<string>();
        List<string> Trend = new List<string>();
        List<Incidents> Incident_List = new List<Incidents>();

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
                txtCentreNumber1.MaxLength = 2;
                txtCentreNumber2.MaxLength = 3;
                txtCentreNumber3.MaxLength = 2;
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
            }
            catch (Exception)

            {


            }
            try
            {
                //loads tweets from user
                string FileLocTweet = @"Resources\" + user + "-tweet.json";
                string jsonTweet = File.ReadAllText(FileLocTweet);
                TweetList = JsonConvert.DeserializeObject<List<Tweet>>(jsonTweet);
            }
            catch (Exception)
            {


            }
            try
            {
                //loads emails from user
                string FileLocEmail = @"Resources\" + user + "-email.json";
                string jsonEmail = File.ReadAllText(FileLocEmail);
                EmailList = JsonConvert.DeserializeObject<List<Email>>(jsonEmail);
            }
            catch (Exception)
            {


            }
            try
            {
                //Loads users from file
                string FileLocUser = @"Resources\Users.json";
                string jsonUser = File.ReadAllText(FileLocUser);
                UserList = JsonConvert.DeserializeObject<List<User>>(jsonUser);
            }
            catch (Exception)
            {


            }
            try
            {
                //Loads Incident List
                string FileLocSIR = @"Resources\Incidents.json";
                string jsonSIR = File.ReadAllText(FileLocSIR);
                Incident_List = JsonConvert.DeserializeObject<List<Incidents>>(jsonSIR);
            }
            catch (Exception)
            {


            }

            //Loads Mentions and Hashtag list from Csv File



                    
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
            // Saves Email
            string FileLoc = @"Resources\" + user + "-Email.json"; //filename where data will be stored
            File.WriteAllText(FileLoc, JsonConvert.SerializeObject(EmailList));
            Console.WriteLine("All data saved to " + FileLoc);

            // Saves Serious Incident Report
            string FileLocIncident = @"Resources\Incidents.json"; //filename where data will be stored
            File.WriteAllText(FileLocIncident, JsonConvert.SerializeObject(Incident_List));
            Console.WriteLine("All data saved to " + FileLoc);

        }

        private void newEmail()
        {
            ConvertTextWord();
            string CentreNumber = "N/A";
            string Incident = "N/A";
            //Serious Incident Report check
            if (txtSubject.Text.ToLower().Contains("sir"))
            {
                CentreNumber = txtCentreNumber1.Text+"-" + txtCentreNumber2.Text+ "-"+txtCentreNumber3.Text;
                Incident = cmbNature.Text;
                Message = "Serious Incident Report \n Centre Number:" + CentreNumber+"\n Nature Of Incident: " + Incident+"\nMessage Reads:\n"+Message;

                //Checks if incedent has happened before
                
                try
                {
                    bool PreviousIncident = false;


                    foreach (Incidents I in Incident_List)
                    {

                        if (Incident == I.Incident)
                        {

                            I.Occurances = I.Occurances + 1;
                            
                            PreviousIncident = true;
                            break;

                        }
                    }

                    if (PreviousIncident == false)
                    {
                        Incidents NewI = new Incidents(Incident, 1);
                        Incident_List.Add(NewI);
                    }
                    PreviousIncident = false;
                }
                catch (Exception){}
            }



            Email E = new Email();
            E.MessageID = lblMessageID.Content.ToString();
            E.Sender = email;
            E.Recipient = txtRecipient.Text;
            E.Subject = txtSubject.Text;
            E.Message = Message;

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
            ConvertTextWord();
            Tweet T = new Tweet();
            T.Message = Message;

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
            ConvertTextWord();
            Sms S = new Sms();
            S.Message = Message;
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


        //methods for converting text speak
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
                
                TextspeakArray = Abbreviations.ToArray();
                expandedArray = Phrases.ToArray();

            }

            catch (Exception e)
            {

                Console.WriteLine("Error: " + e);
            }

        }

        private void ConvertTextWord()
        {
            string text ="";
            if (msgType =="SMS")
            {
                text = " " + txtSms.Text + " ";
                ConvertTextWord(text);
                
            }
            else if (msgType == "Tweet")
            {
                text = " " + txtTweet.Text + " ";
                ConvertTextWord(text);
            }
            else if (msgType=="Email")
            {
                text = " " + txtEmail.Text + " ";
                ConvertTextWord(text);

            }
                      
        }

        private void ConvertTextWord(string text)
        {
            
              int total = TextspeakArray.Count();
            for (int i = 0; i < total; i++)
            {
                string str = " " + TextspeakArray[i] + " ";

                //checks for the phrase is uppercase and expands it
                if (text.Contains(str))
                {
                    string filename = @"Resources/textwords.csv";
                    StreamReader reader = new StreamReader(File.OpenRead(filename));
                    string expanded = expandedArray[i];
                    string Converted = Regex.Replace(text, str, str + " <" + expanded + "> ");
                    text = Converted;

                }
                // cheacks for the phrase is in lowercase and expands it
                else if (text.Contains(str.ToLower()) && str.ToLower() != "at")
                {
                    string filename = @"Resources/textwords.csv";
                    StreamReader reader = new StreamReader(File.OpenRead(filename));
                    string expanded = expandedArray[i];
                   
                    string Converted = Regex.Replace(text, str.ToLower(), str.ToLower() + " <" + expanded + "> ");
                    text = Converted;

                }


            }

            Message = text;
        }

        private void txtSubject_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSubject.Text.ToLower() == "sir")
            {
                this.Width = 500;
                
            }
            else
            {
                this.Width = 300;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtCentreNumber1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(txtCentreNumber1.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtCentreNumber1.Text = txtCentreNumber1.Text.Remove(txtCentreNumber1.Text.Length - 1);
            }
        }

        private void txtCentreNumber2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(txtCentreNumber2.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtCentreNumber2.Text = txtCentreNumber2.Text.Remove(txtCentreNumber2.Text.Length - 1);
            }
        }

        private void txtCentreNumber3_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(txtCentreNumber3.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtCentreNumber3.Text = txtCentreNumber3.Text.Remove(txtCentreNumber3.Text.Length - 1);
            }
        }
    }
}
