using Aden.Application.FileSpecification.Commands.CreateFileSpecification;
using Aden.Application.FileSpecification.Commands.UpdateFileSpecification;
using Aden.Application.FileSpecification.Queries;
using Aden.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aden.WebUI.Controllers;

[ApiController]
[Route("api/[controller]", Name = "FileSpecifications")]
public class FileSpecificationsController : ControllerBase
{
    private readonly ILogger<FileSpecificationsController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IMediator _mediator;

    public FileSpecificationsController(ILogger<FileSpecificationsController> logger, ApplicationDbContext context,
        IMediator mediator)
    {
        _logger = logger;
        _context = context;
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{id:int}", Name = nameof(GetFileSpecificationById))]
    public async Task<ActionResult> GetFileSpecificationById([FromRoute] int id, CancellationToken token = new())
    {
        var entity = await _mediator.Send(new GetSpecificationByIdQuery(id), token);
        return Ok(entity);
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult> Get(CancellationToken token = new())
    {
        var list = await _mediator.Send(new GetAllSpecificationsQuery(), token);
        return Ok(list);
    }
    
    [HttpPost]
    public async Task<ActionResult> Post([FromBody]CreateSpecificationCommand command)
    {
        var entity = await _mediator.Send(command);
        return new CreatedAtRouteResult(nameof(GetFileSpecificationById), new { id = entity.Id }, entity);
    }
    
    [HttpPut]
    public async Task<ActionResult> Put(UpdateSpecificationCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
}