using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityApp.Events
{
    public class DeviceStatusChangedEventArgs : DeviceEventArgs
    {
        public DeviceStatus PreviousDeviceStatus { get;  }
        public DeviceStatus CurrentDeviceStatus { get; }
        public DeviceStatusChangedEventArgs(string deviceId, DeviceStatus preStatus,DeviceStatus currentStatus)
            : base(deviceId, $"状态变化--{preStatus}->{currentStatus}")
        {
            PreviousDeviceStatus = preStatus;
            CurrentDeviceStatus = currentStatus;
        }
    }
}
