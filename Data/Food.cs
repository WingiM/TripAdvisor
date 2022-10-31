using System;
using System.Collections.Generic;

namespace TripAdvisor.Data
{
    public partial class Food
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Cost { get; set; }
    }
}
