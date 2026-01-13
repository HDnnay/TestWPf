using EntityApp.Events;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestWPf.Converts
{
    public class DeviceStatusToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DeviceStatus status)
            {
                return status switch
                {
                    DeviceStatus.Open => "已打开",
                    DeviceStatus.Close => "已关闭",
                    DeviceStatus.Error => "连接中...",
                    _ => "未知状态"
                };
            }
            return "未知状态";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                return str switch
                {
                    "已打开" => DeviceStatus.Open,
                    "已关闭" => DeviceStatus.Close,
                    "连接中..." => DeviceStatus.Error,
                    _ => DeviceStatus.Close
                };
            }
            return DeviceStatus.Close;
        }

    }
}
