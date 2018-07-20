using System;
using System.Windows.Controls;

namespace EVEMarket.WPF
{
    /// <summary>
    /// Interaction logic for MarketDetails.xaml
    /// </summary>
    public partial class MarketDetails : Page
    {
        public MarketDetails()
        {
            InitializeComponent();
        }

        protected override async void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            //this.IsEnabled = false;

            //this.StatusText.Text = "Loading ...";

            //var vm = this.DataContext as MainViewModel;
            //await vm.Initialize();

            //this.IsEnabled = true;

            //this.StatusText.Text = "Ready";
        }
    }
}