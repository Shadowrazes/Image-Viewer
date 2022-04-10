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
                if (drive.Name.Substring(0, drive.Name.IndexOf(":")) != "C")
                {
                    allDrivesNames.Add(drive.Name);
                    Node rootNode = new Node(drive.Name.Substring(0, drive.Name.IndexOf(":")));
                    rootNode.FilesAndFolders = GetFilesAndFolders(drive.Name);
                    Items.Add(rootNode);
                }
            }
        }

        public ObservableCollection<Node>? GetFilesAndFolders(string path)
        {
            ObservableCollection<Node> filesAndFolders = new ObservableCollection<Node>();

            //if (!allDrivesNames.Any(str => str == strPath))
            //{
            //    FileAttributes attributes = File.GetAttributes(strPath);
            //    if ((attributes & FileAttributes.System) == FileAttributes.System)
            //    {
            //        return subfolders;
            //    }
            //}

            try
            {
                IEnumerable<string> subdirs = Directory.EnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly);
                foreach (string dir in subdirs)
                {
                    Node thisnode = new Node(dir);
                    thisnode.FilesAndFolders = new ObservableCollection<Node>();

                    thisnode.FilesAndFolders = GetFilesAndFolders(dir);
                    if(thisnode.FilesAndFolders != null)
                        filesAndFolders.Add(thisnode);
                }

                string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                IEnumerable<string> files = Directory.EnumerateFiles(path)
                    .Where(file => allowedExtensions.Any(file.ToLower().EndsWith))
                    .ToList();

                foreach (string file in files)
                {
                    filesAndFolders.Add(new Node(file));
                }
            }
            catch
            {
                return null;
            }
            if(filesAndFolders.Count != 0)
                return filesAndFolders;
            else
                return null;
        }
    }
}
