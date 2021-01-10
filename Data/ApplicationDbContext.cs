using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuciuBogdan_7946.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor pentru db context cu options
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Models.Project> Project { get; set; }
        public DbSet<Models.Pages> Pages { get; set; }
    }
}
