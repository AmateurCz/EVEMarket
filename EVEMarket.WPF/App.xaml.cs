using System.Windows;
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
            var fileLog = new FileTarget("FileLogger") { FileName = LoggingPath, Layout = fileLayout };
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, fileLog);

            var consoleLayout = Layout.FromString("${longdate} ${message}");
            var logconsole = new DebuggerTarget("Debuger Logger") { Layout = consoleLayout };
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, logconsole);

            LogManager.Configuration = config;
        }
    }
}