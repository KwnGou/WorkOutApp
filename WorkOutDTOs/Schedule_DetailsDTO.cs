using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOutDTOs
{
    public class Schedule_DailyExercise_ItemDTO
    {
        [Key]
        public int Id { get; set; }

        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }

        [Required]
        public DateTimeOffset Date { get; set; }

        public byte[]? Rowversion { get; set; }
    }

    public class Schedule_DetailsDTO
    {
        [Key]
        public int Id { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public List<Schedule_DailyExercise_ItemDTO> Exercises { get; set; }

        public byte[]? Rowversion { get; set; }
    }
}
