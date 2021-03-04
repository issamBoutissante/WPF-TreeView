using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace TreeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(var drive in Directory.GetLogicalDrives())
            {
                var item = new TreeViewItem();
                item.Header = drive;
                item.Tag = drive;
                item.Items.Add(null);
                item.Expanded += ExpandFolder;
                this.FolderView.Items.Add(item);
            }
        }
        private void ExpandFolder(object sender,EventArgs e)
        {
            var item = (TreeViewItem)sender;
            if (item.Items.Count != 1 || item.Items[0] != null)
                return;
            item.Items.Clear();
            string fullPath = (string)item.Tag;
            #region Get The Folders
            List<string> derictories = new List<string>();
            try
            {
                string[] dirs = Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                    derictories.AddRange(dirs);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            derictories.ForEach(dir =>
            {
                TreeViewItem subItem = new TreeViewItem();
                subItem.Header = GetFileOrFolder(dir);
                subItem.Tag = dir;
                subItem.Items.Add(null);
                subItem.Expanded += ExpandFolder;
                item.Items.Add(subItem);
            });
            #endregion
            #region Get The Files
            List<string> Files = new List<string>();
            try
            {
                string[] fs = Directory.GetFiles(fullPath);
                if (fs.Length > 0)
                    derictories.AddRange(fs);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            derictories.ForEach(filePath =>
            {
                TreeViewItem subItem = new TreeViewItem();
                subItem.Header = GetFileOrFolder(filePath);
                subItem.Tag = filePath;
                item.Items.Add(subItem);
            });
            #endregion
        }
        public static string GetFileOrFolder(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;
            var normalPath = path.Replace('/', '\\');
            var lastIndex = normalPath.LastIndexOf('\\');
            if (lastIndex <= 0)
                return path;
            return path.Substring(lastIndex + 1);
        }
    }
}
