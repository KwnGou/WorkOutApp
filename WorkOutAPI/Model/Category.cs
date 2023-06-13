using System;
using System.Collections.Generic;

namespace WorkOutAPI.Model
{
    public partial class Category
    {
        public Category()
        {
            Exercis = new HashSet<Exercis>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTimeOffset? Modified { get; set; }
        public byte[]? Rowversion { get; set; }

        public virtual ICollection<Exercis> Exercis { get; set; }
    }
}
