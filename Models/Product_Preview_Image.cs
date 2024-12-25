namespace test1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product Preview Images")]
    public partial class Product_Preview_Image
    {
        [Key]
        public int imagepr_id { get; set; }

        public int product_id { get; set; }

        [StringLength(100)]
        public string image_url { get; set; }

        public virtual Product Product { get; set; }
    }
}
