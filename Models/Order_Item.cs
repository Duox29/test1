namespace test1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order Items")]
    public partial class Order_Item
    {
        [Key]
        public int order_item_id { get; set; }

        public int order_id { get; set; }

        public int product_id { get; set; }

        public int option_id { get; set; }

        public int quantity { get; set; }

        public virtual Option Option { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
