using System.Windows;

namespace TaskManagerWPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            // Создаем папку Data если её нет
            if (!System.IO.Directory.Exists("Data"))
                System.IO.Directory.CreateDirectory("Data");
        }
    }
}