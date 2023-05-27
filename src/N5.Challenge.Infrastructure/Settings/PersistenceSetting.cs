namespace N5.Challenge.Infrastructure.Settings
{
    internal class PersistenceSetting
    {
        public const string SettingName = "PersistenceSettings";

        public bool MigrateOnStartup { get; set; }
        public bool UseMsSql { get; set; }
        public bool UseSqLite { get; set; }
        public DbConnectionStrings ConnectionStrings { get; set; } = default!;

        public class DbConnectionStrings
        {
            public string MsSql { get; set; } = "";
            public string Sqlite { get; set; } = "";
        }
    }
}