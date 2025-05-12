using System.Runtime.CompilerServices;
using apbd_test1.Model;
using apbd_test1.Service;
using Microsoft.AspNetCore.Mvc;

namespace apbd_test1.Controller;

[ApiController]
[Route("api/[controller]")]
public class VisitsController : ControllerBase
{
    private readonly IVisitService _visitService;

    public VisitsController(IVisitService visitService)
    {
        _visitService = visitService;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVisit(int id)
    {
        var visit=await _visitService.GetVisitAsync(id);
        if (!visit.Success)
        {
            return NotFound(visit.Message);
        }
        
        return Ok(visit.Result);
    }

    [HttpPost]
    public async Task<IActionResult> AddVisit(NewVisitDTO newVisit)
    {
        var newId=await _visitService.AddVisitAsync(newVisit);
        
        if(!newId.Success)
            return BadRequest(newId.Message);
        
        return CreatedAtAction(nameof(GetVisit), new { id = newId.Result }, newVisit);
    }
}