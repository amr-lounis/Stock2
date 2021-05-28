using Stock.ControllerSQL;
using Stock.Dataset.Model;
using Stock.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Windows.Media.Imaging;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Stock.Utils;

namespace Stock.Controllers
{
    public class TableUsers_CV : ITableUsers
    {
        //-------------------------------------------------------------------------------------
        public user get(long _id)
        {
            return TableUsers_CD.Get(_id) ?? new user {ID = 0,NAME = "default",DESCRIPTION="" };
        }
        //-------------------------------------------------------------------------------------
        public List<user> search(string _value, ref int _this_page, out string _data_out)
        {
            try
            {
                var query = TableUsers_CD.search(_value, ref _this_page, out _data_out);
                return query.ToList();
            }
            catch (Exception){ _data_out = "ERROR"; return new List<user>(); }
        }
        //-------------------------------------------------------------------------------------
        public void add(user _user)
        {
             TableUsers_CD.Add(_user);
        }
        //-------------------------------------------------------------------------------------
        public void edit(user _user)
        {
            TableUsers_CD.Edit(_user);
        }
        //-------------------------------------------------------------------------------------
        public void delete(long _id)
        {
            TableUsers_CD.Delete(_id);
        }
        //-------------------------------------------------------------------------------------
        public BitmapImage getImage(long _id)
        {
            try
            {
                var path = Path.Combine(Config_CV.dir_images_users(), string.Format("{0}.png", _id));
                return Helper.BitmapImageReadFile(path, 300, 300);
            }
            catch (Exception) { return new BitmapImage(new Uri("/assets/images/user.png", UriKind.Relative)); }
        }
        //----------------------------------------------------------------------------------------------------------------
        public void setImage(BitmapImage _image, long _id)
        {
            var path = Path.Combine(Config_CV.dir_images_users(), string.Format("{0}.png", _id));
            if (_image == null) { if (File.Exists(path)) { File.Delete(path); } }
            else { Helper.BitmapImageWriteFile(_image, path); }
        }
    }
}
