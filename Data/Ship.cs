using System;
using System.Collections.Generic;

namespace TripAdvisor.Data
{
    public partial class Ship
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Image { get; set; }
        public int Capacity { get; set; }
        public int CrewMembersCount { get; set; }
        public int FuelTankCapacity { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
