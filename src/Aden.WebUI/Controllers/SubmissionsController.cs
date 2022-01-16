using Aden.Application.FileSpecification.Queries;
using Aden.Application.Submission.Queries;
using Aden.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aden.WebUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubmissionsController: ControllerBase
{
    private readonly ILogger<SubmissionsController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IMediator _mediator;

    public SubmissionsController(ILogger<SubmissionsController> logger, ApplicationDbContext context, IMediator mediator)
    {
        _logger = logger;
        _context = context;
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult> Get([FromRoute] int id, CancellationToken token = new())
    {
        var entity = await _mediator.Send(new GetSubmissionByIdQuery(id), token);
        return Ok(entity);
    }
    
    [HttpGet]
    [Route("")]
    public async Task<ActionResult> Get(CancellationToken token = new())
    {
        var list = await _mediator.Send(new GetAllSubmissionsQuery(), token);
        return Ok(list);
    }

    [HttpPut]
    public async Task<ActionResult> Put()
    {
        return Ok();
    }

    [HttpPost]
    [Route("{specificationId:int}")]
    public async Task<ActionResult> AddSubmission(int specificationId)
    {
        return Ok(specificationId);
    }
}