using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace GameFinderAppV2.Models
{
    public class WorkerModel
    {
        //Table ще се взима от направения избор от comboBox-a
        //TODO да се добави таблица, която съдържа имената на всички таблици
        public List<DBDataModel> filter(Dictionary<string, string> d, string table)
        {
            DatabaseModel db = new DatabaseModel();
            List<DBDataModel> dBDataModels = db.getDatabaseData(d, table);
            return dBDataModels;
        }


        public static PropertyInfo[] getFields(Type type)
        {
            BindingFlags bindingFlags = BindingFlags.Public |
                            BindingFlags.NonPublic |
                            BindingFlags.Instance |
            BindingFlags.Static;

            return type.GetProperties(bindingFlags);
        }
    }
}
