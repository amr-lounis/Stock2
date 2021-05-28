using Stock.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Controllers
{
    public class CLogin : ILogin
    {
        public bool Login(string p_user, string p_password)
        {
           if( (p_user == "admin") && (p_password == "admin"))
            {
                Config_CV.thisUser_ID = 0;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
