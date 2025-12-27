using EntityApp.EventManagers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityApp.Handlers
{
    public class UIHandler
    {

        public UIHandler() 
        {
            DeviceEventManager.Instance.DeviceStatusChanged +=EventManager_DeviceStatusChanged;
        }

        private void EventManager_DeviceStatusChanged(object? sender, Events.DeviceStatusChangedEventArgs e)
        {
            
        }
        public void Unregister() 
        {
            DeviceEventManager.Instance.DeviceStatusChanged -=EventManager_DeviceStatusChanged;
        }
    }
}
