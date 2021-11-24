﻿using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data
{
    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext()
        {
        }
        public FootballBettingContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<Bet> Bets { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayerStatistic> PlayerStatistics { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Town> Towns { get; set; }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.CONNECTION_STRING);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PlayerStatistic>().HasKey(ps => new { ps.PlayerId, ps.GameId });

            modelBuilder.Entity<Team>().HasOne(e => e.PrimaryKitColor)
                                       .WithMany(e => e.PrimaryKitTeams)
                                       .HasForeignKey(e => e.PrimaryKitColorId)
                                       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>().HasOne(e => e.SecondaryKitColor)
                                       .WithMany(e => e.SecondaryKitTeams)
                                       .HasForeignKey(e => e.SecondaryKitColorId)
                                       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Game>().HasOne(e => e.HomeTeam)
                                       .WithMany(e => e.HomeGames)
                                       .HasForeignKey(e => e.HomeTeamId)
                                       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Game>().HasOne(e => e.AwayTeam)
                                       .WithMany(e => e.AwayGames)
                                       .HasForeignKey(e => e.AwayTeamId)
                                       .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
