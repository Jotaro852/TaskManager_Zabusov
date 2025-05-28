using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using TaskManagerWPF.Models;

namespace TaskManagerWPF.Converters
{
    public class PriorityToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Priority priority)
            {
                return priority switch
                {
                    Priority.High => new SolidColorBrush(Colors.OrangeRed),
                    Priority.Medium => new SolidColorBrush(Colors.Gold),
                    Priority.Low => new SolidColorBrush(Colors.LimeGreen),
                    _ => new SolidColorBrush(Colors.Gray)
                };
            }
            return new SolidColorBrush(Colors.Gray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}