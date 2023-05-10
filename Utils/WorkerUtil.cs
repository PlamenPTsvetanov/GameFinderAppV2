using GameFinderAppV2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;

namespace GameFinderAppV2.Utils
{
    public class WorkerUtil
    {
        public WorkerUtil()
        {

        }

        public List<DBDataUtil> filter(List<TextBox> generatedTextBoxes, string table)
        {
            List<DBDataUtil> result = new List<DBDataUtil>();
            if (table.Equals("GameModel"))
            {
                result = buildReturnList(GameUtil.filter(ref generatedTextBoxes));
            }
            else if (table.Equals("PublisherModel"))
            {
                result = buildReturnList(PublisherUtil.filter(ref generatedTextBoxes));
            }
            else if (table.Equals("RegionModel"))
            {
                result = buildReturnList(RegionUtil.filter(ref generatedTextBoxes));
            }

            return result;
        }

        public ObservableCollection<string> getFields(Type type)
        {
            ObservableCollection<string> fields = new ObservableCollection<string>();
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

        public List<DBDataUtil> buildReturnList<T>(List<T> items)
        {
            List<DBDataUtil> ret = new List<DBDataUtil>();
            foreach (object item in items)
            {
                DBDataUtil viewModel = new DBDataUtil();

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
