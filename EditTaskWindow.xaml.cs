using System.Collections.Generic;
using System.Windows;
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
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
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