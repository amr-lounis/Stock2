using Stock.Controllers;
using Stock.Dataset.Model;
using Stock.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stock.ControllerSQL
{
    public static class TableInvoices_CD
    {
        //----------------------------------------------------------------------------------------------------------------
        public static IQueryable<sold_invoice> search(string _value, ref int _this_page, out string _data_out)
        {
            IQueryable<sold_invoice> query = null;
            try
            {
                _value = "";
                   var _db = Entities.GetInstance();
                query = _db.sold_invoice.Where(c =>  (c.DESCRIPTION.ToLower().Contains(_value))).OrderBy("ID"); ;
                _data_out = SkipTake(ref _this_page, ref query);
                return query;
            }
            catch (Exception e) { log(e.Message); _data_out = "ERROR"; return null; }
        }
        //----------------------------------------------------------------------------------------------------------------
        public static sold_invoice Get(long _id)
        {
            var _db = Entities.GetInstance();
            return _db.sold_invoice.Single(c => c.ID == _id);
        }
        //----------------------------------------------------------------------------------------------------------------
        public static long GetLastNonUsed()
        {
            var _db = Entities.GetInstance();
            try
            {
                if (_db.sold_invoice.Where(c => c.MONEY_TOTAL == 0).Count() > 0)
                {
                    return _db.sold_invoice.Where(c => c.MONEY_TOTAL == 0).OrderByDescending(x => x.ID).FirstOrDefault().ID;
                }
                else
                {
                    var o = new sold_invoice();
                    Add(o);
                    return o.ID;
                }
            }
            catch (Exception e) 
            {
                log(e.Message);
                throw new Exception("Error GetLastNonValid");
            }
        }
        //----------------------------------------------------------------------------------------------------------------
        public static sold_invoice GetLastNonValid(long _id_user)
        {
            var _db = Entities.GetInstance();
            return _db.sold_invoice.Where(c => c.VALIDATION.Equals(0) && c.ID_USERS.Equals(_id_user)).First();
        }
        //----------------------------------------------------------------------------------------------------------------
        public static void Add(sold_invoice _soldinvoice)
        {
            var _db = Entities.GetInstance();
            _soldinvoice.DATE_CREATED = DateTime.Now;
            _soldinvoice.DATE_UPDATED = DateTime.Now;
            _db.sold_invoice.Add(_soldinvoice);
            _db.SaveChanges();
        }
        //----------------------------------------------------------------------------------------------------------------
        public static void Edit(sold_invoice _soldinvoice)
        {
            var _db = Entities.GetInstance();
            var o = Get(_soldinvoice.ID);
            o.ID_USERS = _soldinvoice.ID_USERS;
            o.ID_CUSTOMERS = _soldinvoice.ID_CUSTOMERS;
            o.DESCRIPTION = _soldinvoice.DESCRIPTION;
            o.VALIDATION = _soldinvoice.VALIDATION;
            o.MONEY_WITHOUT_ADDEDD = _soldinvoice.MONEY_WITHOUT_ADDEDD;
            o.MONEY_TAX = _soldinvoice.MONEY_TAX;
            o.MONEY_STAMP = _soldinvoice.MONEY_STAMP;
            o.MONEY_TOTAL = _soldinvoice.MONEY_TOTAL;
            o.MONEY_PAID = _soldinvoice.MONEY_PAID;
            o.MONEY_UNPAID = _soldinvoice.MONEY_UNPAID;

            o.DATE_UPDATED = DateTime.Now;
            _db.SaveChanges();
        }
        public static void Edit(long _id, string _column, object _value)
        {
            var _db = Entities.GetInstance();
            var o = Get(_id);
            switch (_column)
            {
                case "ID_USERS": o.ID_USERS = (long)_value; break;
                case "ID_CUSTOMERS": o.ID_CUSTOMERS = (long)_value; break;
                case "DESCRIPTION": o.DESCRIPTION = (string)_value; break;
                case "VALIDATION": o.VALIDATION = (long)_value; break;
                case "MONEY_WITHOUT_ADDEDD": o.MONEY_WITHOUT_ADDEDD = (double)_value; break;
                case "MONEY_PAID": o.MONEY_PAID = (double)_value; break;
                case "MONEY_UNPAID": o.MONEY_UNPAID = (double)_value; break;
                default: break;
            }
            o.DATE_UPDATED = DateTime.Now;
            _db.SaveChanges();
        }

        //----------------------------------------------------------------------------------------------------------------
        public static void Delete(long p_id)
        {
            var _db = Entities.GetInstance();
            _db.sold_invoice.Remove(_db.sold_invoice.Single(c => c.ID == p_id));
            _db.SaveChanges();
        }
        //----------------------------------------------------------------------------------------------------------------
        public static void calcule(long _id)
        {
            double MONEY_WITHOUT_ADDEDD = 0, MONEY_TAX = 0, MONEY_STAMP = 0, MONEY_TOTAL = 0;
            try
            {
                var _db = Entities.GetInstance();
                MONEY_WITHOUT_ADDEDD = _db.sold_product.Where(c => c.ID_INVOICE == _id).Sum(i => i.MONEY_ONE * i.QUANTITY).Value;
                MONEY_TAX = _db.sold_product.Where(c => c.ID_INVOICE == _id).Sum(i => i.MONEY_PAID * i.TAX_PERCE / 100).Value;
                MONEY_STAMP = _db.sold_product.Where(c => c.ID_INVOICE == _id).Sum(i => i.STAMP).Value;
                MONEY_TOTAL = _db.sold_product.Where(c => c.ID_INVOICE == _id).Sum(i => i.MONEY_PAID).Value;
            }
            catch (Exception e) { log(e.Message); }
            try
            {
                var _db = Entities.GetInstance();
                sold_invoice data = Get(_id);
                data.MONEY_WITHOUT_ADDEDD = MONEY_WITHOUT_ADDEDD;
                data.MONEY_TAX = MONEY_TAX;
                data.MONEY_STAMP = MONEY_STAMP;
                data.MONEY_TOTAL = MONEY_TOTAL;
                _db.SaveChanges();
            }
            catch (Exception e) { log(e.Message); }
        }
        //----------------------------------------------------------------------------------------------------------------
        static void log(string _data, string _type = "error")
        {
            Console.WriteLine("\n----------------------------------\n" + _type+":"+_data + "\n----------------------------------\n");
        }
        private static int GetPageSize()
        {
            return Config_CV.load().software.pageSizeSearch;
        }
        private static string SkipTake<T>(ref int page_this, ref IQueryable<T> _query)
        {
            int page_max_size = GetPageSize();
            int _rows_all = _query.Count() - 1;
            int _page_count = (_rows_all / page_max_size);
            if (page_this < 0) page_this = 0;
            if (page_this > _page_count - 1) page_this = _page_count;
            _query = _query.Skip(page_this * page_max_size).Take(page_max_size);

            return string.Format("({0} / {1}) |{2}|", page_this + 1, _page_count + 1, _rows_all + 1);
        }
        private static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> query, string propertyName)
        {
            var entityType = typeof(TSource);
            var propertyInfo = entityType.GetProperty(propertyName);
            ParameterExpression arg = Expression.Parameter(entityType, "x");
            MemberExpression property = Expression.Property(arg, propertyName);
            var selector = Expression.Lambda(property, new ParameterExpression[] { arg });
            var enumarableType = typeof(System.Linq.Queryable);
            var method = enumarableType.GetMethods().Where(m => m.Name == "OrderBy" && m.IsGenericMethodDefinition).Where(m =>
            {
                var parameters = m.GetParameters().ToList();
                return parameters.Count == 2;
            }).Single();
            MethodInfo genericMethod = method.MakeGenericMethod(entityType, propertyInfo.PropertyType);
            var newQuery = (IOrderedQueryable<TSource>)genericMethod.Invoke(genericMethod, new object[] { query, selector });
            return newQuery;
        }
        private static IOrderedQueryable<TSource> OrderByDescending<TSource>(this IQueryable<TSource> query, string propertyName)
        {
            var entityType = typeof(TSource);
            var propertyInfo = entityType.GetProperty(propertyName);
            ParameterExpression arg = Expression.Parameter(entityType, "x");
            MemberExpression property = Expression.Property(arg, propertyName);
            var selector = Expression.Lambda(property, new ParameterExpression[] { arg });
            var enumarableType = typeof(System.Linq.Queryable);
            var method = enumarableType.GetMethods().Where(m => m.Name == "OrderByDescending" && m.IsGenericMethodDefinition).Where(m =>
            {
                var parameters = m.GetParameters().ToList();
                return parameters.Count == 2;
            }).Single();
            MethodInfo genericMethod = method.MakeGenericMethod(entityType, propertyInfo.PropertyType);
            var newQuery = (IOrderedQueryable<TSource>)genericMethod.Invoke(genericMethod, new object[] { query, selector });
            return newQuery;
        }
        //----------------------------------------------------------------------------------------------------------------
    }
}
