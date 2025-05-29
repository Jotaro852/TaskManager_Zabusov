using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TaskManagerWPF.Models;
namespace TaskManagerWPF.Views
{
    public partial class EditTaskWindow : Window
    {
        public TaskItem Task { get; }
        public List<Category> Categories { get; }

        public EditTaskWindow(TaskItem task, List<Category> categories)
        {
            InitializeComponent();
            Task = task;
            Categories = categories;
            DataContext = this;
            
            categoryComboBox.ItemsSource = Categories;
            
            switch (task.Priority)
            {
                case Priority.High:
                    priorityComboBox.SelectedIndex = 2;
                    break;
                case Priority.Medium:
                    priorityComboBox.SelectedIndex = 1;
                    break;
                case Priority.Low:
                    priorityComboBox.SelectedIndex = 0;
                    break;
                default:
                    priorityComboBox.SelectedIndex = 1;
                    break;
            }
            
            if (task.Category == null)
            {
                categoryComboBox.SelectedItem = Categories.FirstOrDefault(c => c.Name == "Без категории");
            }
            else
            {
                categoryComboBox.SelectedItem = Categories.FirstOrDefault(c => c.Name == task.Category.Name);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (priorityComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                Task.Priority = selectedItem.Content.ToString() switch
                {
                    "Высокий" => Priority.High,
                    "Средний" => Priority.Medium,
                    "Низкий" => Priority.Low,
                    _ => Priority.Medium
                };
            }
            
            if (Task.Category?.Name == "Без категории")
            {
                Task.Category = null;
            }
            
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}