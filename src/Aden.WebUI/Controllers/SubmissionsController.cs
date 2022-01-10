using Aden.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aden.WebUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubmissionsController: ControllerBase
{
    private readonly ILogger<SubmissionsController> _logger;
    private readonly ApplicationDbContext _context;

    public SubmissionsController(ILogger<SubmissionsController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult> Get([FromRoute] int id, CancellationToken token = new())
    {
        var entity = await _context.Submissions
            .Include(x => x.Specification)
            .FirstAsync(x => x.Id == id, cancellationToken: token);
        return Ok(entity);
    }
    
    [HttpGet]
    [Route("")]
    public async Task<ActionResult> Get(CancellationToken token = new())
    {
        var list = await _context.Submissions.ToListAsync(cancellationToken: token);
        return Ok(list);
    }

    [HttpPut]
    public async Task<ActionResult> Put()
    {
        return Ok();
    }
}