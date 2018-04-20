using EVEMarket.WPF.Data;
using EVEMarket.WPF.ViewModel;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows;

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

        protected override async void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.IsEnabled = false;
            this.StatusText.Text = "Loading ...";

            var vm = this.DataContext as MainViewModel;
            await vm.Initialize();

            this.IsEnabled = true;
            this.StatusText.Text = "Ready";
        }
    }
}
