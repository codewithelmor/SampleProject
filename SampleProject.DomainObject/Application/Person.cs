using SampleProject.DomainObject.Application.Enums;

namespace SampleProject.DomainObject.Application
{
    public class Person : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
    }
}
