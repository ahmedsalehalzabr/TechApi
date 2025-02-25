﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TechApi.Model;

namespace TechApi.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) 
        {

        }
       public DbSet<Inventory> inventories {  get; set; } 
    }
}
