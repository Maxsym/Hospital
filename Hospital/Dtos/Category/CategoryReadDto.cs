using System.ComponentModel.DataAnnotations;

namespace Hospital.API.Dtos.Category
{
    public class CategoryReadDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string Code { get; set; }
        [Required]
        [MaxLength(256)]
        public string Description { get; set; }
    }
}
