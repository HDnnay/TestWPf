using EntityApp.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityApp.EventManagers
{
    public class DeviceEventManager
    {
        public event EventHandler<DeviceStatusChangedEventArgs> DeviceStatusChanged;
        private static readonly Lazy<DeviceEventManager> _instance
            = new Lazy<DeviceEventManager>(() => new DeviceEventManager());
        public static DeviceEventManager Instance => _instance.Value;
        private DeviceEventManager() { }
        public void PublishStatusChanged(string deviceId, DeviceStatus prveDeviceStatus, DeviceStatus currentDeviceStatus)
        {
            var args = new DeviceStatusChangedEventArgs(deviceId, prveDeviceStatus, currentDeviceStatus);
            OnDeviceStatusChanged(args);
        }

        protected virtual void OnDeviceStatusChanged(DeviceStatusChangedEventArgs args)
        {
            DeviceStatusChanged?.Invoke(this, args);
        }
    }
}
