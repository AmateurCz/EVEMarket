using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace EVEMarket.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var eveLib = new eZet.EveLib.Core.EveLib();
            var central = new eZet.EveLib.EveCentralModule.EveCentral();

            //central.GetHistory();
            var result = central.GetQuicklook(new eZet.EveLib.EveCentralModule.EveCentralOptions()
            {
                Items = new int[] { 17478 },
            });
        }
    }
}
