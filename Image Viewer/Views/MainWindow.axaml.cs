using Avalonia.Controls;
using Image_Viewer.Models;
using Avalonia.Interactivity;

namespace Image_Viewer.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectedNodeChanged(object sender, SelectionChangedEventArgs e)
        {
            Node selectedNode = e.AddedItems[0] as Node;
            selectedNode.LoadNext();
        }
    }
}
