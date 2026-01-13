using EntityApp.Devices;
using EntityApp.EventManagers;
using EntityApp.Events;
using EntityApp.Handlers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestWPf.Commands;

namespace TestWPf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ObservableCollection<PLCDeviceViewModel> _devices;
        private PLCDeviceViewModel _selectedDevice;
        private string _statusText;
        private int _onlineCount;
        private int _offlineCount;
        private int _errorCount;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<PLCDeviceViewModel> Devices => _devices;

        public PLCDeviceViewModel SelectedDevice
        {
            get => _selectedDevice;
            set
            {
                if (_selectedDevice != value)
                {
                    _selectedDevice = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsDeviceSelected));
                }
            }
        }

        public bool IsDeviceSelected => SelectedDevice != null;

        public string StatusText
        {
            get => _statusText;
            set
            {
                if (_statusText != value)
                {
                    _statusText = value;
                    OnPropertyChanged();
                }
            }
        }

        public int OnlineCount
        {
            get => _onlineCount;
            set
            {
                if (_onlineCount != value)
                {
                    _onlineCount = value;
                    OnPropertyChanged();
                }
            }
        }

        public int OfflineCount
        {
            get => _offlineCount;
            set
            {
                if (_offlineCount != value)
                {
                    _offlineCount = value;
                    OnPropertyChanged();
                }
            }
        }

        public int ErrorCount
        {
            get => _errorCount;
            set
            {
                if (_errorCount != value)
                {
                    _errorCount = value;
                    OnPropertyChanged();
                }
            }
        }

        // 命令
        public ICommand OpenSelectedCommand { get; }
        public ICommand CloseSelectedCommand { get; }
        public ICommand RefreshStatsCommand { get; }
        public ICommand ErrorSelectedCommand { get; }
        private readonly DataLogHandler _dataLogHandler;
        public MainViewModel()
        {
            _dataLogHandler = new DataLogHandler();
            _devices = new ObservableCollection<PLCDeviceViewModel>();

            // 初始化命令
            OpenSelectedCommand = new RelayCommand(
                execute: () => SelectedDevice?.OpenDevice(),
                canExecute: () => IsDeviceSelected && SelectedDevice.Status != DeviceStatus.Open);

            CloseSelectedCommand = new RelayCommand(
                execute: () => SelectedDevice?.CloseDevice(),
                canExecute: () => IsDeviceSelected && SelectedDevice.Status != DeviceStatus.Close);
            ErrorSelectedCommand = new RelayCommand(execute: () => SelectedDevice?.ErroDevice(),
                canExecute: () => IsDeviceSelected&&SelectedDevice.Status !=DeviceStatus.Error);
            RefreshStatsCommand = new RelayCommand(
                execute: RefreshStatistics);

            // 监听全局设备状态变化
            DeviceEventManager.Instance.DeviceStatusChanged += OnGlobalStatusChanged;
        }

        // 添加设备（包装你已有的PLCDevice）
        public void AddDevice(PLCDevice plcDevice)
        {
            var deviceVM = new PLCDeviceViewModel(plcDevice);

            Application.Current.Dispatcher.Invoke(() =>
            {
                _devices.Add(deviceVM);
                RefreshStatistics();
            });
        }

        // 添加多个设备
        public void AddDevices(IEnumerable<PLCDevice> plcDevices)
        {
            foreach (var device in plcDevices)
            {
                AddDevice(device);
            }
        }

        // 监听你已有的DeviceEventManager的全局事件
        private void OnGlobalStatusChanged(object sender, DeviceStatusChangedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                StatusText = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - 设备 {e.DeviceId} 状态从 {e.PreviousDeviceStatus} 变为 {e.CurrentDeviceStatus}";
                RefreshStatistics();
            });
        }

        private void RefreshStatistics()
        {
            if (_devices.Count == 0) return;

            OnlineCount = _devices.Count(d => d.IsOnline);
            OfflineCount = _devices.Count(d => d.Status == DeviceStatus.Close);
            ErrorCount = _devices.Count(t => t.Status==DeviceStatus.Error);
        }

        // 清理资源
        public void Dispose()
        {
            DeviceEventManager.Instance.DeviceStatusChanged -= OnGlobalStatusChanged;

            foreach (var device in _devices)
            {
                device.Dispose();
            }
            _devices.Clear();
            _dataLogHandler.Dispose();
        }


    }
}
