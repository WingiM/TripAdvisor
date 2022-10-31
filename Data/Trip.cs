using System;
using System.Collections.Generic;

namespace TripAdvisor.Data
{
    public partial class Trip
    {
        public Trip()
        {
            TripCities = new HashSet<TripCity>();
            TripMembers = new HashSet<TripMember>();
        }

        public int Id { get; set; }
        public string ShipName { get; set; } = null!;
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int? TicketCost { get; set; }

        public virtual ICollection<TripCity> TripCities { get; set; }
        public virtual ICollection<TripMember> TripMembers { get; set; }
    }
}
