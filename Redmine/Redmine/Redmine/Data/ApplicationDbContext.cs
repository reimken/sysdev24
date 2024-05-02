using Microsoft.EntityFrameworkCore;
using Redmine.Models;
using Task = Redmine.Models.Task;

namespace Redmine.Data
{
    public class ApplicationDbContext : DbContext


    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Manager> Managers { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=SQLLiteDatabase.db");
        }
    }
}
