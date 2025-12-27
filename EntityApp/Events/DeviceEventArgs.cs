using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityApp.Events
{
    public abstract class DeviceEventArgs:EventArgs
    {
        public DateTime Timestamp { get; }
        public string DeviceId { get; }
        public string Message { get; }
        public DeviceEventArgs(string deviceId,string message)
        {
            DeviceId = deviceId;
            Message = message;
            Timestamp = DateTime.Now;
        }
    }
}
