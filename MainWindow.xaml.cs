using GameFinderAppV2.Models;
using GameFinderAppV2.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace GameFinderAppV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    { 
        private string selectedObject { get; set; }
        private bool flag = true;
        private UIGeneratorViewModel uiGenerator { get; set; }
        public ObservableCollection<String> SearchModels { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            SearchModels = new ObservableCollection<string> { "Games", "Publishers", "Regions"};
            uiGenerator = new UIGeneratorViewModel(ref gridGeneratedFields);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void cbFields_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (flag)
            {
                flag = false;
                string selectedItem = cbFields.SelectedItem as string;

                uiGenerator.addNewSearchGridRow(selectedItem, ref gridGeneratedFields);
            }
            flag = true;
        }
    
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            uiGenerator.clearGrid(gridOutput);
            gridOutput.DataContext = uiGenerator;
            uiGenerator.search(selectedObject, ref gridOutput);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            uiGenerator.clearGrid(gridOutput);
        }

        private void cbSearchOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedObject = cbSearchOptions.SelectedItem as string;
            if (selectedObject != null)
            {
                uiGenerator.getFieldsForComboBox(selectedObject);

                Binding binding = new Binding("FieldList");
                binding.Source = uiGenerator;
                cbFields.SetBinding(ListBox.ItemsSourceProperty, binding);

                lblSearch.Visibility = Visibility.Hidden;
                cbSearchOptions.Visibility = Visibility.Hidden;

                lblFields.Visibility = Visibility.Visible;
                cbFields.Visibility = Visibility.Visible;
            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            uiGenerator.clearGrid(gridOutput, gridGeneratedFields);

            lblSearch.Visibility = Visibility.Visible;
            cbSearchOptions.Visibility = Visibility.Visible;
            cbSearchOptions.SelectedIndex = -1; 

            lblFields.Visibility = Visibility.Hidden;
            cbFields.Visibility = Visibility.Hidden;
        }

    }

    internal class TagForButton
    {
        public int rowIndex { get; set; }
        public string fieldName { get; set; }

        public Grid grid { get; set; }
    }
}
