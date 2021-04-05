using System;
using System.ComponentModel.DataAnnotations;
using Hospital.BL.Enums;

namespace Hospital.API.Dtos.Product
{
    public class ProductReadDto
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasurement { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Barcode { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }

}
