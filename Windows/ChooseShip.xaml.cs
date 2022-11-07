using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using TripAdvisor.Data;

namespace TripAdvisor.Windows;

public partial class ChooseShip : Window
{
    public List<Ship> Ships { get; set; }
    public Ship SelectedShip { get; set; }

    public ChooseShip(ApplicationContext context)
    {
        InitializeComponent();
        this.DataContext = this;
        
        Ships = context.Ships.AsNoTracking().ToList();
        var path = Directory.GetParent($"{Directory.GetCurrentDirectory()}")?.FullName;
        var pathPArent = Directory.GetParent(path!)?.FullName;
        var savePath = Directory.GetParent(pathPArent!)?.FullName + @"\Images\Ships\";

        foreach(var ship in Ships)
        {
            ship.Image = savePath + @"\" + ship.Name + ".jpg";
        }
    }

    private void SelectButton_OnClick(object sender, RoutedEventArgs e)
    {
        SelectedShip = ShipsDg.SelectedItem as Ship;
        if(SelectedShip is not null)
        {
            DialogResult = true;
        }
        else
        {
            DialogResult = false;
        }
    }
}