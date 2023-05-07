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
        public WorkerViewModel()
        {
          
        }

        public List<DBDataViewModel> filter(List<TextBox> generatedTextBoxes, string table)
        {
            List<DBDataViewModel> result = new List<DBDataViewModel>();
            if (table.Equals("GameModel"))
            {
                result = buildReturnList(GameViewModel.filter(ref generatedTextBoxes));
            } else if (table.Equals("PublisherModel"))
            {
                result = buildReturnList(PublisherViewModel.filter(ref generatedTextBoxes));
            }
            else if (table.Equals("RegionModel"))
            {
                result = buildReturnList(RegionViewModel.filter(ref generatedTextBoxes));
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
