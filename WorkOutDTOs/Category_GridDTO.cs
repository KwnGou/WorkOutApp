using System.ComponentModel.DataAnnotations;

namespace WorkOutDTOs
{
    public class Category_GridDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTimeOffset? Modified { get; set; }

        public byte[]? Rowversion { get; set; }
    }
}