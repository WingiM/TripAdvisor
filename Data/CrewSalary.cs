using System;
using System.Collections.Generic;

namespace TripAdvisor.Data
{
    public partial class CrewSalary
    {
        public int Id { get; set; }
        public int? CrewMemberId { get; set; }
        public int Salary { get; set; }

        public virtual Member? CrewMember { get; set; }
    }
}
