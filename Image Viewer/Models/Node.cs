using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;

namespace Image_Viewer.Models
{
    public class Node
    {
        private bool isHashed;
        public ObservableCollection<Node>? FilesAndFolders { get; set; }

        public string NodeName { get; }
        public string FullPath { get; }

        public Node(string _FullPath, bool isDisk)
        {
            FilesAndFolders = new ObservableCollection<Node>();
            FullPath = _FullPath;
            if (!isDisk)
                NodeName = Path.GetFileName(_FullPath);
            else
                NodeName = "Диск " + _FullPath.Substring(0, _FullPath.IndexOf(":"));
            isHashed = false;
        }

        public void LoadNext()
        {
            foreach(Node file in FilesAndFolders)
            {
                if (!file.isHashed)
                {
                    file.GetFilesAndFolders();
                }
            }
        }

        public void GetFilesAndFolders()
        {
            if (!isHashed)
            {
                try
                {
                    IEnumerable<string> subdirs = Directory.EnumerateDirectories(FullPath, "*", SearchOption.TopDirectoryOnly);
                    foreach (string dir in subdirs)
                    {
                        Node thisnode = new Node(dir, false);
                        FilesAndFolders.Add(thisnode);
                    }

                    string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    IEnumerable<string> files = Directory.EnumerateFiles(FullPath)
                        .Where(file => allowedExtensions.Any(file.ToLower().EndsWith))
                        .ToList();

                    foreach (string file in files)
                    {
                        FilesAndFolders.Add(new Node(file, false));
                    }
                }
                catch
                {

                }
                isHashed = true;
            }
        }
    }
}
