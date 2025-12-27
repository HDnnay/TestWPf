using EntityApp.Devices;
using EntityApp.Handlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestWPf.ViewModels
{
    public class NotificationViewModel : ViewModelBase
    {
        public NotificationViewModel()
        {
            
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }
        private PLCDeviceViewModel _device;
        public PLCDeviceViewModel Device
        {
            get { return _device; }
            set { _device = value; OnPropertyChanged(nameof(Device)); }
        }
        
    }
}
