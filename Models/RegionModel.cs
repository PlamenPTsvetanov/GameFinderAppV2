namespace GameFinderAppV2.Models
{
    public class RegionModel : DBEntityModel
    {
        public string name { get; set; }
        public long population { get; set; }
        public string timezone { get; set; }
    }
}
