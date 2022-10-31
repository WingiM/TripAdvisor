using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TripAdvisor.Data;
using TripAdvisor.Windows;

namespace TripAdvisor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApplicationContext _context;
        public List<Fuel> FuelTypes { get; set; }
        public List<City>? Cities { get; set; }
        public List<Food>? Foods { get; set; }
        public List<Member>? Members { get; set; }

        public Ship? CurrentShip { get; set; }

        public MainWindow()
        {
            _context = new();
            InitializeComponent();
            this.DataContext = this;

            FuelTypes = _context.Fuels.ToList();
        }

        private void ChooseShip_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new ChooseShip(_context);
            if (window.ShowDialog() == true)
            {
                CurrentShip = _context.Ships.First(x => x.Id == 1);
                ShipBlock.Text = CurrentShip.Name;
            }
        }

        private void CreateMember_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreateMember(_context);
            if (window.ShowDialog() == true)
            {
                MessageBox.Show("ok");
            }
        }

        private void CreateCity_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreateCity(_context);
            if (window.ShowDialog() == true)
            {
                MessageBox.Show("ok");
            }
        }
    }
}
