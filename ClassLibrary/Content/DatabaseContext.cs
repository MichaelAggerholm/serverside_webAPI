using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Content
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<School> Schools { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EUDStudent> EUDStudents { get; set; }
        public DbSet<HTXStudent> HTXStudents { get; set; }
        public DbSet<Student> Students { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options,
                               IConfiguration configuration) : base(options)
        {
            this._configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<HTXStudent>().ToTable("HTXStudent");
            modelBuilder.Entity<EUDStudent>().ToTable("EUDStudent");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<School>().ToTable("School");

            //removing cascading from school to persons, so that when we delete a school
            //we don't delete or lose the people from the school  
            modelBuilder.Entity<Person>()
            .HasOne(s => s.School)
            .WithMany(p => p.Persons)
            .OnDelete(DeleteBehavior.NoAction);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString;

            connectionString = this._configuration.GetConnectionString("Web_API_DBConnectionString");
            
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString);
        }
    }
}
