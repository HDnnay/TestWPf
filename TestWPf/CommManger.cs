using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWPf.Events;

namespace TestWPf
{
    public class CommManger
    {
        private static readonly Lazy<CommManger> _instanc = new Lazy<CommManger>(() => new CommManger());
        public static CommManger Instance
        {
            get { return _instanc.Value; }
        }
        private CommManger() 
        {
        }
        public event EventHandler<DataEventArgs> DataReceived;
        public void Send(string data)
        {
            DataReceived?.Invoke(this, new DataEventArgs { Data = data});
        }
    }
}
