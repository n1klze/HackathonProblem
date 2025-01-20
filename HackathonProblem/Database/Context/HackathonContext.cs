using HackathonProblem.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace HackathonProblem.Database.Context;

public class HackathonContext : DbContext
{
    public DbSet<HackathonDto> Hackathons { get; set; }
    public DbSet<TeamLeadDto> TeamLeads { get; set; }
    public DbSet<JuniorDto> Juniors { get; set; }
    public DbSet<TeamLeadWishlistDto> TeamLeadsWishlists { get; set; }
    public DbSet<JuniorWishlistDto> JuniorsWishlists { get; set; }
    public DbSet<TeamDto> Teams { get; set; }

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
            .Entity<HackathonDto>()
            .HasMany(h => h.Juniors)
            .WithMany()
            .UsingEntity("HackathonsJuniors");

        modelBuilder
            .Entity<HackathonDto>()
            .HasMany(h => h.TeamLeads)
            .WithMany()
            .UsingEntity("HackathonsTeamLeads");

        modelBuilder.Entity<HackathonDto>().HasMany(h => h.JuniorsWishlists).WithOne();

        modelBuilder.Entity<HackathonDto>().HasMany(h => h.TeamLeadsWishlists).WithOne();

        modelBuilder.Entity<HackathonDto>().HasMany(h => h.Teams).WithOne();
    }
}
