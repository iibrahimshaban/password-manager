using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace password_manager
{
    internal class Login : WebsiteInfo
    {
        public override string print()
        {
            return this.Password;
        }
    }
}
