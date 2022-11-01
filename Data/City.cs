using System;
using System.Collections.Generic;

namespace TripAdvisor.Data
{
    public partial class City
    {
        public City()
        {
            TripCities = new HashSet<TripCity>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Image { get; set; }

        public virtual ICollection<TripCity> TripCities { get; set; }

        public override string ToString() => Name;
    }
}
