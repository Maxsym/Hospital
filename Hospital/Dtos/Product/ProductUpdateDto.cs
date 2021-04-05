using System;
using System.ComponentModel.DataAnnotations;
using Hospital.BL.Enums;

namespace Hospital.API.Dtos.Product
{
    public class ProductUpdateDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string Code { get; set; }
        [Required]
        [MaxLength(256)]
        public string Description { get; set; }
        [Required]
        [Range(1, 5)]
        public int UnitOfMeasurement { get; set; }
        public DateTime ExpireDate { get; set; }
        [Required]
        [RegularExpression("^\\d{12}")]
        public string Barcode { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
