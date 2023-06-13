using System;
using System.Collections.Generic;

namespace WorkOutAPI.Model
{
    public partial class Exercis
    {
        public Exercis()
        {
            ScheduleDailyExercis = new HashSet<ScheduleDailyExercis>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CategoryId { get; set; }
        public byte[]? Rowversion { get; set; }
        public bool IsAerobic { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<ScheduleDailyExercis> ScheduleDailyExercis { get; set; }
    }
}
