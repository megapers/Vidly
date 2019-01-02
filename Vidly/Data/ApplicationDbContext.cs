using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vidly.Models;

namespace Vidly.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    { 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }   

        public DbSet<Customer> Customers{ get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Rental> Rental { get; set; }

        //Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Customer>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder
                .Entity<Customer>()
                .Property(c => c.BirthDate)
                .IsRequired(false);

            modelBuilder
                .Entity<Movie>()
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder
                .Entity<Movie>()
                .Property(m => m.GenreId)
                .IsRequired();

            modelBuilder
                .Entity<Genre>()
                .Property(g => g.Id)
                .IsRequired()
                .UseSqlServerIdentityColumn();

        }
    }
}
