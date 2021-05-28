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
    public static class TableProducts_CD
    {
        public static IQueryable<product> search(string _value, ref int _this_page, out string _data_out)
        {
            IQueryable<product> query = null;
            var _db = Entities.GetInstance();
            query = _db.product.Where(c => (c.NAME.ToLower().Contains(_value)) || (c.DESCRIPTION.ToLower().Contains(_value))).OrderBy("NAME"); ;
            _data_out = SkipTake(ref _this_page, ref query);
            return query;
        }
        //----------------------------------------------------------------------------------------------------------------
        public static product Get(long p_id)
        {
            var _db = Entities.GetInstance();
            return _db.product.Single(c => c.ID == p_id);
        }
        //----------------------------------------------------------------------------------------------------------------
        public static void Add(product _product)
        {
            var _db = Entities.GetInstance();
            _db.product.Add(_product);
            _db.SaveChanges();
        }
        //----------------------------------------------------------------------------------------------------------------
        public static void Edit(product _product)
        {
            var _db = Entities.GetInstance();
            var o = Get(_product.ID);
            o.ID_CATEGORY = _product.ID_CATEGORY;
            o.ID_UNITE = _product.ID_UNITE;

            o.NAME = _product.NAME;
            o.DESCRIPTION = _product.DESCRIPTION;
            o.CODE = _product.CODE;

            o.TAX_PERCE = _product.TAX_PERCE;
            o.STAMP = _product.STAMP;
            o.MONEY_PURCHASE = _product.MONEY_PURCHASE;
            o.MONEY_SELLING = _product.MONEY_SELLING;
            o.MONEY_SELLING_MIN = _product.MONEY_SELLING_MIN;
            _db.SaveChanges();
        }
        //----------------------------------------------------------------------------------------------------------------
        public static void Delete(long p_id)
        {
            var _db = Entities.GetInstance();
            _db.product.Remove(_db.product.Single(c => c.ID == p_id));
            _db.SaveChanges();
        }
        //----------------------------------------------------------------------------------------------------------------
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
