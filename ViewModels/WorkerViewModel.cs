using GameFinderAppV2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;

namespace GameFinderAppV2.ViewModels
{
    public class WorkerViewModel
    {
        private DatabaseModel _db;

        public WorkerViewModel()
        {
            _db = new DatabaseModel();
        }

        public List<DBDataViewModel> filter(List<TextBox> generatedTextBoxes, string table)
        {
            List<DBDataViewModel> result = new List<DBDataViewModel>();
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
    
        public List<DBDataViewModel> buildReturnList<T>(List<T> items)
        {
            List<DBDataViewModel> ret = new List<DBDataViewModel>();
            foreach (object item in items)
            {
                DBDataViewModel viewModel = new DBDataViewModel();

                T newEntity = (T)item;
                PropertyInfo[] fields = WorkerModel.getFields(newEntity.GetType());
                foreach (PropertyInfo info in fields)
                {
                    if (!info.Name.Equals("Id"))
                        viewModel.param.Add(info.Name, info.GetValue(newEntity)?.ToString() ?? "null");
                }
                ret.Add(viewModel);
            }
            return ret;
        }
    }
}
