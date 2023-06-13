using System.ComponentModel.DataAnnotations;

namespace WorkOutDTOs
{
    public class Exercise_GridDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public byte[]? Rowversion { get; set; }

        [MaxLength(50)]
        public string? NewCategory { get; set; } 

        public Boolean? IsAerobic { get; set; }
    }
}
