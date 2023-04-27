using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace GameFinderAppV2.Models
{
    public class WorkerModel
    {
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
