using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace TreeView
{
    class StringToImageConverter : IValueConverter
    {
        public static StringToImageConverter converter=new StringToImageConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = (string)value;
            if (path==null)
                return null;
            string name = MainWindow.GetFileOrFolder(path);

            string image = "images/file.png";

            if (string.IsNullOrEmpty(name))
                image = "images/drive.png";
            else if (new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))
                image = "images/folder_closed.png";
            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
