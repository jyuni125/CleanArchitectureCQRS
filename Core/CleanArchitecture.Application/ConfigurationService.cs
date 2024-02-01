using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Commons.Behaviour;
using CleanArchitecture.Application.Commons.Rules;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(c =>
            {
                c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                //validation
                c.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });


            //for rules
            services.AddTransient<AuthRules>();
            services.AddRulesFromAssemblyContaining(Assembly.GetEntryAssembly(), typeof(BaseRules));


            return services;
        }

        public static IServiceCollection AddRulesFromAssemblyContaining(this IServiceCollection services,
                                                                        Assembly assembly,
                                                                        Type type )
        {

            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();

            foreach(var item in types)
            {
                services.AddTransient(item);
            }

            return services;
        }
    }
}
