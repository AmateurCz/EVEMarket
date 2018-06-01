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
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
            SimpleIoc.Default.Register<IStaticData, DbStaticData>();

            base.OnStartup(e);
        }
    }
}