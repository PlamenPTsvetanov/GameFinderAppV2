using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using GameFinderAppV2.Models;
using System.Reflection.Emit;
using Label = System.Windows.Controls.Label;

namespace GameFinderAppV2.ViewModels
{
    public class UIGeneratorViewModel : INotifyPropertyChanged
    {
        private List<String> _orderedList = new List<string>();

        private ObservableCollection<String> _fieldList = new ObservableCollection<String>();
        public ObservableCollection<String> FieldList { get { return _fieldList; } }
        private Grid gridGeneratedFields { get; set; }
        private Grid gridOutput { get; set; }
        private int generatedRowIndex { get; set; }

        private WorkerViewModel workerViewModel { get; set; }
        
        public event PropertyChangedEventHandler? PropertyChanged;

        public UIGeneratorViewModel(
            ref Grid gridGeneratedFields,
            ref Grid gridOutput) 
        {
            this.gridGeneratedFields = gridGeneratedFields;
            this.gridOutput = gridOutput;
            workerViewModel = new WorkerViewModel();
        }
        
        public void addNewSearchGridRow(
            string selectedItem, 
            ref int generatedRowIndex,
            ref List<TextBox> generatedTextBoxes)
        {
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(50);

            TextBox newTextBox = new TextBox();
            newTextBox.Name = "tbox" + selectedItem;
            newTextBox.Text = "";
            newTextBox.Height = 25;
            newTextBox.Width = 200;
            newTextBox.VerticalAlignment = VerticalAlignment.Top;
            newTextBox.HorizontalAlignment = HorizontalAlignment.Left;
            newTextBox.ToolTip = setToolTip(selectedItem);

            Label newLabel = new Label();
            newLabel.Content = selectedItem + ":";
            newLabel.Margin = new Thickness(10, 0, 0, 0);
            newLabel.Height = 30;
            newLabel.Width = 100;
            newLabel.VerticalAlignment = VerticalAlignment.Top;
            newLabel.HorizontalAlignment = HorizontalAlignment.Left;

            Button button = new Button();
            button.Content = "❌";
            TagForButton t = new TagForButton();
            t.rowIndex = generatedRowIndex;
            t.fieldName = selectedItem;
            button.Tag = t;

            button.Click += xButtonClick;
            button.VerticalAlignment = VerticalAlignment.Top;
            button.HorizontalAlignment = HorizontalAlignment.Left;
            button.Height = 25;
            button.Width = 30;
            button.Margin = new Thickness(20, 0, 0, 0);
            Grid.SetRow(newLabel, generatedRowIndex);
            Grid.SetColumn(newLabel, 0);

            Grid.SetRow(newTextBox, generatedRowIndex);
            Grid.SetColumn(newTextBox, 1);

            Grid.SetRow(button, generatedRowIndex++);
            Grid.SetColumn(button, 2);
            
            gridGeneratedFields.RowDefinitions.Add(rowDefinition);
            gridGeneratedFields.Children.Add(newLabel);
            gridGeneratedFields.Children.Add(newTextBox);
            gridGeneratedFields.Children.Add(button);

            generatedTextBoxes.Add(newTextBox);
        }

        public void getFieldsForComboBox(string table)
        {
            string namespaceOfProject = this.GetType().Namespace.Substring(0, this.GetType().Namespace.IndexOf('.'));
            _fieldList = workerViewModel.getFields(Type.GetType(namespaceOfProject +  ".Models." + (table.Substring(0, table.Length - 1) + "Model")));
            _orderedList = _fieldList.ToList();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_fieldList)));
        }

        public void generateLabel(object content, ref int idx, int row)
        {
            Label newLabel = new Label();
            newLabel.Content = content;
            newLabel.Height = 30;
            newLabel.Width = 90;

            newLabel.VerticalAlignment = VerticalAlignment.Center;
            newLabel.HorizontalAlignment = HorizontalAlignment.Left;
            newLabel.BorderBrush = Brushes.Gray;
            newLabel.BorderThickness = new Thickness(2);
            Grid.SetRow(newLabel, row);
            Grid.SetColumn(newLabel, idx++);
            gridOutput.Children.Add(newLabel);
        }

        public void search(string selectedObject, ref List<TextBox> generatedTextBoxes)
        {
            List<string> _fields = _fieldList.ToList();
            if (gridOutput.RowDefinitions.Count == 0)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(30);

                gridOutput.RowDefinitions.Add(rowDefinition);


                for (int ii = 0; ii < _fields.Count; ii++)
                {
                    ColumnDefinition colDefinition = new ColumnDefinition();
                    colDefinition.Width = new GridLength(1, GridUnitType.Star);
                    gridOutput.ColumnDefinitions.Add(colDefinition);
                }

                int idx = 0;
                foreach (String field in _fields)
                {
                    Label newLabel = new Label();
                    newLabel.Content = field;
                    newLabel.Height = 30;
                    newLabel.Width = 90;

                    newLabel.VerticalAlignment = VerticalAlignment.Center;
                    newLabel.HorizontalAlignment = HorizontalAlignment.Left;

                    Grid.SetRow(newLabel, 0);
                    Grid.SetColumn(newLabel, idx++);
                    gridOutput.Children.Add(newLabel);
                }
            }
            string search = selectedObject.Substring(0, selectedObject.Length - 1) + "Models";
            List<DBDataModel> filtered = workerViewModel.filter(generatedTextBoxes, search);

            int i = 0;
            foreach (DBDataModel model in filtered)
            {
                RowDefinition rowDef = new RowDefinition();
                rowDef.Height = new GridLength(30);

                gridOutput.RowDefinitions.Add(rowDef);
                int row = gridOutput.RowDefinitions.Count - 1;

                foreach (string cell in model.param.Keys)
                {
                    foreach (string field in _fields)
                    {
                        if (cell.Equals(field))
                        {
                            generateLabel(model.param.GetValueOrDefault(cell), ref i, row);
                            break;
                        }
                    }
                }
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(gridOutput)));
        }

        public void clearGrid(Grid grid)
        {
            grid.Children.Clear();
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();
        }

        private string setToolTip(string name)
        {
            if (name.Contains("Title") ||
            name.Contains("Publisher") ||
            name.Contains("Name") ||
            name.Contains("Edition"))
            {
                return "Field requires a string value for " + name.ToLower() + " attribute.";
            }
            else if (name.Contains("Genres") ||
                name.Contains("Platforms"))
            {
                return "Field requires a string value separated by commas for " + name.ToLower() + " attribute.";
            }
            else if (name.Contains("Price") ||
                name.Contains("Rating"))
            {
                return "Field requires a double value separated by a divider <.> for " + name.ToLower() + " attribute.";
            }
            else if (name.Contains("ReleaseYear"))
            {
                return "Field requires a valid year value for " + name.ToLower() + " attribute."; ;
            }
            return "";
        }

        private void xButtonClick(object sender, RoutedEventArgs e)
        {
            TagForButton tag = (sender as Button)?.Tag as TagForButton;

            _fieldList.Add(tag.fieldName);
            gridGeneratedFields.RowDefinitions.RemoveAt(tag.rowIndex);

            bool isRemoved = false;
            for (int i = gridGeneratedFields.Children.Count - 1; i >= 0; i--)
            {
                UIElement element = gridGeneratedFields.Children[i];
                int row = Grid.GetRow(element);
                if (row == tag.rowIndex)
                {
                    TextBox tb = element as TextBox;
                    if (tb != null)
                    {
                        tb.Text = "";
                    }
                    gridGeneratedFields.Children.Remove(element);
                    isRemoved = true;
                }
            }

            foreach (UIElement el in gridGeneratedFields.Children)
            {
                if (Grid.GetRow(el) > tag.rowIndex)
                {
                    int v = Grid.GetRow(el) - 1;
                    Grid.SetRow(el, v);
                }
            }
            if (isRemoved)
            {
                generatedRowIndex--;
            }
            reorderFields();
        }

        private void reorderFields()
        {
            ObservableCollection<string> orderedList = new ObservableCollection<string>();
            foreach (string field in _fieldList)
            {
                if (_orderedList.Contains(field))
                {
                    orderedList.Add(field);
                }
            }

            _fieldList = orderedList;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_fieldList)));
        }
    }
}
