using GameFinderAppV2.Models;
using System.Collections.Generic;

namespace GameFinderAppV2.ViewModels
{

    public class DBDataViewModel
    {
        private DBDataModel _DBModel { get; set; }

        public DBDataViewModel() { 
            _DBModel = new DBDataModel();
        }

        public string tableName { get { return _DBModel.tableName; } }
        public Dictionary<string, string> param { get { return _DBModel.param; } }
    }
}
