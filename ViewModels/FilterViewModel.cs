using GameFinderAppV2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace GameFinderAppV2.ViewModels
{
    public class FilterViewModel
    {
        public static List<T> filter<T>(ref List<TextBox> generatedTextBoxes, ref List<T> list)
        {
            List<T> returnList = new List<T>();

            bool valid = true;
            generatedTextBoxes.ForEach(t => { valid = checkIfValidInput(t); });
            if (!valid)
            {
                return returnList;
            }
            foreach (object entity in list)
            {
                bool pass = false;
                T newEntity = (T)entity;
                foreach (TextBox textBox in generatedTextBoxes)
                {
                    string simpleTName = textBox.Name.Substring(4, textBox.Name.Length - 4);
                    PropertyInfo[] fieldInfos = newEntity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    foreach (PropertyInfo fieldInfo in fieldInfos)
                    {
                        if (fieldInfo.Name.Equals(simpleTName))
                           pass = contains<T>(fieldInfo, newEntity, textBox);
 
                    }

                }
                if (pass)
                {
                    returnList.Add(newEntity);
                }
            }

            return returnList;
        }
        private static bool contains<T>(PropertyInfo fieldInfo, T newEntity, TextBox textBox)
        {
            List<string> input = new List<string>();

            string[] strings = textBox.Text.Split(",");
            foreach (var item in strings)
            {
                input.Add(item);
            }

            foreach (string inp in input)
            {
                if (fieldInfo.GetValue(newEntity) != null && fieldInfo.GetValue(newEntity).ToString().Contains(inp) && !inp.Equals(String.Empty))
                {
                    return true;
                }
            }
            return false;
        }
        private static bool checkIfValidInput(TextBox tb)
        {
            string pattern = "";
            if (tb.Name.Contains("Title") ||
            tb.Name.Contains("Publisher") ||
            tb.Name.Contains("Name") ||
            tb.Name.Contains("AgeCategory") ||
            tb.Name.Contains("Edition"))
            {
                pattern = "[A-z]+";
            }
            else if (tb.Name.Contains("Genres") ||
                tb.Name.Contains("Platforms"))
            {
                pattern = "[A-z]+[, ]*";
            }
            else if (tb.Name.Contains("Price") ||
                tb.Name.Contains("Rating"))
            {
                pattern = "[0-9.]+";
            }
            else if (tb.Name.Contains("ReleaseYear"))
            {
                pattern = "[1|2][0-9]{3}";
            }

            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(tb.Text))
            {
                MessageBox.Show(String.Format("Entered value {0} is invalid for field {1}", tb.Text, tb.Name.Substring(4)), "Error");
                return false;
            }
            return true;
        }
    }
}
