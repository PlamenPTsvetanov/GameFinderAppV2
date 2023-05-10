using GameFinderAppV2.Models;
using System.Collections.Generic;

namespace GameFinderAppV2.Utils
{

    public class DBDataUtil
    {
        private DBDataModel _DBModel { get; set; }

        public DBDataUtil()
        {
            _DBModel = new DBDataModel();
        }

        public string tableName { get { return _DBModel.tableName; } }
        public Dictionary<string, string> param { get { return _DBModel.param; } }
    }
}
