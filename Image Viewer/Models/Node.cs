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
        public ObservableCollection<Node>? FilesAndFolders { get; set; }

        public string NodeName { get; }
        public string FullPath { get; }

        public Node(string _FullPath)
        {
            FullPath = _FullPath;
            NodeName = Path.GetFileName(_FullPath);
        }
    }
}
