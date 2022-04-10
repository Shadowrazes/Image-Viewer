using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using System.Windows.Input;
using ReactiveUI;
using System.IO;
using System.Diagnostics;
using Image_Viewer.Models;

namespace Image_Viewer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Node> Items { get; }

        List<string> allDrivesNames;

        public MainWindowViewModel()
        {
            allDrivesNames = new List<string>();
            Items = new ObservableCollection<Node>();
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in allDrives)
            {
                allDrivesNames.Add(drive.Name);
                Node rootNode = new Node(drive.Name, true); //drive.Name.Substring(0, drive.Name.IndexOf(":"))
                rootNode.GetFilesAndFolders();
                Items.Add(rootNode);
            }
        }
    }
}
