using Microsoft.EntityFrameworkCore;
using System;

namespace Agitur.EFDataAccess
{
    public class AgiturDbContext : DbContext
    {
        public AgiturDbContext(DbContextOptions<AgiturDbContext> options) : base(options)
        {

        }
    }
}
