using EntityApp.EventManagers;
using EntityApp.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityApp.Handlers
{
    public class DataLogHandler
    {
        public DataLogHandler()
        {
            DeviceEventManager.Instance.DeviceStatusChanged +=OnDeviceStatusChanged;
        }
        private readonly object _fileLock = new object(); // 文件锁，防止并发写入冲突
        private async void OnDeviceStatusChanged(object? sender, DeviceStatusChangedEventArgs e)
        {
            try
            {
                var logContent = $"{DateTime.Now:yyyy:MM:dd HH:mm:ss} 设备：{e.DeviceId} 状态从{e.PreviousDeviceStatus}-->{e.CurrentDeviceStatus}";
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TestWpf\\logs");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                var file = path+ $"\\device_{e.DeviceId}.log";
                if (!File.Exists(file))
                {
                    using var stream = File.Create(file);
                    using var writer = new StreamWriter(stream);
                    await writer.WriteLineAsync("=================== 设备监控日志 ===================");

                    await writer.WriteLineAsync(logContent);
                    writer.WriteLine("====================================== \n");
                }
                else
                {
                    lock (_fileLock) // 确保线程安全
                    {
                        // 使用 StreamWriter 的 append 模式
                        using var writer = new StreamWriter(file, append: true, Encoding.UTF8);
                        writer.WriteLine("=================== 设备监控日志===================");
                        writer.WriteLineAsync(logContent);
                        writer.WriteLine("======================================\n");
                    }
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        public void Dispose()
        {
            DeviceEventManager.Instance.DeviceStatusChanged -=OnDeviceStatusChanged;

        }
    }
}
