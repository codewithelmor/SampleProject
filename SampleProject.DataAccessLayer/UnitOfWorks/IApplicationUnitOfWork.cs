using SampleProject.DataAccessLayer.Application;

namespace SampleProject.DataAccessLayer.UnitOfWorks
{
    public interface IApplicationUnitOfWork : IUnitOfWork<ApplicationDbContext>
    {
    }
}