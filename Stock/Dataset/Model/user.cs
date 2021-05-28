namespace Stock.Dataset.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("stock.user")]
    public partial class user
    {
        [Column(TypeName = "uint")]
        public long ID { get; set; }

        [Column(TypeName = "uint")]
        public long? ID_ROLE { get; set; }

        [StringLength(25)]
        public string NAME { get; set; }

        [StringLength(255)]
        public string DESCRIPTION { get; set; }

        [StringLength(25)]
        public string PASSWORD { get; set; }

        [StringLength(25)]
        public string GENDER { get; set; }

        [StringLength(25)]
        public string ACTIVITY { get; set; }

        [StringLength(25)]
        public string NRC { get; set; }

        [StringLength(25)]
        public string NIF { get; set; }

        [StringLength(25)]
        public string ADDRESS { get; set; }

        [StringLength(25)]
        public string CITY { get; set; }

        [StringLength(25)]
        public string COUNTRY { get; set; }

        [StringLength(25)]
        public string PHONE { get; set; }

        [StringLength(25)]
        public string FAX { get; set; }

        [StringLength(25)]
        public string WEBSITE { get; set; }

        [StringLength(25)]
        public string EMAIL { get; set; }
    }
}
