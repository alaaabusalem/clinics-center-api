using clinics_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace clinics_api.Data
{
    public class ClinicsDbContext: IdentityDbContext<ApplicationUser>
    {
        public ClinicsDbContext(DbContextOptions options):base(options)
        {
            
        }
    }
}
