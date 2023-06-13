using System;
using System.Collections.Generic;

namespace WorkOutAPI.Model
{
    public partial class ScheduleDailyExercis
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int ExerciseId { get; set; }
        public DateTimeOffset Date { get; set; }
        public byte[]? Rowversion { get; set; }

        public virtual Exercis Exercise { get; set; } = null!;
        public virtual Schedule Schedule { get; set; } = null!;
    }
}
