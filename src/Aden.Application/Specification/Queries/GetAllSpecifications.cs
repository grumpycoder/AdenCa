using Aden.Application.Interfaces;
using MediatR;

namespace Aden.Application.Specification.Queries;

public static class GetAllSpecifications
{
    public class Query : IRequest<IEnumerable<Domain.Entities.Specification>>
    {
    }

    public class Handler : IRequestHandler<Query,
            IEnumerable<Domain.Entities.Specification>>
    {
        private readonly IUnitOfWork _uow;

        public Handler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<Domain.Entities.Specification>> Handle(Query request,
            CancellationToken cancellationToken)
        {
            var list = await _uow.Specifications.All();
            // var list = await _context.Specifications
            //     .Include(x => x.Submissions)
            //     .AsNoTracking().ToListAsync(cancellationToken);

            return list;
        }
    }
}