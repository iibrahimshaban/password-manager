using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace password_manager
{
    internal abstract class WebsiteInfo
    {

        public string Password {  get; set; }

        public abstract string print();
       
    }
}
