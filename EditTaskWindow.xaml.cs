using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TaskManagerWPF.Models;

namespace TaskManager
{
    public partial class EditTaskWindow : Window
    {
        public TaskItem Task { get; }
        private List<Category> Categories { get; }

        public EditTaskWindow(TaskItem task, List<Category> categories)
        {
            InitializeComponent();
            Task = task;
            Categories = categories;
            DataContext = this;
            
            // Инициализация приоритета
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
                    priorityComboBox.SelectedIndex = 1; // Средний по умолчанию
                    break;
            }
            
            // Инициализация категории
            if (task.Category == null)
            {
                // Устанавливаем "Без категории" если нет категории
                var noCategory = Categories.FirstOrDefault(c => c.Name == "Без категории");
                if (noCategory != null)
                {
                    Task.Category = noCategory;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Обновление приоритета
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
            
            // Сброс категории если выбрано "Без категории"
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