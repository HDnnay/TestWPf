using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestWPf.Commands
{
    public class TraditionalRelayCommand: ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        private bool _lastCanExecuteValue;
        private readonly object _lockObject = new object();

        // 传统的事件声明
        private event EventHandler _canExecuteChanged;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                lock (_lockObject)
                {
                    _canExecuteChanged += value;
                }
            }
            remove
            {
                lock (_lockObject)
                {
                    _canExecuteChanged -= value;
                }
            }
        }

        /// <summary>
        /// 初始化 RelayCommand
        /// </summary>
        /// <param name="execute">执行逻辑</param>
        /// <param name="canExecute">判断是否可执行的逻辑</param>
        public TraditionalRelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;

            // 初始化最后一次可执行状态
            _lastCanExecuteValue = CanExecute(null);
        }

        /// <summary>
        /// 判断命令是否可以执行
        /// </summary>
        public bool CanExecute(object parameter)
        {
            try
            {
                return _canExecute?.Invoke() ?? true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    _execute();
                }
                catch (Exception ex)
                {
                    // 可以在这里记录日志或处理异常
                    System.Diagnostics.Debug.WriteLine($"命令执行失败: {ex.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// 手动触发 CanExecuteChanged 事件
        /// 当命令的可执行状态可能改变时调用此方法
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            var currentValue = CanExecute(null);

            // 只有当值真正变化时才触发事件
            if (currentValue != _lastCanExecuteValue)
            {
                _lastCanExecuteValue = currentValue;
                OnCanExecuteChanged();
            }
        }

        /// <summary>
        /// 触发 CanExecuteChanged 事件
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            EventHandler handler;
            lock (_lockObject)
            {
                handler = _canExecuteChanged;
            }

            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}
