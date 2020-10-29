using Agitur.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace Agitur.EFDataAccess
{
    public class AgiturDbContext : DbContext
    {
        public AgiturDbContext(DbContextOptions<AgiturDbContext> options) : base(options)
        {

        }
        public DbSet<Message> Messages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
