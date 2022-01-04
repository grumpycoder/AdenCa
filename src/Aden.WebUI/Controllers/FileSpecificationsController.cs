using Aden.WebUI.Application.FileSpecification.Commands;
using Aden.WebUI.Application.FileSpecification.Queries;
using Aden.WebUI.Domain.Entities;
using Aden.WebUI.Persistence;
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
    [Route("{id:int}", Name = "GetFileSpecificationById")]
    [Route("{id:int}")]
    public async Task<ActionResult> GetFileSpecificationById([FromRoute] int id, CancellationToken token = new())
    {
        var entity = await _mediator.Send(new GetFileSpecificationByIdQuery(id), token);
        return Ok(entity);
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult> Get(CancellationToken token = new())
    {
        var list = await _mediator.Send(new GetAllFileSpecificationsQuery(), token);
        return Ok(list);
    }

    [HttpPost]
    public async Task<ActionResult> Post(CreateFileSpecificationCommand command)
    {
        var entity = await _mediator.Send(command);
        // return new CreatedAtActionResult(nameof(GetFileSpecificationById), 
        //     nameof(FileSpecificationsController), 
        //     new { id = entity.Id }, 
        //     entity);
        
        return new CreatedAtRouteResult(nameof(GetFileSpecificationById), new { id = entity.Id }, entity);
       
        //return new CreatedAtRouteResult("GetFileSpecification", new { id = entity.Id }, entity);
        //return new CreatedResult(nameof(FileSpecification), entity);
        //return new CreatedAtRouteResult(nameof(FileSpecification), new { id = entity.Id}, entity); 
    }

    [HttpPut]
    public async Task<ActionResult> Put(UpdateFileSpecificationCommand command)
    {
        var entity = _mediator.Send(command);
        return new CreatedResult(nameof(FileSpecification), entity);
    }
}