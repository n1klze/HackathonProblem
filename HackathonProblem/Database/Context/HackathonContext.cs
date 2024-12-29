using HackathonProblem.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace HackathonProblem.Database.Context;

public class HackathonContext : DbContext
{
    public DbSet<Hackathon> Hackathons { get; set; }
    public DbSet<TeamLead> TeamLeads { get; set; }
    public DbSet<Junior> Juniors { get; set; }
    public DbSet<Wishlist> TeamLeadsWishlists { get; set; }
    public DbSet<Wishlist> JuniorsWishlists { get; set; }
    public DbSet<Team> Teams { get; set; }

    public HackathonContext()
    {
        Database.EnsureCreated();
    }

    public HackathonContext(DbContextOptions<HackathonContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Hackathon>()
            .HasMany(h => h.Juniors)
            .WithMany()
            .UsingEntity("HackathonsJuniors");

        modelBuilder
            .Entity<Hackathon>()
            .HasMany(h => h.TeamLeads)
            .WithMany()
            .UsingEntity("HackathonsTeamLeads");

        modelBuilder.Entity<Hackathon>().HasMany(h => h.JuniorsWishlists).WithOne();

        modelBuilder.Entity<Hackathon>().HasMany(h => h.TeamLeadsWishlists).WithOne();

        modelBuilder.Entity<Hackathon>().HasMany(h => h.Teams).WithOne();
    }
}
