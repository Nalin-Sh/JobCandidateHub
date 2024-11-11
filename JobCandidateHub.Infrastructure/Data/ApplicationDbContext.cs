using JobCandidateHub.Domain.Entities.API;
using Microsoft.EntityFrameworkCore;
namespace JobCandidateHub.Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Candidates> Candidates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidates>()
            .HasIndex(c => c.Email)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}


