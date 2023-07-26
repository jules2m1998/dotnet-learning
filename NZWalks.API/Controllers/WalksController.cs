using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs.WalkDTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalksController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IWalkRepository repo;

    public WalksController(IMapper mapper, IWalkRepository repo)
    {
        this.mapper = mapper;
        this.repo = repo;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddWalksRequestDto dto)
    {
        var walk = mapper.Map<Walk>(dto);
        var created = await repo.CreateAsync(walk);
        var reponse = mapper.Map<WalkDto>(created);

        return CreatedAtAction(nameof(GetById), new { reponse.Id }, reponse);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        Walk? walk = await repo.GetByIdAsync(id);
        if (walk == null) return NotFound();
        return Ok(mapper.Map<WalkDto>(walk));
    }

    [HttpGet]
    [ProducesResponseType(typeof(WalkDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? filterOn, 
        [FromQuery] string? filterQuery, 
        [FromQuery] string? sortBy, 
        [FromQuery] bool? isAscending,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 100000
        )
    {
        List<Walk> walks = await repo.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
        return Ok(mapper.Map<List<WalkDto>>(walks));
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto dto)
    {
        var walk = mapper.Map<Walk>(dto);
        Walk? updated = await repo.UpdateAsync(id, walk);
        if(updated == null) return NotFound();
        return Ok(mapper.Map<WalkDto>(updated));
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        Walk? walk = await repo.DeleteAsync(id);
        if (walk == null) return NotFound();
        return NoContent();
    }
}
