namespace NBA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Player")]
    public partial class Player
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Player()
        {
            MatchupLog = new HashSet<MatchupLog>();
            PlayerInTeam = new HashSet<PlayerInTeam>();
            PlayerStatistics = new HashSet<PlayerStatistics>();
        }

        public int PlayerId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int PositionId { get; set; }

        [Column(TypeName = "date")]
        public DateTime JoinYear { get; set; }

        public decimal Height { get; set; }

        public decimal Weight { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(50)]
        public string College { get; set; }

        [Required]
        [StringLength(3)]
        public string CountryCode { get; set; }

        [StringLength(50)]
        public string Img { get; set; }

        public bool IsRetirment { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RetirementTime { get; set; }

        public virtual Country Country { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatchupLog> MatchupLog { get; set; }

        public virtual Position Position { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlayerInTeam> PlayerInTeam { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlayerStatistics> PlayerStatistics { get; set; }
    }
}
