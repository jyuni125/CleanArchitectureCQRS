using CleanArchitecture.Domain.Contracts.IRepositories;
using CleanArchitecture.Domain.Contracts.IServices.IServices;
using CleanArchitecture.Domain.Contracts.IServices.IServicesFacades;
using CleanArchitecture.Domain.Contracts.ITokens;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Databases.Context;
using CleanArchitecture.Infrastructure.Databases.Repositories;
using CleanArchitecture.Infrastructure.Tokens;
using CleanArchitecture.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
            services.AddTransient<ITokenService, TokenServices>();


            //for authentication
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
            {
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                    ValidateLifetime = false,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    ClockSkew = TimeSpan.Zero
                };
            });

        }
        
    }
}
