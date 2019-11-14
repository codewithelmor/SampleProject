using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SampleProject.DataAccessLayer.UnitOfWorks
{
    public interface IUnitOfWork<out TContext> : IDisposable where TContext : DbContext
    {
        TContext GetDbContext();
        Task Commit();
        void Rollback();
    }
}