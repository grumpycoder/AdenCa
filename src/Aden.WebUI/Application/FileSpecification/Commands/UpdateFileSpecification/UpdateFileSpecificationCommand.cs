﻿using Aden.WebUI.Application.Common.Exceptions;
using Aden.WebUI.Persistence;
using MediatR;

namespace Aden.WebUI.Application.FileSpecification.Commands.UpdateFileSpecification;

public class UpdateFileSpecificationCommand : IRequest
{
    public int Id { get; set; }
    public string Filename { get; set; }
    public string FileNumber { get; set; }
    public bool IsSea { get; set; }
    public bool IsLea { get; set; }
    public bool IsSch { get; set; }
}

public class UpdateFileSpecificationCommandHandler : IRequestHandler<UpdateFileSpecificationCommand>
{
    private readonly ApplicationDbContext _context;

    public UpdateFileSpecificationCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateFileSpecificationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.FileSpecifications.FindAsync(request.Id);
        if (entity == null)
        {
            throw new NotFoundException(nameof(FileSpecification), request.Id);
        }

        return Unit.Value;
    }
}