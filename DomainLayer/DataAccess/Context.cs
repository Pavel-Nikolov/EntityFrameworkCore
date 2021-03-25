using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.DataAccess
{
    public class Context : DbContext
    {
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<User> Users { get; set; }

        public Context():base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-E57GMEU\SQLEXPRESS;Database=CodeFirstEFCoreDb;Trusted_Connection=True;");
        }
    }
}
