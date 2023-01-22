using Microsoft.EntityFrameworkCore;

namespace OpenXrm.Core.Database.Infrastructure
{
    public class CoreContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql($"Host=localhost:5432;Database=openXrm;Username=postgres; Password=hallo; Include Error Detail=true");
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
        }
    }
}
