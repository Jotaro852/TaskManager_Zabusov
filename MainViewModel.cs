using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using TaskManagerWPF.Models;
using TaskManagerWPF.Services;
using TaskManagerWPF.Utils;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using TaskManagerWPF.Views;

namespace TaskManagerWPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly TaskService _taskService;
        private readonly CategoryService _categoryService;
        private string _searchText = "";
        private Category _selectedCategory;

        public ObservableCollection<TaskItem> Tasks { get; } = new();
        public List<Category> Categories => _categoryService.Categories;
        public ICollectionView TasksView { get; }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                TasksView.Refresh();
            }
        }

        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                TasksView.Refresh();
            }
        }

        public ICommand AddTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand EditTaskCommand { get; }
        public ICommand ResetFiltersCommand { get; }

        public MainViewModel()
        {
            _taskService = new TaskService();
            _categoryService = new CategoryService();
            
            foreach (var task in _taskService.GetTasks())
            {
                Tasks.Add(task);
            }

            TasksView = CollectionViewSource.GetDefaultView(Tasks);
            TasksView.Filter = FilterTasks;
            
            SelectedCategory = Categories.FirstOrDefault(c => c.Name == "Без категории");

            AddTaskCommand = new RelayCommand(AddTask);
            DeleteTaskCommand = new RelayCommand<TaskItem>(DeleteTask);
            EditTaskCommand = new RelayCommand<TaskItem>(EditTask);
            ResetFiltersCommand = new RelayCommand(ResetFilters);
        }

        private bool FilterTasks(object obj)
        {
            if (obj is not TaskItem task) return false;
            
            if (!string.IsNullOrWhiteSpace(SearchText) &&
                !task.Title.Contains(SearchText) &&
                !(task.Description?.Contains(SearchText) ?? true))
            {
                return false;
            }
            
            if (SelectedCategory != null)
            {
                if (SelectedCategory.Name == "Без категории")
                {
                    if (task.Category != null) return false;
                }
                else if (task.Category?.Name != SelectedCategory.Name)
                {
                    return false;
                }
            }
            
            return true;
        }

        private void AddTask()
        {
            var newTask = new TaskItem 
            {
                Title = "Новая задача",
                Category = null
            };
            
            Tasks.Add(newTask);
            _taskService.AddTask(newTask);
            TasksView.Refresh();
        }

        private void DeleteTask(TaskItem task)
        {
            Tasks.Remove(task);
            _taskService.DeleteTask(task);
            TasksView.Refresh();
        }

        private void EditTask(TaskItem task)
        {
            var editWindow = new EditTaskWindow(task, Categories);
            if (editWindow.ShowDialog() == true)
            {
                _taskService.UpdateTask(task);
                TasksView.Refresh();
            }
        }

        private void ResetFilters()
        {
            SearchText = "";
            SelectedCategory = Categories.FirstOrDefault(c => c.Name == "Без категории");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}