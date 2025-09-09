using Application.DTOs.Drink;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DrinkController : ControllerBase
{
    private readonly IDrinkService _drinkService;
    public DrinkController(IDrinkService drinkService)
    {
        _drinkService = drinkService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DrinkGetResponseDto>>> GetAll()
    {
        var drinks = await _drinkService.GetDrinksAsync();
        return Ok(drinks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DrinkGetResponseDto>> GetById(Guid id)
    {
        var drink = await _drinkService.GetDrinkByIdAsync(id);
        if (drink is null) return NotFound();
        return Ok(drink);
    }

    [HttpPost]
    public async Task<ActionResult<DrinkGetResponseDto>> Create([FromBody] DrinkPostRequestDto dto)
    {
        var created = await _drinkService.CreateDrinkAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

}