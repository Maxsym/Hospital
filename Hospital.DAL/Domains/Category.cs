using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hospital.DAL.Domains
{
    public class Category
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string Code { get; set; }
        [Required]
        [MaxLength(256)]
        public string Description { get; set; }
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
