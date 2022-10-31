using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TripAdvisor.Data;

namespace TripAdvisor;

public partial class ChooseShip : Window
{
    public List<Ship> Ships { get; set; }
    public Ship SelectedShip { get; set; }

    public ChooseShip(ApplicationContext context)
    {
        InitializeComponent();
        this.DataContext = this;
        
        // TODO: show images and names in window to select
        Ships = context.Ships.ToList();
    }

    private void Ok_OnClick(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }
}