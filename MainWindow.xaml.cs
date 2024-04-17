using System.Windows;
using СlassicCheckers.ViewModels;

namespace СlassicCheckers
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModels();
        }
    }
}
