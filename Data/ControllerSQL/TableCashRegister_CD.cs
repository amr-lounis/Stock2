﻿
using Data.Model;
using System;
using System.Linq;

namespace Data.ControllerSQL
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
            catch (Exception e) {return null; }
        }
        //----------------------------------------------------------------------------------------------------------------
        public static sold_product Get(long p_id)
        {
            var _db = Entities.GetInstance();
            return _db.sold_product.Single(c => c.ID == p_id);
        }
        //----------------------------------------------------------------------------------------------------------------
        public static void Add(sold_product _sold_product)
        {
            var _db = Entities.GetInstance();
            bool _isExist = _db.sold_product.Any(o => (o.ID_PRODUCT == _sold_product.ID_PRODUCT) && (o.ID_INVOICE == _sold_product.ID_INVOICE));
            if (_isExist)
            {
                Console.WriteLine("isExist sold_product sp.QUANTITY = sp.QUANTITY + 1;");
                var sp = _db.sold_product.SingleOrDefault(o => (o.ID_PRODUCT == _sold_product.ID_PRODUCT) && (o.ID_INVOICE == _sold_product.ID_INVOICE));
                sp.QUANTITY = sp.QUANTITY + 1;
                _db.SaveChanges();
                TableInvoices_CD.calcule(sp.ID_INVOICE ?? 0);
            }
            else
            {
                _db.sold_product.Add(_sold_product);
                _db.SaveChanges();
                TableInvoices_CD.calcule(_sold_product.ID_INVOICE ?? 0);
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

            _db.SaveChanges();
            TableInvoices_CD.calcule(o.ID_INVOICE ?? 0);
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
    }
}
