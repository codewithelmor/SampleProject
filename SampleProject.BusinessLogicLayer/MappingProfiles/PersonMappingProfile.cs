using AutoMapper;
using SampleProject.DataTransferObject.ViewModels;
using SampleProject.DomainObject.Application;

namespace SampleProject.BusinessLogicLayer.MappingProfiles
{
    public class PersonMappingProfile : Profile
    {
        public PersonMappingProfile()
        {
            CreateMap<Person, PersonViewModel>()
                .ForMember(dest => dest.Gender,
                    opts => opts.MapFrom(src => new SelectViewModel()
                    {
                        Label = ((int)src.Gender).ToString(),
                        Value = src.Gender.ToString()
                    }));
        }
    }
}