using Aden.Application.Common.Exceptions;
using Aden.Application.Interfaces;
using MediatR;

namespace Aden.Application.Specification.Commands;

public static class RetireSpecification
{

    public record Command(int Id) : IRequest{}

    public class Handler : IRequestHandler<Command>
    {
        private readonly IUnitOfWork _uow;

        public Handler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await _uow.Specifications.GetSpecificationWithSubmissions(request.Id);
            // _context.Specifications
            //     .Include(s => s.Submissions)
            //     .FirstAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

            if (entity == null) throw new NotFoundException(nameof(Domain.Entities.Specification), request.Id);

            if (entity.IsRetired) throw new BadRequestException("Specification is already retired");

            entity.Retire();

            _uow.Complete(); 
            return Unit.Value;
        }
    }
}