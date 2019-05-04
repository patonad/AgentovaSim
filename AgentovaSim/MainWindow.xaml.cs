using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PropertyChanged;
using simulation;

namespace AgentovaSim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class MainWindow : Window
    {
        private int _pocetRepl = 1000;
        private Thread t;
        public MySimulation MySimulation { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            MySimulation = new MySimulation(1,0,0,0,0,0);
            DataContext = this;
        }
        private void Simuluj()
        {
            MySimulation.Simulate(_pocetRepl);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MySimulation.Fast = true;
            _pocetRepl = 1000;
            Thread t = new Thread(Simuluj);
            t.Start();
            var a = MySimulation.Pocet / _pocetRepl;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MySimulation.Fast = false;
            _pocetRepl = 1;
            t = new Thread(Simuluj);
            t.Start();
        }
    }
}
