using SampleProject.DataAccessLayer.Application;
using SampleProject.DataAccessLayer.UnitOfWorks;
using SampleProject.DomainObject.Application;

namespace SampleProject.DataAccessLayer.Repositories
{
    public class PersonRepository : BaseRepository<Person, ApplicationDbContext>, IPersonRepository
    {
        public PersonRepository(IApplicationUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
