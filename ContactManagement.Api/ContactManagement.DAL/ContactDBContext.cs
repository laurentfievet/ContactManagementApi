using ContactManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagement.DAL
{
    public partial class ContactDBContext : DbContext
    {
        //entities
        public DbSet<Adress> Adress { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Enterprise> Enterprise { get; set; }
        public DbSet<ContactEnterprise> ContactEnterprise { get; set; }
        public DbSet<EnterpriseAdress> EnterpriseAdress { get; set; }

        public ContactDBContext(DbContextOptions<ContactDBContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Initial Catalog=ContactManagement;Trusted_Connection=True;");

            }
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactEnterprise>(entity =>
            {
                entity.HasKey(e => new { e.ContactId, e.EnterpriseId });
            });
            modelBuilder.Entity<EnterpriseAdress>(entity =>
            {
                entity.HasKey(e => new { e.AdressId, e.EnterpriseId });
            });
        }
        
    }
}
