namespace Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("stock.role")]
    public partial class role
    {
        [Column(TypeName = "uint")]
        public long ID { get; set; }

        [StringLength(25)]
        public string NAME { get; set; }

        [StringLength(255)]
        public string DESCRIPTION { get; set; }
    }
}
