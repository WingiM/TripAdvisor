using System;
using System.Collections.Generic;
using System.Globalization;
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
            if (!int.TryParse(PercentTb.Text, out var percent) || CurrentShip is null)
                return;

            int totalCost = 0;
            if (FuelCb.SelectedItem is not null)
            {
                var fuelType = FuelCb.SelectedItem as Fuel;
                totalCost += CurrentShip.FuelTankCapacity * fuelType!.Cost;
            }

            var crew = new List<Member>();
            var members = new List<Member>();
            foreach (Member member in MemberListBox.Items)
            {
                if (member.RoleId == 1)
                    crew.Add(member);
                else
                    members.Add(member);
            }

            foreach (var crewMember in crew)
            {
                totalCost += _context.CrewSalaries.First(x => x.CrewMemberId == crewMember.Id).Salary;
            }

            foreach (FoodDto dto in FoodListBox.Items)
            {
                totalCost += dto.Food!.Cost * dto.Count;
            }

            double pc = 1 + (percent / 100.0);
            var costToComplete = totalCost * pc;
            var ticketCost = 0;
            if (crew.Count != MemberListBox.Items.Count)
                ticketCost = (int)costToComplete / (MemberListBox.Items.Count - crew.Count);

            TravelCostTb.Text = totalCost.ToString();
            TicketCostTb.Text = ticketCost.ToString();
            ProfitTb.Text = ((members.Count * ticketCost) - totalCost).ToString();
        }

        private bool CheckFields()
        {
            if (!int.TryParse(PercentTb.Text, out var percent))
            {
                MessageBox.Show("Процент окупаемости - не число");
                return false;
            }

            if (!DateTime.TryParse(DateBegin.Text, out var beginDate))
            {
                MessageBox.Show("Дата начала - не дата");
                return false;
            }

            if (!DateTime.TryParse(DateEnd.Text, out var endDate))
            {
                MessageBox.Show("Дата конца - не дата");
                return false;
            }

            if (endDate <= beginDate)
            {
                MessageBox.Show("Неправильно введены даты");
                return false;
            }

            if (MemberListBox.Items.Count > CurrentShip.Capacity)
            {
                MessageBox.Show("Корабль переполнен");
                return false;
            }

            var crew = _context.Members.Where(x => x.RoleId == 1).ToList();
            if (crew.Count < CurrentShip.CrewMembersCount)
            {
                MessageBox.Show("Недостаточно членов экипажа");
                return false;
            }

            if (MemberListBox.Items.Count <= 0)
            {
                MessageBox.Show("Необходимо выбрать участников путешествия");
                return false;
            }

            if (CitiesLb.Items.Count <= 0)
            {
                MessageBox.Show("Необходимо выбрать города путешествия");
                return false;
            }

            if (FuelCb.SelectedItem is null)
            {
                MessageBox.Show("Необходимо выбрать тип топлива");
                return false;
            }

            if (CurrentShip is null)
            {
                MessageBox.Show("Необходимо выбрать корабль");
                return false;
            }

            return true;
        }

        private void SaveTrip()
        {
            if (!CheckFields())
                return;
            DateTime.TryParse(DateBegin.Text, out var beginDate);
            DateTime.TryParse(DateEnd.Text, out var endDate);
            var travel = new Trip
            {
                DateFrom = beginDate, DateTo = endDate, ShipName = CurrentShip.Name,
                TicketCost = int.Parse(TicketCostTb.Text)
            };

            foreach (City city in CitiesLb.Items)
            {
                var tripCity = new TripCity { CityId = city.Id, Trip = travel };
                travel.TripCities.Add(tripCity);
            }

            foreach (Member member in MemberListBox.Items)
            {
                var tripMember = new TripMember { Member = member, Trip = travel };
                travel.TripMembers.Add(tripMember);
            }

            _context.Add(travel);
            _context.SaveChanges();
            MessageBox.Show("Удачного пути!");
            NewTrip();
        }

        private void ChooseShip_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new ChooseShip(_context);
            if (window.ShowDialog() == true)
            {
                CurrentShip = window.SelectedShip;
                ShipBlock.Text = CurrentShip.Name;
            }
            else
            {
                MessageBox.Show("Error!", "Error");
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
            else
            {
                MessageBox.Show("Error!", "Error");
            }
        }

        private void CreateCity_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreateCity(_context);
            if (window.ShowDialog() == true)
            {
                MessageBox.Show("ok");
            }
            else
            {
                MessageBox.Show("Error!", "Error");
            }
        }

        private void SelectMember_OnClick(object sender, RoutedEventArgs e)
        {
            List<int> existingMembers = new List<int>();
            foreach (Member member in MemberListBox.Items)
            {
                existingMembers.Add(member.Id);
            }

            var window = new ChooseMember(_context, existingMembers.ToArray());
            if (window.ShowDialog() == true)
            {
                var res = window.SelectedMember;
                MemberListBox.Items.Add(res);
                MessageBox.Show("ok");
            }
            else
            {
                MessageBox.Show("Error!", "Error");
            }

            UpdateInfo();
        }

        private void PercentTb_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateInfo();
        }

        private void ChooseCity_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new ChooseCity(_context);
            if (window.ShowDialog() == true)
            {
                var res = window.SelectedCity;
                CitiesLb.Items.Add(res);
                MessageBox.Show("ok");
            }
            else
            {
                MessageBox.Show("Error!", "Error");
            }
        }

        private void SaveTripButton_OnClick(object sender, RoutedEventArgs e)
        {
            SaveTrip();
        }

        private void CreateNewTripButton_OnClick(object sender, RoutedEventArgs e)
        {
            NewTrip();
        }

        private void NewTrip()
        {
            ShipBlock.Text = "";
            PercentTb.Text = "0";
            TravelCostTb.Text = "0";
            TicketCostTb.Text = "0";
            ProfitTb.Text = "0";
            CurrentShip = null;
            FuelCb.SelectedItem = null;
            DateBegin.Text = DateTime.MinValue.ToString(CultureInfo.InvariantCulture);
            DateEnd.Text = DateTime.MinValue.ToString(CultureInfo.InvariantCulture);
            FoodListBox.Items.Clear();
            MemberListBox.Items.Clear();
            CitiesLb.Items.Clear();
        }

        private void AddFoodButton_OnClick(object sender, RoutedEventArgs e)
        {
            var exsistingFood = new List<int>();

            foreach (FoodDto tripFood in FoodListBox.Items)
            {
                exsistingFood.Add(tripFood.Food!.Id);
            }

            var window = new AddFood(_context, exsistingFood.ToArray());
            if (window.ShowDialog() == true)
            {
                var res = window.ChosenTripFood;
                FoodListBox.Items.Add(res);
                MessageBox.Show("ok");
            }

            UpdateInfo();
        }
    }
}