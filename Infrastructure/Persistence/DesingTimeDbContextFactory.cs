using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Persistence
{
    // Basit design-time factory: EF CLI bu factory'i kullanarak DbContext oluşturacak.
    // Öncelikle environment variable "SOCIALLIB_CONN" aranır. Bulunmazsa fallback kullanılır.
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // 1) Öncelik: environment variable (daha güvenli)
            var envConn = Environment.GetEnvironmentVariable("SOCIALLIB_CONN");
            var conn = !string.IsNullOrWhiteSpace(envConn)
                ? envConn
                : "Host=localhost;Port=5432;Database=SocialLibDB;Username=postgres;Password=mynewpass";

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(conn,
                npgsqlOptions => { /* optional: npgsqlOptions.MigrationsAssembly("Infrastructure"); */ });

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
