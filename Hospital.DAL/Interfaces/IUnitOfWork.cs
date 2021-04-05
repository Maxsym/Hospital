using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hospital.DAL.Domains;

namespace Hospital.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
