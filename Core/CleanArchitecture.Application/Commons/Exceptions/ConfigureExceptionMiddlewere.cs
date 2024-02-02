using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Commons.Exceptions
{
    public static class ConfigureExceptionMiddlewere
    {
        public static void ConfigureExceptionHandlingMiddlewere(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddlewere>();
        }
    }
}
