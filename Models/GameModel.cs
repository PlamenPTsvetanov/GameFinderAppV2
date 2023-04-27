using GameFinderAppV2.Models;
using System;

namespace GameFinderAppV2.Models
{
    public class GameModel : DBEntityModel
    {
        public string Title { get; set; }
        public short ReleaseYear { get; set; }
        public String Genres { get; set; }
        public String Platforms { get; set; }
        public double Price { get; set; }
        public double? Rating { get; set; }
        public String? AgeCategory { get; set; }
        public String? Edition { get; set; }
        public PublisherModel? Publisher { get; set; }
    }
}
