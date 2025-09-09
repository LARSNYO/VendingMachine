using System.Net.Http.Headers;
using Application.DTOs.Brand;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class BrandController : ControllerBase
{
    private readonly IBrandService _brandService;
    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BrandGetResponseDto>>> GetAll()
    {
        var brands = await _brandService.GetBrandsAsync();
        return Ok(brands);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BrandGetResponseDto>> GetById(Guid id)
    {
        var brand = await _brandService.GetBrandByIdAsyncAsNoTracking(id);
        if (brand is null) return NotFound();
        return Ok(brand);
    }

    [HttpPost]
    public async Task<ActionResult<BrandGetResponseDto>> Create([FromBody] BrandPostRequestDto dto)
    {
        var created = await _brandService.CreateBrandAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BrandGetResponseDto>> Update(Guid id, [FromBody] BrandPostRequestDto dto)
    {
        if (!await _brandService.BrandExistAsync(id)) return NotFound();

        await _brandService.UpdateBrandAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        if (!await _brandService.BrandExistAsync(id)) return NotFound();

        await _brandService.DeleteBrandAsync(id);
        return NoContent();
    }
}