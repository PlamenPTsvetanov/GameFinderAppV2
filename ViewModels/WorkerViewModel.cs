using GameFinderAppV2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace GameFinderAppV2.ViewModels
{
    public class WorkerViewModel
    {
        private DatabaseModel _db;

        public WorkerViewModel()
        {
            _db = new DatabaseModel();
        }

        public List<DBDataModel> filter(List<TextBox> generatedTextBoxes, string table)
        {
            /* Dictionary<string, string> dict = new Dictionary<string, string>();

             foreach (TextBox t in generatedTextBoxes)
             {
                 string field = t.Name.Substring(4, t.Name.Length - 4);
                 string value = t.Text;

                 dict.Add(field, value);
             }
             return _db.getDatabaseData(dict, table);*/
            List<DBDataModel> result = new List<DBDataModel>();
            if (table.Equals("GameModel"))
            {
                List<GameModel> gameModels = _db.Games.ToList();
                List<GameModel> filtered = FilterViewModel.filter<GameModel>(ref generatedTextBoxes, ref gameModels);
                result = buildReturnList<GameModel>(filtered);
            } else if (table.Equals("PublisherModel"))
            {
                List<PublisherModel> publishers = _db.Publishers.ToList();
                List<PublisherModel> filtered = FilterViewModel.filter<PublisherModel>(ref generatedTextBoxes, ref publishers);
                result = buildReturnList<PublisherModel>(filtered);
            }

            return result;
        }

        public ObservableCollection<String> getFields(Type type)
        {
            ObservableCollection<String> fields = new ObservableCollection<String>();
            PropertyInfo[] propertyInfos = WorkerModel.getFields(type);

            foreach (PropertyInfo info in propertyInfos) 
            {
                if (!info.Name.Equals("Id"))
                {
                    fields.Add(info.Name);
                }
            }

            return fields;
        }
    
        public List<DBDataModel> buildReturnList<T>(List<T> items)
        {
            List<DBDataModel> ret = new List<DBDataModel>();
            foreach (object item in items)
            {
                DBDataModel model = new DBDataModel();

                T newEntity = (T)item;
                PropertyInfo[] fields = WorkerModel.getFields(newEntity.GetType());
                foreach (PropertyInfo info in fields)
                {
                    if (!info.Name.Equals("Id"))
                        model.param.Add(info.Name, info.GetValue(newEntity)?.ToString() ?? "null");
                }
                ret.Add(model);
            }
            return ret;
        }
    }
}
