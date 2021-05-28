using Stock.ControllerSQL;
using Stock.Dataset.Model;
using Stock.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Controllers
{
    public class TableInvoices_CV : ITableInvoices
    {
        //-------------------------------------------------------------------------------------
        public sold_invoice get(long _id)
        {
            return TableInvoices_CD.Get(_id);
        }
        //-------------------------------------------------------------------------------------
        public long GetID_NonUsed()
        {
            return TableInvoices_CD.GetLastNonUsed();
        }
        //-------------------------------------------------------------------------------------
        public List<sold_invoice> search(string _value, DateTime _begin, DateTime _end, ref int _this_page, out string _data_out)
        {
            try
            {
                var query = TableInvoices_CD.search(_value, ref _this_page, out _data_out);
                return query.ToList();
            }
            catch (Exception) { _data_out = "ERROR"; return new List<sold_invoice>(); }
        }
        //-------------------------------------------------------------------------------------
        public void add(sold_invoice _soldinvoice)
        {
            TableInvoices_CD.Add(_soldinvoice);
        }
        //-------------------------------------------------------------------------------------
        public void edit(sold_invoice _Invoice)
        {
            TableInvoices_CD.Edit(_Invoice);
        }
        public void edit(long _id, string _column, object _value)
        {
            TableInvoices_CD.Edit(_id, _column, _value);
        }
        //-------------------------------------------------------------------------------------
        public void delete(long _id)
        {
            TableInvoices_CD.Delete(_id);
        }
        //-------------------------------------------------------------------------------------
    }
}