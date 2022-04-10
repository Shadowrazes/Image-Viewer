using Avalonia.Controls;
using Image_Viewer.Models;
using Avalonia.Interactivity;
using Image_Viewer.ViewModels;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Avalonia.Input;
using Avalonia;
using Avalonia.LogicalTree;
using Avalonia.Layout;
using Avalonia.Controls.Primitives;

namespace Image_Viewer.Views
{
    public partial class MainWindow : Window
    {
        private Carousel _Slider;
        private Button _Back;
        private Button _Next;

        private void Init()
        {
            _Slider = this.FindControl<Carousel>("Slider");
            _Back = this.FindControl<Button>("Back");
            _Next = this.FindControl<Button>("Next");
        }
        public MainWindow()
        {
            InitializeComponent();
            Init();
            _Back.Click += (s, e) => _Slider.Previous();
            _Next.Click += (s, e) => _Slider.Next();
        }

        private void ChangedSelectedNode(object sender, PointerReleasedEventArgs e)
        {
            string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            TreeViewItem treeViewItem = sender as TreeViewItem;
            Node selectedNode = treeViewItem.DataContext as Node;

            if (allowedExtensions.Any(selectedNode.NodeName.ToLower().EndsWith))
            {
                string path = selectedNode.FullPath.Substring(0, selectedNode.FullPath.IndexOf(selectedNode.NodeName));
                var files = Directory.EnumerateFiles(path)
                    .Where(file => allowedExtensions.Any(file.ToLower().EndsWith))
                    .ToList();
                files.Remove(selectedNode.FullPath);
                var context = this.DataContext as MainWindowViewModel;
                context.RefreshImageList(files, selectedNode.FullPath);
            }
        }

        private void ClickForLoadNodes(object sender, TemplateAppliedEventArgs e)
        {
            ContentControl treeViewItem = sender as ContentControl;
            Node selectedNode = treeViewItem.DataContext as Node;
            selectedNode.LoadNext();
        }
    }
}
