using Microsoft.EntityFrameworkCore;

namespace SSJPSAPI.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Employeejpe> Employeejpes { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Companyjpc> Companyjpcs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Postjob> Postjobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Super Admin"},
                new Role { Id = 2, Name = "Admin"},
                new Role { Id = 3, Name = "Company"},
                new Role { Id = 4, Name = "Employee"});

            modelBuilder.Entity<User>().HasData(
               new User { Id = 1, FullName = "Anil", Email = "anil@gmail.com", Mobile = "9876543210", Password = "Anil@123" },
               new User { Id = 2, FullName = "Vivek", Email = "vivek@gmail.com", Mobile = "8547963210", Password = "Vivek@123" },
               new User { Id = 3, FullName = "Rizwan", Email = "rizwan@gmail.com", Mobile = "8547963213", Password = "Rizwan@123" },
               new User { Id = 4, FullName = "Rohit", Email = "rohit@gmail.com", Mobile = "8569321441", Password = "Rohit@123" });

            modelBuilder.Entity<UserRole>().HasData(
              new UserRole { Id = 1, UserId = 1, RoleId = 1 },
              new UserRole { Id = 2, UserId = 2, RoleId = 2 },
              new UserRole { Id = 3, UserId = 3, RoleId = 3 },
              new UserRole { Id = 4, UserId =4, RoleId = 4 });
        }
    }

}
