using System.Windows;
using System.Windows.Controls;
using NModbus.Tools.SlaveExplorer.View;
using NModbus.Tools.SlaveExplorer.ViewModel;
using Xceed.Wpf.AvalonDock.Layout;

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
        }

        private void ExitMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add<TViewModel, TView>()
            where TViewModel : new()
            where TView : UserControl, new()
        {
            var viewModel = new TViewModel();

            var view = new TView()
            {
                DataContext = viewModel
            };

            var layoutDocument = new LayoutDocument
            {
                Content = view
            };

            MainDocumentPane.Children.Add(layoutDocument);
            MainDocumentPane.SelectedContentIndex = MainDocumentPane.ChildrenCount - 1;
        }

        private void MenuItemNewSlaveExplorer_Click(object sender, RoutedEventArgs e)
        {
            Add<SlaveExplorerViewModel, SlaveExplorerView>();
        }
    }
}
