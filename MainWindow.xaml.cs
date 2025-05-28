using System.Windows;
using TaskManagerWPF.ViewModels;

namespace TaskManagerWPF.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}