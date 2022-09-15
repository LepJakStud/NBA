namespace NBA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Matchup")]
    public partial class Matchup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Matchup()
        {
            MatchupDetail = new HashSet<MatchupDetail>();
            MatchupLog = new HashSet<MatchupLog>();
        }

        public int MatchupId { get; set; }

        public int SeasonId { get; set; }

        public int MatchupTypeId { get; set; }

        public int Team_Away { get; set; }

        public int Team_Home { get; set; }

        public DateTime Starttime { get; set; }

        public int Team_Away_Score { get; set; }

        public int Team_Home_Score { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        public int Status { get; set; }

        [StringLength(50)]
        public string CurrentQuarter { get; set; }

        public virtual MatchupType MatchupType { get; set; }

        public virtual Season Season { get; set; }

        public virtual Team Team { get; set; }

        public virtual Team Team1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatchupDetail> MatchupDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatchupLog> MatchupLog { get; set; }
    }
}
