using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AAASamples.Models
{
    public class AAADbContext : IdentityDbContext<IdentityUser>
    {
        public AAADbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
