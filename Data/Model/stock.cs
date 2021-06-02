namespace Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("stock.stock")]
    public partial class stock
    {
        [Column(TypeName = "uint")]
        public long ID { get; set; }

        [Column(TypeName = "uint")]
        public long? ID_PRODUCT { get; set; }

        [StringLength(25)]
        public string NAME { get; set; }

        [StringLength(255)]
        public string DESCRIPTION { get; set; }

        public double? QUANTITY { get; set; }

        public double? QUANTITY_MIN { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime? DATE_PRODUCTION { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime? DATE_PURCHASE { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime? DATE_EXPIRATION { get; set; }
    }
}
