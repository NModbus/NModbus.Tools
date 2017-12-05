using System.Windows;
using NModbus.Tools.FrameParser;
using NModbus.Tools.Model;

namespace NModbus.Tools.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            //TODO: create an awesome UI for this once we have more than one tool.
            var factory = new FrameParserToolFactory();

            var context = new ToolCreationContext(factory);

            var tool = factory.Create(context);

            ContentContainer.Children.Add(tool.View);
        }

        private void ExitMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
