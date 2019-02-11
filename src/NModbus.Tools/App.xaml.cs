using GalaSoft.MvvmLight.Threading;
using System.Windows;

namespace NModbus.Tools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DispatcherHelper.Initialize();
        }
    }
}
