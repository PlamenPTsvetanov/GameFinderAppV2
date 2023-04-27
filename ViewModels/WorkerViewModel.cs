using GameFinderAppV2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameFinderAppV2.ViewModels
{
    public class WorkerViewModel
    {
        private WorkerModel _worker;
        private DatabaseModel _db;


        public WorkerViewModel()
        {
            _worker = new WorkerModel();
            _db = new DatabaseModel();
        }

        public List<DBDataModel> filter(List<TextBox> generatedTextBoxes, string table)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            
            foreach (TextBox t in generatedTextBoxes)
            {
                string field = t.Name.Substring(4, t.Name.Length - 4);
                string value = t.Text;

                dict.Add(field, value);
            }
            return _db.getDatabaseData(dict, table);
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
    }
}
