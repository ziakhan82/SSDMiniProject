using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSDMiniProject
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Credential> Credentials { get; set; }

     
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure your SQL Server connection string here/ in real life i will be in appsettings.json file
            optionsBuilder.UseSqlServer("Server=LAPTOP-8U7GLB0P;Database=PasswordManager;Trusted_connection=True;TrustServerCertificate=true");
        }
    }
}

