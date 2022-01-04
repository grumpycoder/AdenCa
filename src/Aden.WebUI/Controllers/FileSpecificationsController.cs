using Aden.WebUI.Application.FileSpecification.Queries;
using Aden.WebUI.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aden.WebUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileSpecificationsController: ControllerBase
{
    private readonly ILogger<FileSpecificationsController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IMediator _mediator;

    public FileSpecificationsController(ILogger<FileSpecificationsController> logger, ApplicationDbContext context, IMediator mediator)
    {
        _logger = logger;
        _context = context;
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult> Get([FromRoute] int id, CancellationToken token = new())
    {
        var entity = await _mediator.Send(new GetFileSpecificationByIdQuery(id));
        return Ok(entity);
    }
    
    [HttpGet]
    [Route("")]
    public async Task<ActionResult> Get(CancellationToken token = new())
    {
        var list = await _mediator.Send(new GetAllFileSpecificationsQuery(), token);
        return Ok(list);
    }
    
}