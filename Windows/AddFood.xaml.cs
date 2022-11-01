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
using System.Windows.Shapes;
using TripAdvisor.Data;

namespace TripAdvisor.Windows
{
    /// <summary>
    /// Interaction logic for AddFood.xaml
    /// </summary>
    public partial class AddFood : Window
    {
        public TripFood? ChosenTripFood { get; set; }
        public List<Food> AvailableFood { get; set; }

        private readonly ApplicationContext _context;

        public AddFood(ApplicationContext context, int[] exsistingFood)
        {
            InitializeComponent();

            _context = context;
            AvailableFood = _context.Foods.Where(x => !exsistingFood.Contains(x.Id)).ToList();

            FoodDataGrid.ItemsSource = AvailableFood;
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            ChosenTripFood = new TripFood 
            { 
                Count = int.Parse(CountTb.Text), 
                Food = FoodDataGrid.SelectedItem as Food 
            };
            DialogResult = true;
        }
    }
}
