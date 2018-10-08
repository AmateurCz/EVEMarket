using System;
using System.Windows;
using EVEMarket.WPF.Interfaces;

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

            var vm = this.DataContext as VmWithInitialization;
            await vm.InitializeAsync();

            this.IsEnabled = true;
        }
    }
}