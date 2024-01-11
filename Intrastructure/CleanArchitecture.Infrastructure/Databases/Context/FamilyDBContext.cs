using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Databases.Context
{
    public class FamilyDBContext : DbContext
    {

        public FamilyDBContext() { }

        public FamilyDBContext(DbContextOptions<FamilyDBContext> options) : base(options) { }

        public DbSet<Family> Families { get; set; }



        //for applying configuration from assembly
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
