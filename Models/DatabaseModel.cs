using System.Data.Entity;

namespace GameFinderAppV2.Models
{
    public class DatabaseModel : DbContext
    {
        private static string _connString = "Server=localhost\\SQLEXPRESS;Database=Game_Database_V2;Trusted_Connection=True;";
        public DatabaseModel() : base("Server=localhost\\SQLEXPRESS;Database=Game_Database_V2;Trusted_Connection=True;") { }
        public DbSet<GameModel> Games { get; set; }
        public DbSet<PublisherModel> Publishers { get; set; }
        public DbSet<RegionModel> Regions { get; set; }
    }
}
