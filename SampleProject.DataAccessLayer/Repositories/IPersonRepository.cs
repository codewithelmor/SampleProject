using SampleProject.DataAccessLayer.Application;
using SampleProject.DomainObject.Application;

namespace SampleProject.DataAccessLayer.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person, ApplicationDbContext>
    {
    }
}
