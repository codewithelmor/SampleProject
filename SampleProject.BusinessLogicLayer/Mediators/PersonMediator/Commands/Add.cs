using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SampleProject.DataAccessLayer.Repositories;
using SampleProject.DataAccessLayer.UnitOfWorks;
using SampleProject.DataTransferObject.BindingModels;
using SampleProject.DataTransferObject.ViewModels;
using SampleProject.DomainObject.Application;
using SampleProject.DomainObject.Application.Enums;

namespace SampleProject.BusinessLogicLayer.Mediators.PersonMediator.Commands
{
    public class Add
    {
        public class Command : PersonBindingModel, IRequest<PersonViewModel> { }

        public class Handler : IRequestHandler<Command, PersonViewModel>
        {
            private readonly IMapper _mapper;
            private readonly ILogger<Add> _logger;
            private readonly IApplicationUnitOfWork _unitOfWork;
            private readonly IPersonRepository _persons;

            public Handler(
                IMapper mapper,
                ILogger<Add> logger,
                IApplicationUnitOfWork unitOfWork,
                IPersonRepository persons)
            {
                _mapper = mapper;
                _logger = logger;
                _unitOfWork = unitOfWork;
                _persons = persons;
            }

            public async Task<PersonViewModel> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var person = await _persons.AddAsync(new Person
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Gender = (Gender)int.Parse(request.Gender),
                        CreatedBy = request.GetUserName()
                    });

                    await _unitOfWork.Commit();
                    
                    return _mapper.Map<PersonViewModel>(person);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}