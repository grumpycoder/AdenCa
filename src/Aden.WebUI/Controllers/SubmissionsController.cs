using Aden.Application.FileSpecification.Queries;
using Aden.Application.Submission.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aden.WebUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubmissionsController: ControllerBase
{
    private readonly ILogger<SubmissionsController> _logger;
    private readonly IMediator _mediator;

    public SubmissionsController(ILogger<SubmissionsController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult> Get([FromRoute] int id, CancellationToken token = new())
    {
        var entity = await _mediator.Send(new GetSubmissionById.Query(id));
        return Ok(entity);
    }
    
    [HttpGet]
    [Route("")]
    public async Task<ActionResult> Get(CancellationToken token = new())
    {
        var list = await _mediator.Send(new GetAllSubmissions.Query(), token);
        return Ok(list);
    }

    [HttpPut]
    public async Task<ActionResult> Put()
    {
        return Ok();
    }

    [HttpPost]
    [Route("{id:int}/cancel")]
    public async Task<ActionResult> CancelSubmission([FromRoute] int id, CancellationToken token = new())
    {
        
        return Ok();
    }
}