using System.Windows;
using RandomPacketSelection.Model;
using RandomPacketSelection.View;
using RandomPacketSelection.ViewModel;

namespace RandomPacketSelection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly UserVM VM;

        public MainWindow()
        {
            InitializeComponent();
            VM = new UserVM();
            this.DataContext = VM;
        }
    }
}
