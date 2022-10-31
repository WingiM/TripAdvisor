using System;
using System.Collections.Generic;

namespace TripAdvisor.Data
{
    public partial class Role
    {
        public Role()
        {
            Members = new HashSet<Member>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Member> Members { get; set; }

        public override string ToString() => Name;
    }
}
