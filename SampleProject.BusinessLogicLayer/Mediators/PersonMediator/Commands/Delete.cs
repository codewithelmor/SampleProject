using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SampleProject.Common.Exceptions;
using SampleProject.DataAccessLayer.Repositories;
using SampleProject.DataAccessLayer.UnitOfWorks;
using SampleProject.DataTransferObject.BindingModels;
using SampleProject.DataTransferObject.ViewModels;

namespace SampleProject.BusinessLogicLayer.Mediators.PersonMediator.Commands
{
    public class Delete
    {
        public class Command : BaseBindingModel, IRequest<PersonViewModel>
        {
            public Guid PersonId { get; }

            public Command(string personId)
            {
                PersonId = new Guid(personId);
            }
        }

        public class Handler : IRequestHandler<Command, PersonViewModel>
        {
            private readonly IMapper _mapper;
            private readonly ILogger<Delete> _logger;
            private readonly IApplicationUnitOfWork _unitOfWork;
            private readonly IPersonRepository _persons;

            public Handler(
                IMapper mapper,
                ILogger<Delete> logger,
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
                    var person = _persons.Get(request.PersonId);

                    if (person != null)
                    {
                        person.IsDeleted = true;
                        person.UpdatedAt = DateTimeOffset.Now;
                        person.UpdatedBy = request.GetUserName();

                        _persons.Update(person);
                        await _unitOfWork.Commit();

                        return _mapper.Map<PersonViewModel>(person);
                    }

                    throw new CustomException(
                        statusCode: HttpStatusCode.NotFound, 
                        message: $"Person with Id {request.PersonId} was not found from the database.");
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
