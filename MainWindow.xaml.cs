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

        public Ship? CurrentShip { get; set; }

        public MainWindow()
        {
            _context = new();
            InitializeComponent();
            this.DataContext = this;

            FuelTypes = _context.Fuels.ToList();
        }

        private void UpdateInfo()
        {
            if (!int.TryParse(PercentTb.Text, out var percent) || CurrentShip is null || FuelCb.SelectedItem is null)
                return;
            
            int totalCost = 0;
            var fuelType = FuelCb.SelectedItem as Fuel;
            totalCost += CurrentShip.FuelTankCapacity * fuelType!.Cost;
            var crew = _context.Members.Where(x => x.RoleId == 1).ToList();
            foreach (var crewMember in crew)
            {
                totalCost += _context.CrewSalaries.First(x => x.CrewMemberId == crewMember.Id).Salary;
            }

            foreach (Food food in FoodListBox.Items)
            {
                totalCost += food.Cost;
            }

            var costToComplete = totalCost *= percent / 100;
            var ticketCost = costToComplete / (MemberListBox.Items.Count - crew.Count);
            
            TravelCostTb.Text = totalCost.ToString();
            TicketCostTb.Text = ticketCost.ToString();
        }

        private void SaveTrip()
        {
            if (!int.TryParse(PercentTb.Text, out var percent))
            {
                MessageBox.Show("Процент окупаемости - не число");
            }

            if (!DateTime.TryParse(DateBegin.Text, out var beginDate))
                MessageBox.Show("Дата начала - не дата");

            if (!DateTime.TryParse(DateBegin.Text, out var endDate))
                MessageBox.Show("Дата конца - не дата");

            if (endDate <= beginDate)
                MessageBox.Show("Неправильно введены даты");

            if (MemberListBox.Items.Count > CurrentShip.Capacity)
            {
                MessageBox.Show("Корабль переполнен");
            }

            var crew = _context.Members.Where(x => x.RoleId == 1).ToList();
            if (crew.Count < CurrentShip.CrewMembersCount)
                MessageBox.Show("Недостаточно членов экипажа");
        }

        private void ChooseShip_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new ChooseShip(_context);
            if (window.ShowDialog() == true)
            {
                CurrentShip = window.SelectedShip;
                ShipBlock.Text = CurrentShip.Name;
            }
            UpdateInfo();
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

        private void SelectMember_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new ChooseMember(_context);
            if (window.ShowDialog() == true)
            {
                var res = window.SelectedMember;
                MemberListBox.Items.Add(res);
                MessageBox.Show("ok");
            }
            UpdateInfo();
        }

        private void ChooseCity_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new ChooseCity(_context);
            if(window.ShowDialog() == true)
            {
                var res = window.SelectedCity;
                CitiesLb.Items.Add(res);
                MessageBox.Show("ok");
            }
        }
    }
}