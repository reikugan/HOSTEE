//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HOSTEE.Models;

namespace HOSTEE
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Folder> Folders { get; set; }
    }

    public class ApplicationUser : IdentityUser
    {
        public ICollection<Note> Notes { get; set; }
    }
}
