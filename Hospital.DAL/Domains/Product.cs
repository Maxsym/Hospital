using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Hospital.BL.Enums;

namespace Hospital.DAL.Domains
{
    public class Product
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
        public EUnitOfMeasurement UnitOfMeasurement { get; set; }
        public DateTime ExpireDate { get; set; }
        [Required]
        [RegularExpression("^\\d{12}")]
        public string Barcode { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
