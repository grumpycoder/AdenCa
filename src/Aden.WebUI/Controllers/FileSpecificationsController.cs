using Aden.WebUI.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aden.WebUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileSpecificationsController: ControllerBase
{
    private readonly ILogger<FileSpecificationsController> _logger;
    private readonly ApplicationDbContext _context;

    public FileSpecificationsController(ILogger<FileSpecificationsController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult> Get([FromRoute] int id, CancellationToken token = new())
    {
        var entity = await _context.FileSpecifications.FindAsync(id);
        return Ok(entity);
    }
    
    [HttpGet]
    [Route("")]
    public async Task<ActionResult> Get(CancellationToken token = new())
    {
        var list = await _context.FileSpecifications.ToListAsync(cancellationToken: token);
        return Ok(list);
    }
    
}