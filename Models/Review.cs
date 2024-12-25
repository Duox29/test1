namespace test1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Review
    {
        [Key]
        public int review_id { get; set; }

        public int product_id { get; set; }

        public int user_id { get; set; }

        public int rating { get; set; }

        public string comment { get; set; }

        public DateTime? review_date { get; set; }

        public virtual Product Product { get; set; }

        public virtual User User { get; set; }
    }
}
