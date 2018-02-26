using Microsoft.EntityFrameworkCore;

namespace TaskAdministratorAPI.Models
{
    public class TaskAdministratorAPIContext : DbContext
    {
        public TaskAdministratorAPIContext (DbContextOptions<TaskAdministratorAPIContext> options)
            : base(options)
        {
        }

        public DbSet<TaskAdministratorAPI.Models.Tasks> Tasks { get; set; }

        public DbSet<TaskAdministratorAPI.Models.Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignments>().HasKey(a => new { a.TaskID, a.UserID });
        }

        public DbSet<TaskAdministratorAPI.Models.Assignments> Assignments { get; set; }
    }
}
