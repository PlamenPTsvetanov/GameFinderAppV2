using System.Collections.Generic;

namespace GameFinderAppV2.Models
{
    public class DBDataModel
    {
        public DBDataModel() { param = new Dictionary<string, string>(); }

        public string tableName { get; set; }
        public Dictionary<string, string> param{ get; set; }
    }
}
