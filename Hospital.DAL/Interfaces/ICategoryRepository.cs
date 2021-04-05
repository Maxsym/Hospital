using System;
using System.Collections.Generic;
using System.Text;
using Hospital.DAL.Domains;
using Hospital.DAL.Implementation;

namespace Hospital.DAL.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
    }
}
