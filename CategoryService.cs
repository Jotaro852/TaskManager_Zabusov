using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TaskManagerWPF.Models;

namespace TaskManagerWPF.Services
{
    public class CategoryService
    {
        private const string FilePath = "Data/categories.json";
        public List<Category> Categories { get; } = new();

        public CategoryService()
        {
            LoadCategories();
            if (Categories.Count == 0)
            {
                InitializeDefaultCategories();
            }
        }

        private void LoadCategories()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                var loaded = JsonConvert.DeserializeObject<List<Category>>(json);
                if (loaded != null) Categories.AddRange(loaded);
            }
        }

        private void InitializeDefaultCategories()
        {
            Categories.AddRange(new[]
            {
                new Category { Name = "Работа", Color = "#FF4CAF50" },
                new Category { Name = "Личное", Color = "#FF2196F3" },
                new Category { Name = "Важное", Color = "#FFFFC107" }
            });
            SaveCategories();
        }

        public void SaveCategories()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(Categories, Formatting.Indented));
        }
    }
}