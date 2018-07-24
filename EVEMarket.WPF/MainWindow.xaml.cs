using System;
using System.Windows;
using EVEMarket.WPF.Interfaces;
using GalaSoft.MvvmLight.Ioc;

namespace EVEMarket.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, NavigationHandler
    {
        public MainWindow()
        {
            InitializeComponent();
            SimpleIoc.Default.Register<NavigationHandler>(() => this);
        }

        public void NavigateTo(Type target)
        {
            var page = Activator.CreateInstance(target);
            this.NavigationFrame.Navigate(page);
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