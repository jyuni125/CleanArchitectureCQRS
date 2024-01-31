using CleanArchitecture.Domain.Contracts.IRepositories;
using CleanArchitecture.Domain.Contracts.IServices.IServices;
using CleanArchitecture.Domain.Contracts.IServices.IServicesFacades;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Databases.Context;
using CleanArchitecture.Infrastructure.Databases.Repositories;
using CleanArchitecture.Infrastructure.Tokens;
using CleanArchitecture.Services.Services;
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
            services.AddScoped(typeof(IFamilyRepository<>), typeof(FamilyRepository<>));
            services.AddScoped(typeof(IStoredFamilyRepository<>), typeof(StoredFamilyRepository<>));
            services.AddScoped(typeof(IFamilyServices<>), typeof(FamilyServices<>));
            services.AddScoped<IRestCall,RestCall>();
            services.AddScoped<IEmailSender, EmailSender>();
            // services.AddScoped<IUnitOfWork, UnitOfWork>();




            //for token
            services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 2;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
                opt.SignIn.RequireConfirmedEmail = false;
            })
                .AddRoles<Role>()
                .AddEntityFrameworkStores<FamilyDBContext>();




            //for token settings
            services.Configure<TokenSettings>(configuration.GetSection("JWT"));

        }
        
    }
}
