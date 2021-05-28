using Stock.Dataset.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Interfaces
{
    interface ITableInvoices
    {
        sold_invoice get(long _id);
        long GetID_NonUsed();
        List<sold_invoice> search(string _value,DateTime _begin,DateTime _end, ref int _this_page, out string _data_out);
        void add(sold_invoice _Invoice);
        void edit(sold_invoice _Invoice);
        void edit(long _id, string _column, object _value);
        void delete(long _id);
    }
}
