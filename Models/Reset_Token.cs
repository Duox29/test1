namespace test1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Reset Tokens")]
    public partial class Reset_Token
    {
        [Key]
        public int token_id { get; set; }

        public int user_id { get; set; }

        [Required]
        [StringLength(20)]
        public string token { get; set; }

        public DateTime? created_at { get; set; }

        public virtual User User { get; set; }
    }
}
