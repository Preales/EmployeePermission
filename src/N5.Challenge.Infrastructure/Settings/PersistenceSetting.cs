namespace N5.Challenge.Infrastructure.Settings
{
    public class PersistenceSetting
    {
        public const string SettingName = "PersistenceSettings";

        public bool MigrateOnStartup { get; set; }
        public bool UseMsSql { get; set; }
        public bool UseSqLite { get; set; }
        public DbConnectionStrings ConnectionStrings { get; set; } = default!;

        public string ConnectionString
            => UseSqLite ? ConnectionStrings.Sqlite.Replace("|DataDirectory|", Directory.GetParent(Directory.GetCurrentDirectory()).FullName) : ConnectionStrings.MsSql;

        public class DbConnectionStrings
        {
            public string MsSql { get; set; } = "";
            public string Sqlite { get; set; } = "";
        }
    }
}