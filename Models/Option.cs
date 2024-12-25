namespace test1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Option
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Option()
        {
            Cart_Items = new HashSet<Cart_Items>();
            Order_Items = new HashSet<Order_Item>();
        }

        [Key]
        public int option_id { get; set; }

        public int product_id { get; set; }

        [StringLength(50)]
        public string description { get; set; }

        public decimal? price { get; set; }

        public int? stock { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart_Items> Cart_Items { get; set; }

        public virtual Product Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Item> Order_Items { get; set; }
    }
}
