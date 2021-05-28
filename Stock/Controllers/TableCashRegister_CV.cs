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
    public class TableCashRegister_CV : ITableCashRegisters
    {
        //-------------------------------------------------------------------------------------
        public sold_product get(long _id)
        {
            return TableCashRegister_CD.Get(_id);
        }
        //-------------------------------------------------------------------------------------
        public List<sold_product> searchByInvoice(long _id_invoice) 
        { 
            try
            {
                return TableCashRegister_CD.search(_id_invoice).ToList();
            }
            catch (Exception) { return new List<sold_product>(); }
        }
        //-------------------------------------------------------------------------------------
        public void add(sold_product _productsold)
        {
            TableCashRegister_CD.Add(_productsold);
        }
        //-------------------------------------------------------------------------------------
        public void edit(sold_product _productsold)
        {
            TableCashRegister_CD.Edit(_productsold);
        }
        public void edit(long _id, string _column, object _value)
        {
            TableCashRegister_CD.Edit(_id, _column, _value);
        }
        //-------------------------------------------------------------------------------------
        public void delete(long _id)
        {
            TableCashRegister_CD.Delete(_id);
        }
        //-------------------------------------------------------------------------------------
    }
}
