using MySql.Data.MySqlClient;
using Stock.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Stock.Dataset.Model
{
    public partial class Entities : DbContext
    {
        //********************************************************
        #region init
        private Entities() // Entities = Constructor of calss model
               : base(new MySqlConnection(Configs.load().config_db.getConnectionString()), true)
        { }
        public Entities(string connString)
                : base(new MySqlConnection(connString), true)
        { }
        private static Entities Instance;
        public static object mutex = new object();
        public static Entities GetInstance()
        {
            lock (mutex)
            {
                if (Instance == null)
                {
                    Instance = new Entities();
                    if (!Instance.Database.Exists())
                    {
                        Console.WriteLine("creatre database");
                        Console.Beep();
                        throw new Exception("Error new Entities ");
                    }
                    Console.Beep();
                }
                return Instance;
            }
        }
        public static void recreate()
        {
            Instance.Dispose();
            Instance = null;
        }

        #endregion
    }
}

// in model contain date time or date stamp
// [Column(TypeName = "timestamp")]
// //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] <<  delete this do error update delete insert
// public DateTime? DATE_CREATED { get; set; }

//********************************************************
//protected override void OnModelCreating(DbModelBuilder modelBuilder)
//{
//base.OnModelCreating(modelBuilder);

//modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
//modelBuilder.Entity<user>().ToTable("user");
//modelBuilder.Entity<permission>().ToTable("permission");
//modelBuilder.Entity<role>().ToTable("role");
//modelBuilder.Entity<role_permission>().ToTable("role_permission");
//modelBuilder.Entity<product>().ToTable("product");
//modelBuilder.Entity<category>().ToTable("category");
//modelBuilder.Entity<sold_product>().ToTable("sold_product");
//modelBuilder.Entity<sold_invoice>().ToTable("sold_invoice");
//modelBuilder.Entity<stock>().ToTable("stock");
//}