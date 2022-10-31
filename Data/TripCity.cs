using System;
using System.Collections.Generic;

namespace TripAdvisor.Data
{
    public partial class TripCity
    {
        public int Id { get; set; }
        public int? TripId { get; set; }
        public int? CityId { get; set; }
        public byte[] StopDuration { get; set; } = null!;

        public virtual City? City { get; set; }
        public virtual Trip? Trip { get; set; }
    }
}
