using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Interfaces
{
    public interface ILogin
    {
        Boolean Login(string user, string password);
    }
}
