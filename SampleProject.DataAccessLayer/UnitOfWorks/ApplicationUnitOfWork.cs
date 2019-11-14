using SampleProject.DataAccessLayer.Application;

namespace SampleProject.DataAccessLayer.UnitOfWorks
{
    public class ApplicationUnitOfWork : UnitOfWork<ApplicationDbContext>, IApplicationUnitOfWork
    {
        public ApplicationUnitOfWork(ApplicationDbContext context) : base(context)
        {
        }
    }
}