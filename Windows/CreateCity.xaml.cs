using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TripAdvisor.Data;

namespace TripAdvisor.Windows
{
    /// <summary>
    /// Interaction logic for CreateCity.xaml
    /// </summary>
    public partial class CreateCity : Window
    {
        private string _savedToPath;
        private readonly ApplicationContext _context;

        public CreateCity(ApplicationContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private void ImageDownload_Click(object sender, RoutedEventArgs e)
        {
            string filePath;
            OpenFileDialog openFileDialog = new();
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    filePath = openFileDialog.FileName;
                    var path = Directory.GetParent($"{Directory.GetCurrentDirectory()}")?.FullName;
                    var pathParent = Directory.GetParent(path!)?.FullName;
                    var savePath = Directory.GetParent(pathParent!)?.FullName + @"\Images\Cities\";
                    _savedToPath = savePath + $"{System.IO.Path.GetFileNameWithoutExtension(filePath)}.jpg";
                    File.Copy(filePath, _savedToPath);
                    MessageBox.Show("Изображение добавлено");
                }
                catch
                {
                    MessageBox.Show("Изображение таким именем уже существует!");
                }
            }
        }

        private void CreateCity_Click(object sender, RoutedEventArgs e)
        {
            var city = new City { Name = CityName.Text, Image = System.IO.Path.GetFileName(_savedToPath) };
            _context.Add(city);
            _context.SaveChanges();
            MessageBox.Show("Город успешно добавлен");
            DialogResult = true;
        }
    }
}