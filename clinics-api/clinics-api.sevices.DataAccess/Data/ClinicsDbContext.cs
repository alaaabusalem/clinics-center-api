using clinics_api.services.Model.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace clinics_api.sevices.DataAccess.Data
{
    public class ClinicsDbContext : IdentityDbContext<ApplicationUser>
    {
        public ClinicsDbContext(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // doctor user relation
            modelBuilder.Entity<ApplicationUser>()
           .HasOne<Doctor>(user => user.doctor)
           .WithOne(Doctor => Doctor.ApplicationUser)
           .HasForeignKey<Doctor>(Doctor => Doctor.UserId);

            // Appointment user relation
            modelBuilder.Entity<ApplicationUser>()
            .HasMany<Appointment>(user => user.appointments)
            .WithOne(Appointment => Appointment.ApplicationUser)
            .HasForeignKey(Appointment => Appointment.UserId);

            // seed appointmment status
            modelBuilder.Entity<AppointmentStatus>().HasData(
                   new AppointmentStatus { AppointmentStatusId = 1, name= "booked", IsDisabal=false},
                   new AppointmentStatus { AppointmentStatusId = 2, name = "Disabled", IsDisabal = false },
                   new AppointmentStatus { AppointmentStatusId = 3, name = "done", IsDisabal = false },
                   new AppointmentStatus { AppointmentStatusId = 4, name = "Patient did'nt come", IsDisabal = false }
               );

            // seed Location
            modelBuilder.Entity<Location>().HasData(
                   new Location {  LocationId= 1, LocationName = "Amman", IsDisabal = false },
                   new Location { LocationId = 2, LocationName = "Amman,Alshmesani", IsDisabal = false },
                   new Location { LocationId = 3, LocationName = "Amman,Abdoun", IsDisabal = false },
                   new Location { LocationId = 5, LocationName = "Irbid", IsDisabal = false },
                   new Location { LocationId = 6, LocationName = "Zarqa", IsDisabal = false }

               );

            // seed Department
            modelBuilder.Entity<Department>().HasData(
                   new Department { DepartmentId = 1, DepartmentName = "Cardiology", IsDisabal = false },
                   new Department { DepartmentId = 2, DepartmentName = "Nephrologist", IsDisabal = false },
                   new Department { DepartmentId = 3, DepartmentName = "Orthopedics", IsDisabal = false },
                   new Department { DepartmentId = 5, DepartmentName = "Gynecology", IsDisabal = false },
                   new Department { DepartmentId = 6, DepartmentName = "Pediatrics", IsDisabal = false }

               );

            // seed roles
            SeedRole(modelBuilder, "Admin");
            SeedRole(modelBuilder, "Doctor");
            SeedRole(modelBuilder, "Patient");

            // seed admin
            var hasher = new PasswordHasher<ApplicationUser>();


            var admin = new ApplicationUser
            {
                Id = "1",
                UserName = "adminAlaa",
                NormalizedUserName = "ADMIN",
                Email = "adminalaa@hotmail.com",
                PhoneNumber = "1234567890",
                NormalizedEmail = "adminalaa@HOTMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false
            };
            admin.PasswordHash = hasher.HashPassword(admin, "Adminalaa@123");
            modelBuilder.Entity<ApplicationUser>().HasData(admin);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "admin",
                UserId = admin.Id
            });

        }

        private void SeedRole(ModelBuilder modelBuilder, string roleName)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };

            modelBuilder.Entity<IdentityRole>().HasData(role);
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentStatus> AppointmentStatuses { get; set; }
        public DbSet<Location> Locations { get; set; }




    }
}
