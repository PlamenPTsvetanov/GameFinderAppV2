using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using GameFinderAppV2.Models;
using Label = System.Windows.Controls.Label;

namespace GameFinderAppV2.ViewModels
{
    public class UIGeneratorViewModel : INotifyPropertyChanged
    {
        private List<String> _orderedList = new List<string>();

        private ObservableCollection<String> _fieldList = new ObservableCollection<String>();
        public ObservableCollection<String> FieldList { get { return _fieldList; } }
        private Grid gridGeneratedFields { get; set; }
        private int generatedRowIndex = 0;
        private List<TextBox> _genTextBoxes = new List<TextBox>();
        private WorkerViewModel workerViewModel { get; set; }
        
        public event PropertyChangedEventHandler? PropertyChanged;

        public UIGeneratorViewModel(
            ref Grid gridGeneratedFields) 
        {
            this.gridGeneratedFields = gridGeneratedFields;
            workerViewModel = new WorkerViewModel();
        }
        
        public void addNewSearchGridRow(string selectedItem, ref Grid generatedFields)
        {
            removeFieldFromList(selectedItem);
            string tboxName = "tbox" + selectedItem;
            
            removeFromGeneratedTextBoxes(tboxName);

            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(50);

            TextBox newTextBox = new TextBox();
            newTextBox.Name = tboxName;
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

            this.generatedRowIndex = generatedRowIndex;

            generatedFields.RowDefinitions.Add(rowDefinition);
            generatedFields.Children.Add(newLabel);
            generatedFields.Children.Add(newTextBox);
            generatedFields.Children.Add(button);

            _genTextBoxes.Add(newTextBox);
        }

        public void getFieldsForComboBox(string table)
        {
            string namespaceOfProject = this.GetType().Namespace.Substring(0, this.GetType().Namespace.IndexOf('.'));
            _fieldList = workerViewModel.getFields(Type.GetType(namespaceOfProject +  ".Models." + (table.Substring(0, table.Length - 1) + "Model")));
            _orderedList = _fieldList.ToList();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_fieldList)));
        }

        public void generateLabel(object content, ref int idx, int row, ref Grid gridOut)
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
            gridOut.Children.Add(newLabel);
        }

        public void search(string selectedObject, ref Grid gridOut)
        {
            if (gridOut.RowDefinitions.Count == 0)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(30);

                gridOut.RowDefinitions.Add(rowDefinition);


                for (int ii = 0; ii < _orderedList.Count; ii++)
                {
                    ColumnDefinition colDefinition = new ColumnDefinition();
                    colDefinition.Width =  GridLength.Auto;
                    gridOut.ColumnDefinitions.Add(colDefinition);
                }

                int idx = 0;
                foreach (String field in _orderedList)
                {
                    Label newLabel = new Label();
                    newLabel.Content = field;
                    newLabel.Height = 30;
                    newLabel.Width = 90;

                    newLabel.VerticalAlignment = VerticalAlignment.Center;
                    newLabel.HorizontalAlignment = HorizontalAlignment.Left;

                    Grid.SetRow(newLabel, 0);
                    Grid.SetColumn(newLabel, idx++);
                    gridOut.Children.Add(newLabel);
                }
            }
            string search =
                selectedObject.Substring(0, selectedObject.Length - 1) + "Model";

            List<DBDataViewModel> filtered = workerViewModel.filter(_genTextBoxes, search);

            foreach (DBDataViewModel model in filtered)
            {
                int i = 0;
                RowDefinition rowDef = new RowDefinition();
                rowDef.Height = new GridLength(30);

                gridOut.RowDefinitions.Add(rowDef);
                int row = gridOut.RowDefinitions.Count - 1;

                foreach (string cell in model.param.Keys)
                {
                    foreach (string field in _orderedList)
                    {
                        if (cell.Equals(field))
                        {
                            generateLabel(model.param.GetValueOrDefault(cell), ref i, row, ref gridOut);
                            break;
                        }
                    }
                }
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(gridOut)));
        }

        public void clearGrid(Grid gridOutput)
        {
            generatedRowIndex = 0;
            gridOutput.Children.Clear();
            gridOutput.RowDefinitions.Clear();
            gridOutput.ColumnDefinitions.Clear();
        }

        public void clearGrid(Grid gridOutput, Grid gridGeneratedFields)
        {
            clearGrid(gridOutput);
            _genTextBoxes.Clear();
            gridGeneratedFields.Children.Clear();
            gridGeneratedFields.RowDefinitions.Clear();
        }

        private string setToolTip(string name)
        {
            if (name.Contains("Title") ||
            name.Contains("Publisher") ||
            name.Contains("Name") ||
            name.Contains("AgeCategory") ||
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
            
            for (int i = gridGeneratedFields.Children.Count - 1; i >= 0; i--)
            {
                if (Grid.GetRow(gridGeneratedFields.Children[i]) == tag.rowIndex)
                {
                    if (gridGeneratedFields.Children[i] as TextBox != null)
                    {
                        TextBox textBox = gridGeneratedFields.Children[i] as TextBox;
                        removeFromGeneratedTextBoxes(textBox.Name);
                    }
                    gridGeneratedFields.Children.Remove(gridGeneratedFields.Children[i]);   
                }
            }
            gridGeneratedFields.RowDefinitions.RemoveAt(tag.rowIndex);

            for (int i = 0; i < gridGeneratedFields.Children.Count; i++)
            {
                int currRow = Grid.GetRow(gridGeneratedFields.Children[i]);
                if (currRow > tag.rowIndex)
                {
                    Grid.SetRow(gridGeneratedFields.Children[i], currRow - 1 <= 0 ? 0 : currRow - 1);

                    if (gridGeneratedFields.Children[i] as Button != null)
                    {
                        Button button = gridGeneratedFields.Children[i] as Button;

                        TagForButton newTag = new TagForButton();
                        TagForButton oldTag = (TagForButton)button.Tag;

                        newTag.rowIndex = oldTag.rowIndex - 1 <= 0 ? 0 : oldTag.rowIndex - 1;
                        newTag.fieldName = oldTag.fieldName;

                        button.Tag = newTag;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(gridGeneratedFields)));
                    }  
                }
            }
            generatedRowIndex = gridGeneratedFields.RowDefinitions.Count;
            gridGeneratedFields.UpdateLayout();

            reorderFields();
        }

        private void reorderFields()
        {
            ObservableCollection<string> orderedList = new ObservableCollection<string>();
            foreach (string field in _orderedList)
            {
                if (_fieldList.Contains(field))
                {
                    orderedList.Add(field);
                }
            }

            _fieldList = orderedList;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FieldList)));
        }

        private void removeFromGeneratedTextBoxes(string name)
        {
            for (int i = _genTextBoxes.Count - 1; i >= 0; i--)
            {
                if (_genTextBoxes.ElementAt(i).Name.Equals(name))
                {
                    _genTextBoxes.RemoveAt(i);
                }
            }
        }

        private void removeFieldFromList(string field)
        {
            _fieldList.Remove(field);
        }
    }
}
