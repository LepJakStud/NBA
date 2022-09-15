namespace NBA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Team")]
    public partial class Team
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Team()
        {
            Matchup = new HashSet<Matchup>();
            Matchup1 = new HashSet<Matchup>();
            MatchupLog = new HashSet<MatchupLog>();
            PlayerInTeam = new HashSet<PlayerInTeam>();
            PlayerStatistics = new HashSet<PlayerStatistics>();
            PostSeason = new HashSet<PostSeason>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TeamId { get; set; }

        [Required]
        [StringLength(50)]
        public string TeamName { get; set; }

        public int DivisionId { get; set; }

        [Required]
        [StringLength(50)]
        public string Coach { get; set; }

        [Required]
        [StringLength(3)]
        public string Abbr { get; set; }

        [StringLength(100)]
        public string Stadium { get; set; }

        [Required]
        [StringLength(50)]
        public string Logo { get; set; }

        public decimal TotalSalary
        {
            get => PlayerInTeam.Where(p=>p.SeasonId==3).Sum(pin => pin.Salary);
        }        
        public double? Points
        {
            get {
                var a = NBAEntities.GetContext().Matchup.ToList().Where(m => m.SeasonId == 3 && (m.Team_Away == TeamId || m.Team_Home == TeamId));
                return Math.Round(PlayerStatistics.Where(p=>p.MatchupId==a.Where(m=>m.MatchupId==p.MatchupId).Select(m=>m.MatchupId).FirstOrDefault()).Select(p => p.Point).Average(), 2); }
        }
        public double? sumPoints
        {
            get => PlayerStatistics.Select(p => p.Point).Sum();
        }
        public double? Reboubds
        {
            get => Math.Round(PlayerStatistics.Select(p => p.Rebound).Average(), 2);
        }
        public double? sumReboubds
        {
            get => PlayerStatistics.Select(p => p.Rebound).Sum();
        }
        public double? Assists
        {
            get => Math.Round(PlayerStatistics.Select(p => p.Assist).Average(), 2);
        }
        public double? sumAssists
        {
            get => PlayerStatistics.Select(p => p.Assist).Sum();
        }
        public double? Steals
        {
            get => Math.Round(PlayerStatistics.Select(p => p.Steal).Average(), 2);
        }
        public double? sumSteals
        {
            get => PlayerStatistics.Select(p => p.Steal).Sum();
        }
        public double? Blocks
        {
            get => Math.Round(PlayerStatistics.Select(p => p.Block).Average(), 2);
        }
        public double? sumBlocks
        {
            get => PlayerStatistics.Select(p => p.Block).Sum();
        }
        public double? Turnovers
        {
            get => Math.Round(PlayerStatistics.Select(p => p.Turnover).Average(), 2);
        }
        public double? sumTurnovers
        {
            get => PlayerStatistics.Select(p => p.Turnover).Sum();
        }
        public double? Fouls
        {
            get => Math.Round(PlayerStatistics.Select(p => p.Foul).Average(), 2);
        }
        public double? sumFouls
        {
            get => PlayerStatistics.Select(p => p.Foul).Sum();
        }
        public virtual Division Division { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Matchup> Matchup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Matchup> Matchup1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatchupLog> MatchupLog { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlayerInTeam> PlayerInTeam { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlayerStatistics> PlayerStatistics { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostSeason> PostSeason { get; set; }
    }
}