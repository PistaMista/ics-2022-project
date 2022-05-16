using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CarPool.App.Converters
{
    public class StringToDateTime : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime time = DateTime.Now;

            if (value is DateTime)
            {
                time = (DateTime)value;
            }

            return time.ToString("dd.MM.yyyy HH:mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime time;
            bool success = DateTime.TryParseExact(value.ToString(), "dd.MM.yyyy HH:mm", culture, DateTimeStyles.None, out time);
            return success ? time : DateTime.Now;
        }
    }
}
