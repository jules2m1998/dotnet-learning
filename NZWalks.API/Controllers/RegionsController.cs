using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;
using System.Net;
using System.Text.Json;

namespace NZWalks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RegionsController : ControllerBase
{
    private readonly IRegionRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<RegionsController> logger;

    public RegionsController(IRegionRepository repo, IMapper mapper, ILogger<RegionsController> logger)
    {
        _repo = repo;
        _mapper = mapper;
        this.logger = logger;
    }

    [HttpGet]
    //[Authorize(Roles ="Reader")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        logger.LogInformation("Get all action method was invoked");

        var regions = (await _repo.GetAllAsync()).ToList();
        logger.LogInformation("Finished GetAllRegions request with data: {data}", JsonSerializer.Serialize(regions));
        logger.LogWarning("This is a log warning");
        logger.LogError("This is a log error");
        var regionsDto = _mapper.Map<List<RegionDto>>(regions);
        return Ok(regionsDto);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Reader")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var region = await _repo.GetByIdAsync(id);
        if (region == null) return NotFound();
        return Ok(_mapper.Map<RegionDto>(region));
    }

    [HttpPost]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Create([FromBody] AddRegionRequestDto regionDto)
    {
        var region = _mapper.Map<Region>(regionDto);
        await _repo.CreateAsync(region);
        return CreatedAtAction(nameof(GetById), new { region.Id }, _mapper.Map<RegionDto>(region));
    }

    [HttpPut]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto requestDto)
    {
        var region = _mapper.Map<Region>(requestDto);
        var updated = await _repo.UpdateAsync(id, region);
        if (updated == null) return NotFound();

        return Ok(_mapper.Map<RegionDto>(updated));
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var region = await _repo.DeletAsync(id);
        if(region == null) return NotFound();

        return NoContent();
    }
}
