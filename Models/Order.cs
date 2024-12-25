namespace test1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Order_Items = new HashSet<Order_Item>();
        }

        [Key]
        public int order_id { get; set; }

        public int user_id { get; set; }

        public DateTime? order_date { get; set; }

        [StringLength(20)]
        public string status { get; set; }

        public string shipping_address { get; set; }

        [StringLength(50)]
        public string payment_method { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Item> Order_Items { get; set; }

        public virtual User User { get; set; }
    }
}
