using System.Windows;
using DavesToolkit.Logging;
using EVEMarket.Data.Providers;
using EVEMarket.WPF.Data.Providers;
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
            LogPump.AddLogger(new ConsoleLogger());
            SimpleIoc.Default.Register<IStaticData, StaticDb>();
        }
    }
}