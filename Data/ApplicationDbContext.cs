using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KnowledgeWebApp.Models;

namespace KnowledgeWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<KnowledgeWebApp.Models.knowledge> knowledge { get; set; } = default!;
    }
}
