using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Stock.Utils;

namespace Stock.Controllers
{
    public class Config_CV
    {
        //-----------------------------------------------------------------------------------------------
        public Config_CV()
        {
            config_db = new Config_db();
            config_db.db_type = "mysql";
            config_db.config_mysql = new Config_mysql();

            company = new Config_company();
            software = new Config_software();
        }
        //----------------------------------------------------------------------------------------------- I/O object
        public Config_db config_db;
        public Config_company company;
        public Config_software software;
        //----------------------------------------------------------------------------------------------- save object
        public static void save(Config_CV _Config)
        {
            CreateDirectories();
            try
            {
                Helper.WriteToBinaryFile(dir_home() + "Config_db.bin", _Config.config_db);
                Helper.WriteToBinaryFile(dir_home() + "Config_company.bin", _Config.company);
                Helper.WriteToBinaryFile(dir_home() + "Config_software.bin", _Config.software);
            }
            catch (Exception){}
        }
        //----------------------------------------------------------------------------------------------- load object
        public static Config_CV load()
        {
            CreateDirectories();
            try
            {
                Config_CV o = new Config_CV();
                o.config_db = Helper.ReadFromBinaryFile<Config_db>(dir_home() + "Config_db.bin");
                o.company = Helper.ReadFromBinaryFile<Config_company>(dir_home() + "Config_company.bin");
                o.software = Helper.ReadFromBinaryFile<Config_software>(dir_home() + "Config_software.bin");
                return o;
            }
            catch (Exception)
            {
                var o = new Config_CV();
                save(o);
                return o;
            }
        }
        //-----------------------------------------------------------------------------------------------
        public static string dir_home() { return Path.Combine(Directory.GetCurrentDirectory(), @"config\"); }
        public static string dir_backups() { return Path.Combine(dir_home(), @"backups\"); }
        public static string dir_Invoices() { return Path.Combine(dir_home(), @"invoices\"); }
        public static string dir_images_users() { return Path.Combine(dir_home(), @"images\users\"); }
        public static string dir_images_products() { return Path.Combine(dir_home(), @"images\products\"); }
        public static long thisUser_ID = 0;
        //----------------------------------------------------------------------------------------------- 
        private static void CreateDirectories(){
            try{
                string[] array_dir = { dir_home(), dir_backups(), dir_Invoices(), dir_images_users() , dir_images_products() };
                foreach (string dir in array_dir) if (!System.IO.Directory.Exists(dir)) System.IO.Directory.CreateDirectory(dir);
            }
            catch (Exception){}
        }
        //-----------------------------------------------------------------------------------------------
    }
    //----------------------------------------------------------------------------------------------- class Serializable
    #region class Serializable
    [Serializable]
    public class Config_db
    {
        public string db_type;
        public Config_mysql config_mysql;
        public string getConnectionString()
        {
            string ConnectionString = "";
            switch (this.db_type)
            {
                case "mysql": ConnectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", config_mysql.Host, config_mysql.Port, config_mysql.UserID, config_mysql.Password, config_mysql.db_name, config_mysql.sslM);break;
                default:break;
            }
            return ConnectionString;
        }
    }
    [Serializable]
    public class Config_mysql
    {
        public String Charset = "UTF8";
        public String Host = "localhost";
        public string db_name = "stock";
        public String UserID = "root";
        public String Password = "";
        public string sslM = "none";
        public int Port = 3306;
        public string getConnectionString()
        {
            return String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", Host, Port, UserID, Password, db_name, sslM);
        }
    }
    [Serializable]
    public class Config_company
    {
        public String NAME = "Company";
        public String ACTIVITY = "";
        public String NRC = "";
        public String NIF = "";
        public String DESCRIPTION = "";
        public String CREATE_DATE = "";
        public String ADDRESS = "";
        public String CITY = "";
        public String COUNTRY = "";
        public String PHONE = "";
        public String FAX = "";
        public String WEBSITE = "";
        public String EMAIL = "";
    }
    [Serializable]
    public class Config_software
    {
        public String activation_hash_add = "lounis_amar_inventory_management_system";
        public String language = "EN";
        public String currency = "DA";
        public int pageSizeSearch = 5;
    }
    #endregion
}
