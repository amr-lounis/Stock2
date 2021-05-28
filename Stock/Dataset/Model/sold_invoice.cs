namespace Stock.Dataset.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("stock.sold_invoice")]
    public partial class sold_invoice
    {
        public sold_invoice()
        {
            ID = 0;
            ID_USERS = 0;
            ID_CUSTOMERS = 0;
            DESCRIPTION = "";
            VALIDATION = 0;
            MONEY_WITHOUT_ADDEDD = 0;
            MONEY_TAX = 0;
            MONEY_STAMP = 0;
            MONEY_TOTAL = 0;
            MONEY_PAID = 0;
            MONEY_UNPAID = 0;

            DATE_CREATED = DateTime.Now;
            DATE_UPDATED = DateTime.Now;
        }
        [Column(TypeName = "uint")]
        public long ID { get; set; }

        [Column(TypeName = "uint")]
        public long? ID_USERS { get; set; }

        [Column(TypeName = "uint")]
        public long? ID_CUSTOMERS { get; set; }

        [StringLength(25)]
        public string DESCRIPTION { get; set; }

        [Column(TypeName = "uint")]
        public long? VALIDATION { get; set; }

        [Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? DATE_CREATED { get; set; }

        [Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? DATE_UPDATED { get; set; }

        public double? MONEY_WITHOUT_ADDEDD { get; set; }

        public double? MONEY_TAX { get; set; }

        public double? MONEY_STAMP { get; set; }

        public double? MONEY_TOTAL { get; set; }

        public double? MONEY_PAID { get; set; }

        public double? MONEY_UNPAID { get; set; }
    }
}
