using Microsoft.EntityFrameworkCore;
using EmpStore.Models;
using System.Collections.Generic;

namespace EmpStore.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}

