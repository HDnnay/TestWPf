using EntityApp.EventManagers;
using EntityApp.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityApp.Devices
{
    public class PLCDevice
    {
        private DeviceStatus _status = DeviceStatus.Close;
        public string DeviceId { get; }
        public PLCDevice(string id)
        {
            DeviceId = id;
        }
        public DeviceStatus Status
        { 
            get { return _status; }
            set
            {
                if (_status!=value)
                {
                    var prvStatus = _status;
                    var currentStatus = value;
                    _status = value;
                    DeviceEventManager.Instance.PublishStatusChanged(DeviceId,prvStatus,currentStatus);
                }
            }
        }
    }
}
