using System;
using System.Collections.Generic;

namespace WorkOutAPI.Model
{
    public partial class Schedule
    {
        public Schedule()
        {
            ScheduleDailyExercis = new HashSet<ScheduleDailyExercis>();
        }

        public int Id { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public byte[]? Rowversion { get; set; }

        public virtual ICollection<ScheduleDailyExercis> ScheduleDailyExercis { get; set; }
    }
}
