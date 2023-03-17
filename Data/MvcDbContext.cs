using Microsoft.EntityFrameworkCore;
using MvcCrudApp.Models.Domain;

namespace MvcCrudApp.Data 
{
    public class MvcDbContext : DbContext
    {
        public MvcDbContext(DbContextOptions<MvcDbContext> options) : base(options)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
    }
}