using EntityApp.Devices;
using EntityApp.EventManagers;
using EntityApp.Events;
using EntityApp.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestWPf.ViewModels
{
    public class PLCDeviceViewModel : ViewModelBase
    {

        private PLCDevice _device;
        private string _displayName;
        public PLCDeviceViewModel(PLCDevice device) 
        { 
            
            _device = device?? throw new ArgumentNullException(nameof(device));
            _displayName = $"设备{device.DeviceId}";
            DeviceEventManager.Instance.DeviceStatusChanged +=OnDeviceStatusChanged;
        }
        public string DeviceId { get { return _device.DeviceId; } }
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value;OnPropertyChanged(nameof(DisplayName)); }
        }
        public DeviceStatus Status => _device.Status;
        // 计算属性（用于UI绑定）
        public string StatusText => Status switch
        {
            DeviceStatus.Open => "运行中",
            DeviceStatus.Close => "已关闭",
            DeviceStatus.Error => "故障",
            _ => "未知"
        };
        public string StatusColor => Status switch
        {
            DeviceStatus.Open => "#4CAF50", // 绿色
            DeviceStatus.Close => "#FF9800", //红色
            DeviceStatus.Error => "#F44336", //橙色
            _ => "#FF9800" 
        };
        // 提供对原始PLCDevice的访问（如果需要）
        public PLCDevice GetPLCDevice() { return _device; }
        public bool IsOnline => Status == DeviceStatus.Open;

        private void OnDeviceStatusChanged(object? sender, DeviceStatusChangedEventArgs e)
        {
            if (e.DeviceId == DeviceId)
            {
                Application.Current.Dispatcher.Invoke(() => {
                    OnPropertyChanged(nameof(Status));
                    OnPropertyChanged(nameof(StatusText));
                    OnPropertyChanged(nameof(StatusColor));
                    OnPropertyChanged(nameof(IsOnline));
                });
            }
        }
        public void OpenDevice()
        {
           if(_device.Status!=DeviceStatus.Open)
                _device.Status = DeviceStatus.Open;
        }
        public void CloseDevice()
        {
            if (_device.Status != DeviceStatus.Close)
            {
                _device.Status = DeviceStatus.Close;
            }
        }
        public void ErroDevice()
        {
            if (_device.Status != DeviceStatus.Error)
            {
                _device.Status = DeviceStatus.Error;
            }
        }
        public void Dispose()
        {
            DeviceEventManager.Instance.DeviceStatusChanged -= OnDeviceStatusChanged;
        }
    }
}
