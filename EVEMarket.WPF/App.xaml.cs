using System.Windows;
using EVEMarket.DataProviders;
using EVEMarket.WPF.DataProviders;
using GalaSoft.MvvmLight.Ioc;

namespace EVEMarket.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SimpleIoc.Default.Register<IStaticData, DbStaticData>();
        }
    }
}