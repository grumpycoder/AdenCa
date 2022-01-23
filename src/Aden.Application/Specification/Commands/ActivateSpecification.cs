using Aden.Application.Common.Exceptions;
using Aden.Application.Interfaces;
using MediatR;

namespace Aden.Application.Specification.Commands;

public static class ActivateSpecification
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
            var entity = await _uow.Specifications.GetById(request.Id);

            if (entity == null) throw new NotFoundException(nameof(Domain.Entities.Specification), request.Id);

            entity.Activate();

            _uow.Complete(); 

            return Unit.Value;
        }
    }
}