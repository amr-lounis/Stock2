using Stock.Controllers;
using Stock.Dataset.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stock.ControllerSQL
{
    public static class TableUsers_CD
    {
        public static IQueryable<user> search(string _value, ref int _this_page,out string _data_out)
        {
            IQueryable<user> query = null;
            try
            {
                var _db = Entities.GetInstance();
                query = _db.user.Where(c => (c.NAME.ToLower().Contains(_value)) ||(c.DESCRIPTION.ToLower().Contains(_value)) ).OrderBy("NAME"); ;
                _data_out = SkipTake(ref _this_page,ref query);
                return query;
            }
            catch (Exception e) { log(e.Message); _data_out = "ERROR"; return null; }
        }
        //----------------------------------------------------------------------------------------------------------------
        public static user Get(long p_id)
        {
            try
            {
                var _db = Entities.GetInstance();
                return _db.user.Single(c => c.ID == p_id);
            }
            catch (Exception) { return null; }
        }
        //----------------------------------------------------------------------------------------------------------------
        public static bool Add(user _user)
        {
            try
            {
                var _db = Entities.GetInstance();
                _db.user.Add(_user);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e) { log(e.Message); return false; }
        }
        //----------------------------------------------------------------------------------------------------------------
        public static bool Edit(user _user)
        {
            try
            {
                var _db = Entities.GetInstance();
                var o = Get(_user.ID);
                o.ID_ROLE = _user.ID_ROLE;
                o.NAME = _user.NAME;
                o.PASSWORD = _user.PASSWORD;
                o.GENDER = _user.GENDER;
                o.ACTIVITY = _user.ACTIVITY;
                o.NRC = _user.NRC;
                o.NIF = _user.NIF;
                o.ADDRESS = _user.ADDRESS;
                o.CITY = _user.CITY;
                o.COUNTRY = _user.COUNTRY;
                o.PHONE = _user.PHONE;
                o.FAX = _user.FAX;
                o.WEBSITE = _user.WEBSITE;
                o.EMAIL = _user.EMAIL;
                o.DESCRIPTION = _user.DESCRIPTION;
                _db.SaveChanges();
                return true;
            }
            catch (Exception e) { log(e.Message); return false; }
        }
        //----------------------------------------------------------------------------------------------------------------
        public static bool Delete(long p_id)
        {
            try
            {
                var _db = Entities.GetInstance();
                _db.user.Remove(_db.user.Single(c => c.ID == p_id));
                _db.SaveChanges();
                return true;
            }
            catch (Exception e) { log(e.Message); return false; }
        }
        //----------------------------------------------------------------------------------------------------------------
        private static bool IsExistName(string p_string)
        {
            try
            {
                var _db = Entities.GetInstance();
                return _db.user.Any(o => o.NAME == p_string);
            }
            catch (Exception) { return false; }
        }
        //----------------------------------------------------------------------------------------------------------------
        static void log(string _data, string _type = "error")
        {
            Console.WriteLine("\n----------------------------------\n" + _type + ":" + _data + "\n----------------------------------\n");
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
