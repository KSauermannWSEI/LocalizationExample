using Microsoft.EntityFrameworkCore;
using MyBestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBestApp.Data
{
    public class LocalizationContext : DbContext
    {
        public DbSet<Translation> Translations { get; set; }
        public LocalizationContext(DbContextOptions<LocalizationContext> options) : base(options)
        {

        }
    }
}
