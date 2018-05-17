using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _40217045_CW1
{
    class User
    {
        private string name;
        private string emailAdd;
        private string twitter;
        private int phonenumber;

        public string Name {
            get { return name; }
            set { name = value; }
        }
        public string EmailAdd
        {
            get { return emailAdd; }
            set { emailAdd = value; }
        }
        public string Twitter
        {
            get { return twitter; }
            set { twitter = value; }
        }
        public int Phonenumber
        {
            get { return phonenumber; }
            set { phonenumber = value; }
        }
    }
}
