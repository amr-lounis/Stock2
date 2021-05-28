using Stock.ControllerSQL;
using Stock.Dataset.Model;
using Stock.Interfaces;
using Stock.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Stock.Controllers
{
    public class TableProducts_CV : ITableProducts
    {
        //----------------------------------------------------------------------------------------------------------------
        public product get(long _id)
        {
            return TableProducts_CD.Get(_id);
        }
        //----------------------------------------------------------------------------------------------------------------
        public List<product> search(string _value, ref int _this_page, out string _data_out)
        {
            try
            {
                var query = TableProducts_CD.search(_value, ref _this_page, out _data_out);
                return query.ToList();
            }
            catch (Exception) { _data_out = "ERROR"; return new List<product>(); }
        }
        //----------------------------------------------------------------------------------------------------------------
        public void add(product _product)
        {
            _product.ID = 0;
            TableProducts_CD.Add(_product);
        }
        //----------------------------------------------------------------------------------------------------------------
        public void edit(product _product)
        {
            TableProducts_CD.Edit(_product);
        }
        //----------------------------------------------------------------------------------------------------------------
        public void delete(long _id)
        {
            TableProducts_CD.Delete(_id);
            setImage(null, _id);
        }
        //----------------------------------------------------------------------------------------------------------------
        public BitmapImage getImage(long _id)
        {
            try
            {
                var path = Path.Combine(Config_CV.dir_images_products(), string.Format("{0}.png", _id));
                return Helper.BitmapImageReadFile(path, 300, 300);
            }
            catch ( Exception) { return new BitmapImage(new Uri("/assets/images/openBox.png", UriKind.Relative));}
        }
        //----------------------------------------------------------------------------------------------------------------
        public void setImage(BitmapImage _image, long _id)
        {
            var path = Path.Combine(Config_CV.dir_images_products(), string.Format("{0}.png", _id));
            if (_image == null) { if (File.Exists(path)) { File.Delete(path); } }
            else { Helper.BitmapImageWriteFile(_image, path); }
        }
    }
}

