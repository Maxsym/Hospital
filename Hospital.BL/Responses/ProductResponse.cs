using System;
using System.Collections.Generic;
using System.Text;
using Hospital.DAL.Domains;

namespace Hospital.BL.Responses
{
    public class ProductResponse : BaseResponse<Product>
    {
        public ProductResponse(Product product) : base(product) { }

        public ProductResponse(string message) : base(message) { }
    }
}
