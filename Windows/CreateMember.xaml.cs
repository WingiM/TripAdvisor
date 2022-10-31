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
            MessageBox.Show("Участник успешно добавлен");
            DialogResult = true;
        }
    }
}
