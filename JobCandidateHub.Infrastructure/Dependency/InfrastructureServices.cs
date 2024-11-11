using JobCandidateHub.Application.Interfaces.Repository;
using JobCandidateHub.Application.Interfaces.Services;
using JobCandidateHub.Infrastructure.Data;
using JobCandidateHub.Infrastructure.Implementation.Repository;
using JobCandidateHub.Infrastructure.Implementation.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobCandidateHub.Infrastructure.Dependency;

public static class InfrastructureService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString,
                              b => b.MigrationsAssembly("JobCandidateHub.Infrastructure")));

        services.AddScoped<IGenericRepository, GenericRepository>();
        services.AddScoped<ICandidateServices, CandidateServices>();



        return services;

       
    }

}
