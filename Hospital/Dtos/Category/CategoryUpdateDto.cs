using System.ComponentModel.DataAnnotations;

namespace Hospital.API.Dtos.Category
{
    public class CategoryUpdateDto
    {
        [Required]
        [MaxLength(60)]
        public string Code { get; set; }
        [Required]
        [MaxLength(256)]
        public string Description { get; set; }
    }
}
