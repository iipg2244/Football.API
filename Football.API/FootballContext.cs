using Football.API.Models;
using Football.API.Models.Others;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Football.API
{
    public partial class FootballContext : DbContext
    {
        private readonly string _connectionString = Methods.GetConfigurationRoot().GetConnectionString("DefaultConnection");

        public FootballContext()
        {
        }

        public FootballContext(DbContextOptions<FootballContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Manager> Managers { get; set; } = null!;
        public virtual DbSet<Player> Players { get; set; } = null!;
        public virtual DbSet<Referee> Referees { get; set; } = null!;
        public virtual DbSet<PlayerMatchAway> PlayerAways { get; set; } = null!;
        public virtual DbSet<PlayerMatchHouse> PlayerHouses { get; set; } = null!;
        public virtual DbSet<Match> Matches { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired()
                    .HasDefaultValue("");

                entity.Property(e => e.YellowCard)
                    .HasDefaultValue(0);

                entity.Property(e => e.RedCard)
                   .HasDefaultValue(0);

            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired()
                    .HasDefaultValue("");

                entity.Property(e => e.YellowCard)
                    .HasDefaultValue(0);

                entity.Property(e => e.RedCard)
                   .HasDefaultValue(0);

                entity.Property(e => e.MinutesPlayed)
                    .HasDefaultValue(0);

            });

            modelBuilder.Entity<Referee>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired()
                    .HasDefaultValue("");

                entity.Property(e => e.MinutesPlayed)
                    .HasDefaultValue(0);

            });

            modelBuilder.Entity<PlayerMatchAway>(entity =>
            {
                entity.HasKey(e => new { e.MatchId, e.PlayerId });

                entity.HasOne(d => d.Player)
                     .WithMany(p => p.MatchesAway)
                     .HasForeignKey(d => d.PlayerId)
                     .HasConstraintName("FK_AwayPlayerId");

            });

            modelBuilder.Entity<PlayerMatchHouse>(entity =>
            {
                entity.HasKey(e => new { e.MatchId, e.PlayerId });

                entity.HasOne(d => d.Player)
                     .WithMany(p => p.MatchesHouse)
                     .HasForeignKey(d => d.PlayerId)
                     .HasConstraintName("FK_HousePlayerId");

            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.HouseManagerId);

                entity.HasOne(d => d.HouseManager)
                    .WithMany(p => p.HouseMatches)
                    .HasForeignKey(d => d.HouseManagerId)
                    .HasPrincipalKey(p => p.Id)
                    .HasConstraintName("FK_HouseManagerId");

                entity.Property(e => e.AwayManagerId);

                entity.HasOne(d => d.AwayManager)
                  .WithMany(p => p.AwayMatches)
                  .HasForeignKey(d => d.AwayManagerId).OnDelete(DeleteBehavior.NoAction)
                  .HasPrincipalKey(p => p.Id)
                  .HasConstraintName("FK_AwayManagerId");

                entity.Property(e => e.RefereeId);

                entity.HasOne(d => d.Referee)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.RefereeId)
                    .HasConstraintName("FK_RefereeId");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
