using System;
using System.Collections.Generic;

namespace TripAdvisor.Data
{
    public partial class Member
    {
        public Member()
        {
            CrewSalaries = new HashSet<CrewSalary>();
            TripMembers = new HashSet<TripMember>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public int? RoleId { get; set; }
        public string Gender { get; set; } = null!;

        public virtual Role? Role { get; set; }
        public virtual ICollection<CrewSalary> CrewSalaries { get; set; }
        public virtual ICollection<TripMember> TripMembers { get; set; }
    }
}
