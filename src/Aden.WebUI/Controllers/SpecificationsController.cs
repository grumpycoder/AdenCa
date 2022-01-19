using Aden.Application.FileSpecification.Commands.UpdateFileSpecification;
using Aden.Application.FileSpecification.Queries;
using Aden.Application.Specification.Commands;
using Aden.Application.Submission.Commands.CreateSubmission;
using Aden.Domain.Events;
using Aden.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aden.WebUI.Controllers;

[ApiController]
[Route("api/[controller]", Name = "Specifications")]
public class SpecificationsController : ControllerBase
{
    private readonly ILogger<SpecificationsController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IMediator _mediator;

    public SpecificationsController(ILogger<SpecificationsController> logger, ApplicationDbContext context,
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
        _mediator.Publish(new SampleEvent("Hello from mediator event")); 
        
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
    public async Task<ActionResult> Post([FromBody]CreateSpecification.Command command)
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
    
    [HttpPost]
    [Route("retire/{id:int}")]
    public async Task<ActionResult> Retire([FromRoute] int id)
    {
        var result = await _mediator.Send(new RetireSpecificationCommand(id));
        return Ok(result);
    }

    [HttpPost]
    [Route("activate/{id:int}")]
    public async Task<ActionResult> Activate([FromRoute] int id)
    {
        var result = await _mediator.Send(new ActivateSpecificationCommand(id));
        return Ok(result);
    }
    
    [HttpPost]
    [Route("{id:int}/submission")]
    public async Task<ActionResult> AddSubmission([FromRoute]int id, CreateSubmissionCommand command, CancellationToken token = new())
    {
        var entity = await _mediator.Send(command, token);
        return Ok(entity);
    }
}