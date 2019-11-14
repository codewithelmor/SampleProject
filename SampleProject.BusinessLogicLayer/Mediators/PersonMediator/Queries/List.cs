using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SampleProject.DataAccessLayer.Repositories;
using SampleProject.DataAccessLayer.UnitOfWorks;
using SampleProject.DataTransferObject.BindingModels;
using SampleProject.DataTransferObject.ViewModels;

namespace SampleProject.BusinessLogicLayer.Mediators.PersonMediator.Queries
{
    public class List
    {
        public class Query : BaseBindingModel, IRequest<List<PersonViewModel>> { }

        public class Handler : IRequestHandler<Query, List<PersonViewModel>>
        {
            private readonly IMapper _mapper;
            private readonly ILogger<List> _logger;
            private readonly IApplicationUnitOfWork _unitOfWork;
            private readonly IPersonRepository _persons;

            public Handler(
                IMapper mapper,
                ILogger<List> logger,
                IApplicationUnitOfWork unitOfWork,
                IPersonRepository persons)
            {
                _mapper = mapper;
                _logger = logger;
                _unitOfWork = unitOfWork;
                _persons = persons;
            }

            public async Task<List<PersonViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var persons = await _persons
                        .Find(x => !x.IsDeleted && x.CreatedBy == request.GetUserName())
                        .ToListAsync(cancellationToken: cancellationToken);

                    return _mapper.Map<List<PersonViewModel>>(persons);
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