using System.Windows;
using EVEMarket.Data.Providers;
using EVEMarket.WPF.Data.Providers;
using GalaSoft.MvvmLight.Ioc;
using NLog;
using NLog.Layouts;
using NLog.Targets;

namespace EVEMarket.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string LoggingPath => "Log.txt";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var config = new NLog.Config.LoggingConfiguration();
            var fileLayout = Layout.FromString("${longdate}|${level}|${logger}| ${message}");
            var consoleLayout = Layout.FromString("${longdate} ${message}");

            var logfile = new FileTarget("FileLogger") { FileName = LoggingPath, Layout = fileLayout };
            var logconsole = new DebuggerTarget("Debuger Logger") { Layout = consoleLayout };

            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);

            LogManager.Configuration = config;

            SimpleIoc.Default.Register<IStaticData, StaticDb>();
        }
    }
}