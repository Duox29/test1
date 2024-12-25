namespace test1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product Specifications")]
    public partial class Product_Specification
    {
        [Key]
        public int specification_id { get; set; }

        public int product_id { get; set; }

        [StringLength(50)]
        public string os { get; set; }

        [StringLength(50)]
        public string cpu { get; set; }

        [StringLength(50)]
        public string gpu { get; set; }

        [StringLength(50)]
        public string ram { get; set; }

        [StringLength(50)]
        public string rom { get; set; }

        [StringLength(50)]
        public string camera { get; set; }

        [StringLength(50)]
        public string screen { get; set; }

        [StringLength(50)]
        public string battery { get; set; }

        [StringLength(50)]
        public string charger { get; set; }

        public virtual Product Product { get; set; }
    }
}
