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
    /// Interaction logic for MembersAddWindow.xaml
    /// </summary>
    /// 
    public partial class CreateMember : Window
    {
        private ApplicationContext _context;
        public List<Role> Roles { get; set; }
        public CreateMember(ApplicationContext context)
        {
            InitializeComponent();
            this.DataContext = this;
            _context = context;

            Roles = _context.Roles.ToList();
        }

        private void CreateMember_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(AgeTb.Text, out var age))
            {
                MessageBox.Show("Возраст должен быть числом");
                DialogResult = false;
            }
            else if (RoleCb.SelectedItem is null)
            {
                MessageBox.Show("Не выбрана роль");
                DialogResult = false;
            }
            else if(string.IsNullOrEmpty(NameTb.Text))
            {
                MessageBox.Show("Не правильно введено имя!");
                DialogResult = false;
            }
            else if (string.IsNullOrEmpty(GenderTb.Text))
            {
                MessageBox.Show("Не правильно введена информация о пол!");
                DialogResult = false;
            }
            else
            {
                var member = new Member
                {
                    Name = NameTb.Text,
                    Age = age,
                    Gender = GenderTb.Text,
                    RoleId = (RoleCb.SelectedItem as Role).Id
                };
                _context.Add(member);
                _context.SaveChanges();
                MessageBox.Show("Участник успешно добавлен");
                DialogResult = true;
            }
        }
    }
}
