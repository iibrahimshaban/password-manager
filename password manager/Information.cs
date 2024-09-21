using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace password_manager
{
    internal class Information : WebsiteInfo
    {
        public string Gmail { get; private set; }
        public string Username { get; private set; }
        public string Phonenumber { get; private set; }

        public void SetGmail(string gmail )
        {
            
                this.Gmail = gmail;
        }
        public void SetuserName(string username)
        {
            
                Username = username;
        }
        public void SetPhone(string Phone)
        {

                Phonenumber = Phone;
        }
        
        public override string print()
        {
            return $"Password : {(string.IsNullOrWhiteSpace(Password) ? "signed by gmail" : Password)}, " +
              $"Gmail : {(string.IsNullOrWhiteSpace(Gmail) ? "ibrahimkhaled@gmail.com" : Gmail)}, " +
              $"Username : {(string.IsNullOrWhiteSpace(Username) ? "Ibrahim_khaled" : Username)}, " +
              $"Phone number : {(string.IsNullOrWhiteSpace(Phonenumber) ? "01010101010" : Phonenumber)}";
        }
    }
}
