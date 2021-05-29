using Stock.ControllerSQL;
using Stock.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Stock.Controllers
{
    public class CLogin : ILogin
    {
        public bool Login(string _user, string _password)
        {
            var id = TableUsers_CD.Logine(_user, _password);
            Configs.thisUser_ID = id;
            return id > 0 ? true : false;
        }
    }
}
