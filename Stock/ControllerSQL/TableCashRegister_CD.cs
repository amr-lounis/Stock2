using Stock.Dataset.Model;
using Stock.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.ControllerSQL
{
    public static class TableCashRegister_CD
    {
        public static IQueryable<sold_product> search(long _id_invoice)
        {
            try
            {
                var _db = Entities.GetInstance();
                return  _db.sold_product.Where(c => c.ID_INVOICE == _id_invoice ) ;
            }
            catch (Exception e) { log(e.Message); return null; }
        }
        //----------------------------------------------------------------------------------------------------------------
        public static sold_product Get(long p_id)
        {
            var _db = Entities.GetInstance();
            return _db.sold_product.Single(c => c.ID == p_id);
        }
        //----------------------------------------------------------------------------------------------------------------
        public static void Add(sold_product _productsold)
        {
            var _db = Entities.GetInstance();
            bool _isExist = _db.sold_product.Any(o => (o.ID_PRODUCT == _productsold.ID_PRODUCT) && (o.ID_INVOICE == _productsold.ID_INVOICE));
            if (!_isExist)
            {
                calcule(ref _productsold);
                _db.sold_product.Add(_productsold);

                _db.SaveChanges();
                TableInvoices_CD.calcule(_productsold.ID_INVOICE ?? 0);
            }
            else
            {
                log("isExist", "info");
            }
        }
        //----------------------------------------------------------------------------------------------------------------
        public static void Edit(sold_product _productsold)
        {
            var _db = Entities.GetInstance();
            var o = Get(_productsold.ID);
            o.ID_INVOICE = _productsold.ID_INVOICE;
            o.ID_PRODUCT = _productsold.ID_PRODUCT;
            o.MONEY_ONE = _productsold.MONEY_ONE;
            o.QUANTITY = _productsold.QUANTITY;
            o.TAX_PERCE = _productsold.TAX_PERCE;
            o.STAMP = _productsold.STAMP;

            calcule(ref _productsold);
            _db.SaveChanges();

            TableInvoices_CD.calcule(_productsold.ID_INVOICE ?? 0);
        }
        public static void Edit(long _id, string _column, object _value)
        {
            var _db = Entities.GetInstance();
            var o = Get(_id);
            switch (_column)
            {
                case "ID_INVOICE": o.ID_INVOICE = (long)_value; break;
                case "NAME": o.NAME = (string)_value; break;
                case "DESCRIPTION": o.DESCRIPTION = (string)_value; break;
                case "MONEY_ONE": o.MONEY_ONE = (double)_value; break;
                case "QUANTITY": o.QUANTITY = (double)_value; break;
                case "TAX_PERCE": o.TAX_PERCE = (double)_value; break;
                case "STAMP": o.STAMP = (double)_value; break;
                default: break;
            }

            calcule(ref o);
            _db.SaveChanges();

            TableInvoices_CD.calcule(o.ID_INVOICE ?? 0);
        }
      
        //----------------------------------------------------------------------------------------------------------------
        public static void Delete(long p_id)
        {
            var _db = Entities.GetInstance();
            var o = _db.sold_product.Single(c => c.ID == p_id);
            _db.sold_product.Remove(o);

            _db.SaveChanges();
            TableInvoices_CD.calcule(o.ID_INVOICE ?? 0);
        }
        //----------------------------------------------------------------------------------------------------------------
        public static void calcule(ref sold_product _productsold)
        {
            try
            {
                var v = ((_productsold.MONEY_ONE) * _productsold.QUANTITY + (_productsold.MONEY_ONE * _productsold.TAX_PERCE / 100) + _productsold.STAMP);
                _productsold.MONEY_PAID = Helper.rnd(v);
            }
            catch (Exception e) { log(e.Message); }
        }
        //----------------------------------------------------------------------------------------------------------------
        static void log(string _data, string _type = "error")
        {
            Console.WriteLine("\n----------------------------------\n" + _type + ":" + _data + "\n----------------------------------\n");
        }
        //----------------------------------------------------------------------------------------------------------------
    }
}
