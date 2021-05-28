using Stock.Dataset.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Interfaces
{
    interface ITableCashRegisters
    {
        sold_product get(long _id);
        List<sold_product> searchByInvoice(long _id_invoice);
        void add(sold_product _productsold);
        void edit(sold_product _productsold);
        void edit(long _id, string _column, object _value);
        void delete(long _id);
    }
}
