namespace Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("stock.sales")]
    public partial class sales
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int order_id { get; set; }

        public int? customer_id { get; set; }

        [StringLength(50)]
        public string item_type { get; set; }

        [StringLength(50)]
        public string sales_channel { get; set; }

        [StringLength(1)]
        public string order_priority { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime? order_date { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime? ship_date { get; set; }

        public double? units_sold { get; set; }

        public double? unit_price { get; set; }

        public double? unit_cost { get; set; }

        public double? total_revenue { get; set; }

        public double? total_cost { get; set; }
    }
}
