using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TestingTask.Models
{
    public class Taskk
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int status_id { get; set; }
    }

    public class Status
    {
        [Key]
        public int status_id { get; set; }
        public string status { get; set; }
    }

    public class TaskksContext : DbContext
    {
        public DbSet<Taskk> Taskks { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public TaskksContext(DbContextOptions<TaskksContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>();
        }
    }
}
