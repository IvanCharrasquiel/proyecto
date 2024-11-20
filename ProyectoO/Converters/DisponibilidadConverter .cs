using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace ProyectoO.Converters
{
    public class DisponibilidadConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool disponible = (bool)value;
            return disponible ? Colors.LightGreen : Colors.LightGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
