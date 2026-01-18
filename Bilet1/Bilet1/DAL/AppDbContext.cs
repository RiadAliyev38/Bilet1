using Bilet1.Models;
using Microsoft.EntityFrameworkCore;

namespace Bilet1.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Designation> Designations { get; set; }
    }
}
