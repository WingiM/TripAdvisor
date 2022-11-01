using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for ChooseCity.xaml
    /// </summary>
    public partial class ChooseCity : Window
    {
        private readonly ApplicationContext _context;
        public List<City> Cities;
        public City SelectedCity;
        public ChooseCity(ApplicationContext context)
        {
            InitializeComponent();
            this.DataContext = this;

            _context = context;

            Cities = _context.Cities.AsNoTracking().ToList();
            CitiesDg.ItemsSource = Cities;
            var path = Directory.GetParent($"{Directory.GetCurrentDirectory()}")?.FullName;
            var pathPArent = Directory.GetParent(path!)?.FullName;
            var savePath = Directory.GetParent(pathPArent!)?.FullName + @"\Images\Cities\";

            foreach (var city in Cities)
            {
                city.Image = savePath + @"\" + city.Name + ".jpg";
            }
        }

        private void SelectCity_OnClick(object sender, RoutedEventArgs e)
        {
            SelectedCity = CitiesDg.SelectedItem as City;
            if(SelectedCity is not null)
            {
                DialogResult = true;
            }
            else
            {
                DialogResult = false;
            }
        }
    }
}
