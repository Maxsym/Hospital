using System;
using System.Collections.Generic;
using System.Text;
using Hospital.DAL.Domains;
using Hospital.DAL.Interfaces;

namespace Hospital.DAL.Implementation
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(HospitalDbContext context) : base(context){}
    }
}
