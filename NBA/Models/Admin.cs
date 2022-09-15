namespace NBA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(6)]
        public string Jobnumber { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Passwords { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string Gender { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(1)]
        public string RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
