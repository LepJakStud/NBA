namespace NBA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MatchupDetail")]
    public partial class MatchupDetail
    {
        public int Id { get; set; }

        public int MatchupId { get; set; }

        public int Quarter { get; set; }

        public int Team_Away_Score { get; set; }

        public int Team_Home_Score { get; set; }

        public virtual Matchup Matchup { get; set; }
    }
}
