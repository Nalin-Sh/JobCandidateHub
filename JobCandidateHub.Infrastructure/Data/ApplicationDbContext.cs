using Microsoft.EntityFrameworkCore;
namespace JobCandidateHub.Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}


