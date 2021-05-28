using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace Stock.Dataset.Model
{
    public partial class Entities : DbContext
    {

        public virtual DbSet<category> category { get; set; }
        public virtual DbSet<permission> permission { get; set; }
        public virtual DbSet<product> product { get; set; }
        public virtual DbSet<role> role { get; set; }
        public virtual DbSet<role_permission> role_permission { get; set; }
        public virtual DbSet<sold_invoice> sold_invoice { get; set; }
        public virtual DbSet<sold_product> sold_product { get; set; }
        public virtual DbSet<stock> stock { get; set; }
        public virtual DbSet<unit> unit { get; set; }
        public virtual DbSet<user> user { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<user>().ToTable("user");
            modelBuilder.Entity<permission>().ToTable("permission");
            modelBuilder.Entity<role>().ToTable("role");
            modelBuilder.Entity<role_permission>().ToTable("role_permission");
            modelBuilder.Entity<product>().ToTable("product");
            modelBuilder.Entity<category>().ToTable("category");
            modelBuilder.Entity<sold_product>().ToTable("sold_product");
            modelBuilder.Entity<sold_invoice>().ToTable("sold_invoice");
            modelBuilder.Entity<stock>().ToTable("stock");

            modelBuilder.Entity<category>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<category>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<permission>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<permission>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.CODE)
                .IsUnicode(false);

            modelBuilder.Entity<role>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<role>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<sold_invoice>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<sold_product>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<sold_product>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<stock>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<stock>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<unit>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<unit>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.PASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.GENDER)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.ACTIVITY)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.NRC)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.NIF)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.ADDRESS)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.CITY)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.COUNTRY)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.PHONE)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.FAX)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.WEBSITE)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);
        }
    }
}
