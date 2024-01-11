using CleanArchitecture.Infrastructure.Databases.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure
{
    public static class EFConfiguration
    {
        
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FamilyDBContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));




            //for Dependency Injection
           // services.AddScoped(typeof(IFamilyRepository<>), typeof(FamilyRepository<>));

           // services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        
    }
}
