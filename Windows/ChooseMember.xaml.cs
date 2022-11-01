using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using TripAdvisor.Data;

namespace TripAdvisor.Windows;

public partial class ChooseMember : Window
{
    private ApplicationContext _context;
    public Member SelectedMember { get; set; }
    public List<Member> Members { get; set; }
    public ChooseMember(ApplicationContext context, int[] existingMembers)
    {
        _context = context;
        InitializeComponent();
        this.DataContext = this;
        _context.Roles.Load();
        Members = _context.Members.Where(x => !existingMembers.Contains(x.Id)).ToList();
    }

    private void OkButton_OnClick(object sender, RoutedEventArgs e)
    {
        SelectedMember = MainDataGrid.SelectedItem as Member;
        DialogResult = true;
    }
}