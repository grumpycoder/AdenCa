using Aden.Application.Specification.Commands;
using Aden.Application.Specification.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aden.WebUI.Controllers;

[ApiController]
[Route("api/[controller]", Name = "Specifications")]
public class SpecificationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SpecificationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{id:int}", Name = nameof(GetFileSpecificationById))]
    public async Task<ActionResult> GetFileSpecificationById([FromRoute] int id, CancellationToken token = new())
    { 
        var entity = await _mediator.Send(new GetSpecificationById.Query(id), token);
        return Ok(entity);
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult> Get(CancellationToken token = new())
    {
        
        var list = await _mediator.Send(new GetAllSpecifications.Query(), token);
        return Ok(list);
    }
    
    [HttpPost]
    public async Task<ActionResult> Post([FromBody]CreateSpecification.Command command)
    {
        
        var entity = await _mediator.Send(command);
        return new CreatedAtRouteResult(nameof(GetFileSpecificationById), new { id = entity.Id }, entity);
    }
    
    [HttpPut]
    public async Task<ActionResult> Put(UpdateSpecification.Command command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPost]
    [Route("retire/{id:int}")]
    public async Task<ActionResult> Retire([FromRoute] int id)
    {
        var result = await _mediator.Send(new RetireSpecification.Command(id));
        return Ok(result);
    }

    [HttpPost]
    [Route("activate/{id:int}")]
    public async Task<ActionResult> Activate([FromRoute] int id)
    {
        var result = await _mediator.Send(new ActivateSpecification.Command(id));
        return Ok(result);
    }
 
}