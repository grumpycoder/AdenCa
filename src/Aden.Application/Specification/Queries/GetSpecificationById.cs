using Aden.Application.Common.Exceptions;
using Aden.Application.Interfaces;
using MediatR;

namespace Aden.Application.Specification.Queries;

public static class GetSpecificationById
{
    public class Query : IRequest<Domain.Entities.Specification>
    {
        public int Id { get; }

        public Query(int id)
        {
            Id = id;
        }
    }

    public class
        Handler : IRequestHandler<Query, Domain.Entities.Specification>
    {
        private readonly IUnitOfWork _uow;

        public Handler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Domain.Entities.Specification> Handle(Query request,
            CancellationToken cancellationToken)
        {
            var entity = await _uow.Specifications.GetSpecificationWithSubmissions(request.Id); 
            
            if (entity == null) throw new NotFoundException(nameof(Domain.Entities.Specification), request.Id);

            return entity;
        }
    }
}