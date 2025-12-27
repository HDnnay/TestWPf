using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestWPf.Constrolls;
using TestWPf.Events;
using TestWPf.Windows;

namespace TestWPf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CommManger Instance { get; }
        public MainWindow()
        {
            Instance = CommManger.Instance;
            Instance.DataReceived+=Instance_DataReceived;
            InitializeComponent();
            
        }

        private void Instance_DataReceived(object? sender, DataEventArgs e)
        {
            textBox.Text = e.Data;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            new TestWindow().Show();
        }
        private static async Task<string> GetDataAsync()
        {
            // 模拟一个异步操作
            await Task.Delay(1000); // 关键点：这里没有使用 .ConfigureAwait(false)
                                    // 异步操作完成后，默认会尝试回到原始的UI线程执行后续代码
            return "数据加载完成";
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            Instance.Send("测试");
            CustumerTextBlock1.IsLighited = !CustumerTextBlock1.IsLighited;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var mes = ((TextBox)sender).Text;

            Instance.Send(mes);
        }
    }
}