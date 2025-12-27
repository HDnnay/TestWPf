using EntityApp.Devices;
using EntityApp.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestWPf.ViewModels;

namespace TestWPf.Windows
{
    /// <summary>
    /// NotificationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NotificationWindow : Window
    {
        private readonly MainViewModel _viewModel;
        private List<PLCDevice> _plcDevices; // 保存你已有的PLCDevice引用
        public NotificationWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;
            InitializePLCDevices();
            this.Closed += NotificationWindow_Closed;
        }

        private void InitializePLCDevices()
        {
            // 创建你已有的PLCDevice对象
            _plcDevices = new List<PLCDevice>
        {
            new PLCDevice("PLC001"),
            new PLCDevice("PLC002"),
            new PLCDevice("PLC003"),
            new PLCDevice("PLC004")
        };

            // 将PLCDevice添加到ViewModel
            _viewModel.AddDevices(_plcDevices);

            // 可以立即设置一些设备状态
            _plcDevices[0].Status = DeviceStatus.Open;
            _plcDevices[1].Status = DeviceStatus.Open;
            _plcDevices[2].Status = DeviceStatus.Close;
            _plcDevices[3].Status = DeviceStatus.Error;
        }

        private void NotificationWindow_Closed(object? sender, EventArgs e)
        {
            _viewModel.Dispose();
        }
    }
}
