
using System.Reflection.Metadata;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace password_manager
{
    internal class Program
    {
        private static readonly Dictionary<string, WebsiteInfo> _websiteManager = new();
        public const string File_path = "Password.txt";



        static void Main(string[] args)
        {

            ReadInfo();
            while (true)
            {
                
                if (_websiteManager.ContainsKey("admin"))
                {
                    if (Login() == true)
                    Console.WriteLine("welecom admin ... ");
                    break;
                }
                else
                {
                    if (SignUp() == true)
                        break;
                }
            } 
                
                while (true)
                {
                    Console.WriteLine("[1]list all websites \t [2]Add/change website info");
                    Console.WriteLine("[3]get website info  \t [4]delete website ");
                    Console.Write("selected option : ");
                    int choice = 0;
                    int.TryParse(Console.ReadLine(), out choice);

                    switch (choice)
                    {
                        case 1:
                            ListWebsites();
                            break;
                        case 2:
                            AddWebsites();
                            break;
                        case 3:
                            GetWebsites();
                            break;
                        case 4:
                            DeleteWebsites();
                            break;
                        default:
                            Console.WriteLine("please enter a valid number ");
                            break;
                    }
                }
            
        }

        private static bool Login()
        {
            Console.Write("please enter password : ");
                string pass = Console.ReadLine().ToLower();
                Login log = new Login() { Password =pass};

            foreach (var item in _websiteManager)
            {
                if (_websiteManager.ContainsKey("admin"))
                {
                    if (item.Value.Password == log.Password)
                        return true;
                    else
                        Console.WriteLine("incorrect password");
                }
            }
            return false;

                
        }
        private static bool SignUp() 
        {


            Console.WriteLine("for skip signup  ......... press skip ");
            string input = Console.ReadLine().ToLower();
            if (input == "skip")
                return true;
            else if (input != "skip")
            {

                Console.Write("enter the new admin password : ");
                string password = Console.ReadLine().ToLower();
                WebsiteInfo web = new Login() { Password = password };
                _websiteManager.Add("admin", web);
                SaveInfo();
                return true;

            }
            else
                return false;



        }

        private static void DeleteWebsites()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("deleting ....\nenter the website name : ");
            string input = Console.ReadLine().ToLower();
            if (_websiteManager.ContainsKey(input))
                _websiteManager.Remove(input);
            else
                Console.WriteLine($"there are no website named {input}");
            Console.ForegroundColor = ConsoleColor.White;
            SaveInfo();
        }

        private static void GetWebsites()
        {
            
            Console.Write("enter the website name : ");
            string input = Console.ReadLine().ToLower();
            Console.ForegroundColor = ConsoleColor.Green;
            if (_websiteManager.ContainsKey(input))
            {
                
                Console.WriteLine(_websiteManager[input].print());
            }
            else
                Console.WriteLine($"there are no website named {input}");
            Console.ForegroundColor= ConsoleColor.White;


        }

        private static void AddWebsites()
        {
            Console.Write("website Name  : ");
            string Name = Console.ReadLine().ToLower().Trim();
            Console.Write("website password  : ");
            string Password = Console.ReadLine();

            //change
            if (_websiteManager.ContainsKey(Name))
            {
                Console.Write("write the new password :  ");
                _websiteManager[Name].Password = Password;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("your password had been changed successfully");
                Console.ForegroundColor = ConsoleColor.White;
            } // add
            else
            {
                Console.Write("signed in gmail  : ");
                string Gmail = Console.ReadLine();
                Console.Write("website userName  : ");
                string UserName = Console.ReadLine();
                Console.Write("signed in phone number  : ");
                string PhoneNumber = Console.ReadLine();

               Information web = new Information();
                web.Password = Password;
                web.SetGmail(Gmail);
                web.SetuserName(UserName);
                web.SetPhone(PhoneNumber);
                _websiteManager.Add(Name, web);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("your website had been added successfully");
                Console.ForegroundColor = ConsoleColor.White;
            }
            SaveInfo();
        }

        private static void ListWebsites()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("all current websites are ");
            Console.ForegroundColor = ConsoleColor.White;
            
            foreach (var website in _websiteManager)
            {
                Console.WriteLine($"{website.Key} = {website.Value.print()} ");
            }
            
        }

        private static void ReadInfo()
        {
            if (File.Exists(File_path))
            {
                var Lines = File.ReadAllText(File_path);
                foreach (var line in Lines.Split(Environment.NewLine))
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        var equalSign = line.IndexOf('=');
                        var WebName = line.Substring(0, equalSign).RemoveWhiteSpaces();
                        var WebInfo = line.Substring(equalSign + 1).RemoveWhiteSpaces();

                        var decrypt = EncryptionUtilities.decrypt(WebInfo);

                        if (WebName == "admin")
                        { 
                          Login Lweb = new Login() { Password = decrypt };
                            _websiteManager.Add("admin", Lweb);
                        }
                        else 
                        {
                            var RightValues = new List<string>();

                            string[] arr = decrypt.Split(',');
                            string[] PasworedCheck = arr[0].Split(':');
                            RightValues.Add(PasworedCheck[1].Trim());

                            string[] GmailCheck = arr[1].Split(':');
                            RightValues.Add(GmailCheck[1].Trim());

                            string[] UserCheck = arr[2].Split(':');
                            RightValues.Add(UserCheck[1].Trim());

                            string[] PhoneCheck = arr[3].Split(':');
                            RightValues.Add(PhoneCheck[1].Trim());

                            Information Iweb = new Information() ;
                            Iweb.Password = RightValues[0];
                            Iweb.SetGmail(RightValues[1]);
                            Iweb.SetuserName(RightValues[2]);
                            Iweb.SetPhone(RightValues[3]);
                            _websiteManager.Add(WebName, Iweb);
                        }

                         
                        
                    }

                }
            }

        }
        private static void SaveInfo()
        {
            
            var sb = new StringBuilder();

            sb.AppendLine("");
           // }
 
            foreach (var website in _websiteManager)
            {
  
                    sb.AppendLine($"{website.Key} = {EncryptionUtilities.encrypt(website.Value.print().RemoveWhiteSpaces())}");
                
            }
 
            // it will put the file beside the file.exe
            File.WriteAllText(File_path,sb.ToString());
            
        }
    }
}
