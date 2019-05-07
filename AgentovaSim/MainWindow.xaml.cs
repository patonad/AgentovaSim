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
using AgentovaSim.simulation;
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
        private Thread t;
        public Manazer Manazer { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Label1.Visibility = Visibility.Hidden;
            Label2.Visibility = Visibility.Hidden;
            Label3.Visibility = Visibility.Hidden;
            TextBoxAM.Visibility = Visibility.Hidden;
            TextBoxBM.Visibility = Visibility.Hidden;
            TextBoxCM.Visibility = Visibility.Hidden;
            Label1R.Visibility = Visibility.Hidden;
            Label2R.Visibility = Visibility.Hidden;
            Label3R.Visibility = Visibility.Hidden;
            TextBoxAMR.Visibility = Visibility.Hidden;
            TextBoxBMR.Visibility = Visibility.Hidden;
            TextBoxCMR.Visibility = Visibility.Hidden;
            Manazer = new Manazer();
            DataContext = Manazer;
        }
        //private void Simuluj()
        //{
        //    Manazer._cakanie = _cakanie;
        //    Manazer.Simulate(_pocetRepl);
        //}


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manazer.Fast = true;
            int a;
            int.TryParse(TextBoxA1R.Text, out a);
            Manazer._linkaATyp1 = a;
            int.TryParse(TextBoxA2R.Text, out a);
            Manazer._linkaATyp2 = a;
            int.TryParse(TextBoxAMR.Text, out a);
            Manazer._linkaAMicro = a;
            int.TryParse(TextBoxB1R.Text, out a);
            Manazer._linkaBTyp1 = a;
            int.TryParse(TextBoxB2R.Text, out a);
            Manazer._linkaBTyp2 = a;
            int.TryParse(TextBoxBMR.Text, out a);
            Manazer._linkaBMicro = a;
            int.TryParse(TextBoxC1R.Text, out a);
            Manazer._linkaCTyp1 = a;
            int.TryParse(TextBoxC2R.Text, out a);
            Manazer._linkaCTyp2 = a;
            int.TryParse(TextBoxCMR.Text, out a);
            Manazer._linkaCMicro = a;
            Manazer._cakanie = CheckBoxCakanieR.IsChecked != null && CheckBoxCakanieR.IsChecked.Value;
            int.TryParse(TextBoxPocetRepl.Text, out a);
            Manazer._pocet = a;
            Manazer.SpustiSimulaciuRychlo();
        }
        private void Schovaj()
        {

            //GridGraf.Visibility = Visibility.Hidden;
            GridRep.Visibility = Visibility.Hidden;
            GridSim.Visibility = Visibility.Hidden;
        }
        private void BtnReplikacia_Click(object sender, RoutedEventArgs e)
        {
            Schovaj();
            GridRep.Visibility = Visibility.Visible;
        }
        private void BtnSimulacia_Click(object sender, RoutedEventArgs e)
        {
            Schovaj();
            GridSim.Visibility = Visibility.Visible;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Manazer.Fast = false;
            int a;
            int.TryParse(TextBoxA1.Text, out a);
            Manazer._linkaATyp1 = a;
            int.TryParse(TextBoxA2.Text, out a);
            Manazer._linkaATyp2 = a;
            int.TryParse(TextBoxAM.Text, out a);
            Manazer._linkaAMicro = a;
            int.TryParse(TextBoxB1.Text, out a);
            Manazer._linkaBTyp1 = a;
            int.TryParse(TextBoxB2.Text, out a);
            Manazer._linkaBTyp2 = a;
            int.TryParse(TextBoxBM.Text, out a);
            Manazer._linkaBMicro = a;
            int.TryParse(TextBoxC1.Text, out a);
            Manazer._linkaCTyp1 = a;
            int.TryParse(TextBoxC2.Text, out a);
            Manazer._linkaCTyp2 = a;
            int.TryParse(TextBoxCM.Text, out a);
            Manazer._linkaCMicro = a;
            Manazer._cakanie = CheckBoxCakanie.IsChecked != null && CheckBoxCakanie.IsChecked.Value;
            Manazer.SpustiSimulaciuPomala();
        }
        private void BtnSimPause_Click(object sender, RoutedEventArgs e)
        {
            if (Manazer != null)
                Manazer.Pause();
        }

        private void BtnSimResume_Click(object sender, RoutedEventArgs e)
        {

            if (Manazer != null)
                Manazer.Resume();
        }

        private void BtnSimStop_Click(object sender, RoutedEventArgs e)
        {
            if (Manazer != null)
                Manazer.Stop();
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Manazer != null)
            {
                int a = (int)SlSimRychlost.Value;
                if (Manazer.MySimulation != null)
                {
                    Manazer.MySimulation.Spomalenie = 1000000 - a * a;
                }
            }
        }
        private void CheckBoxMikrobus_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxMikrobus.IsChecked != null && CheckBoxMikrobus.IsChecked.Value)
            {
                Label1.Visibility = Visibility.Visible;
                Label2.Visibility = Visibility.Visible;
                Label3.Visibility = Visibility.Visible;
                TextBoxAM.Visibility = Visibility.Visible;
                TextBoxBM.Visibility = Visibility.Visible;
                TextBoxCM.Visibility = Visibility.Visible;
            }
            else
            {
                Label1.Visibility = Visibility.Hidden;
                Label2.Visibility = Visibility.Hidden;
                Label3.Visibility = Visibility.Hidden;
                TextBoxAM.Visibility = Visibility.Hidden;
                TextBoxBM.Visibility = Visibility.Hidden;
                TextBoxCM.Visibility = Visibility.Hidden;
            }
        }
        private void CheckBoxMikrobus_Click2(object sender, RoutedEventArgs e)
        {
            if (CheckBoxMikrobusR.IsChecked != null && CheckBoxMikrobusR.IsChecked.Value)
            {
                Label1R.Visibility = Visibility.Visible;
                Label2R.Visibility = Visibility.Visible;
                Label3R.Visibility = Visibility.Visible;
                TextBoxAMR.Visibility = Visibility.Visible;
                TextBoxBMR.Visibility = Visibility.Visible;
                TextBoxCMR.Visibility = Visibility.Visible;
            }
            else
            {
                Label1R.Visibility = Visibility.Hidden;
                Label2R.Visibility = Visibility.Hidden;
                Label3R.Visibility = Visibility.Hidden;
                TextBoxAMR.Visibility = Visibility.Hidden;
                TextBoxBMR.Visibility = Visibility.Hidden;
                TextBoxCMR.Visibility = Visibility.Hidden;
            }
        }
    }
}
