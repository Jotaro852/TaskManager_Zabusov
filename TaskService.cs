using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TaskManagerWPF.Models;
using System.Linq;

namespace TaskManagerWPF.Services
{
    public class TaskService
    {
        private const string FilePath = "Data/tasks.json";
        private List<TaskItem> _tasks = new();

        public TaskService()
        {
            LoadTasks();
        }

        public List<TaskItem> GetTasks() => _tasks;

        public void AddTask(TaskItem task)
        {
            _tasks.Add(task);
            SaveTasks();
        }

        public void UpdateTask(TaskItem task)
        {
            SaveTasks();
        }

        public void DeleteTask(TaskItem task)
        {
            _tasks.Remove(task);
            SaveTasks();
        }

        private void SaveTasks()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(_tasks, Formatting.Indented));
        }

        private void LoadTasks()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                _tasks = JsonConvert.DeserializeObject<List<TaskItem>>(json) ?? new List<TaskItem>();
            }
        }
    }
}