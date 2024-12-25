namespace test1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cart_Items
    {
        [Key]
        public int cart_item_id { get; set; }

        public int cart_id { get; set; }

        public int? product_id { get; set; }

        public int? option_id { get; set; }

        public int? quantity { get; set; }

        public DateTime? added_at { get; set; }

        public virtual Cart Cart { get; set; }

        public virtual Option Option { get; set; }

        public virtual Product Product { get; set; }
    }
}
