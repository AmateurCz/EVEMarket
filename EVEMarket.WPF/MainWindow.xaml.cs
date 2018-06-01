using System;
using System.Windows;
using EVEMarket.WPF.ViewModel;

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