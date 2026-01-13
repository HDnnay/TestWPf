using EntityApp.Events;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace TestWPf.Converts
{
    public class DeviceStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DeviceStatus status)
            {
                switch (status)
                {
                    case DeviceStatus.Close:
                        return new SolidColorBrush(Colors.Gray);
                    case DeviceStatus.Error:
                        return new SolidColorBrush(Colors.Orange);
                    case DeviceStatus.Open:
                        return new SolidColorBrush(Colors.Green);
                    default:
                        return new SolidColorBrush(Colors.DarkGray);
                }
            }
            return new SolidColorBrush(Colors.DarkGray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
