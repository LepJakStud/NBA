namespace NBA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pictures
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Image { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public int NumberOfLike { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
