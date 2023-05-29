using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using N5.Challenge.Infrastructure.Seed;
using N5.Challenge.Infrastructure.Settings;

namespace N5.Challenge.Infrastructure.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {

            var persistenceSettings = new PersistenceSetting();
            config.GetSection(PersistenceSetting.SettingName).Bind(persistenceSettings);

            services.Configure<PersistenceSetting>(options => config.GetSection(PersistenceSetting.SettingName).Bind(options));

            services.AddDbContext<N5DBContext>(options =>
            {
                if (persistenceSettings.UseSqLite)
                {
                    var path = persistenceSettings.ConnectionStrings.Sqlite.Replace("|DataDirectory|", Directory.GetParent(Directory.GetCurrentDirectory()).FullName);
                    options.UseSqlite(path);
                }
                else if (persistenceSettings.UseMsSql)
                    options.UseSqlServer(persistenceSettings.ConnectionStrings.MsSql, optsql => optsql.CommandTimeout((int)TimeSpan.FromMinutes(2).TotalSeconds));
                else
                    options.UseSqlServer(config.GetConnectionString("CnxDbContext"), optsql => optsql.CommandTimeout((int)TimeSpan.FromMinutes(2).TotalSeconds));
            });
            services.AddScoped<ILogger, Logger<N5DBContext>>();
            services.AddTransient<N5DBContextSeeder>();
            services.Migrate<N5DBContext>(persistenceSettings);
            return services;
        }

        private static void Migrate<T>(this IServiceCollection services, PersistenceSetting settings) where T : DbContext
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<T>>();
            logger.LogInformation($"{new String('=', 80)}{Environment.NewLine} {settings.ConnectionString} {Environment.NewLine}{new String('=', 80)}");

            logger.LogWarning($"MigrationOnStartup: {(settings.MigrateOnStartup ? "enabled" : "disabled")}");
            if (settings.MigrateOnStartup)
            {
                services.MigrateDb<T>();
            }
        }

        public static void MigrateDb<T>(this IServiceCollection services) where T : DbContext
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<T>>();
            logger.LogWarning($"{new String('=', 80)}{Environment.NewLine}Performing migration for database {typeof(T).Name}{Environment.NewLine}{new String('=', 80)}");
            var dbContext = scope.ServiceProvider.GetRequiredService<T>();
            dbContext.Database.Migrate();

            var seeder = scope.ServiceProvider.GetRequiredService<N5DBContextSeeder>();
            seeder.Seed().GetAwaiter().GetResult();
        }
    }
}