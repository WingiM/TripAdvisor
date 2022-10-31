using System;
using System.Collections.Generic;

namespace TripAdvisor.Data
{
    public partial class TripMember
    {
        public int Id { get; set; }
        public int? TripId { get; set; }
        public int? MemberId { get; set; }

        public virtual Member? Member { get; set; }
        public virtual Trip? Trip { get; set; }
    }
}
