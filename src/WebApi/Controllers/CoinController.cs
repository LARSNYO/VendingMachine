using System.Net.Http.Headers;
using Application.DTOs.Coin;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoinController : ControllerBase
{
    private readonly ICoinService _coinService;
    public CoinController(ICoinService coinService)
    {
        _coinService = coinService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CoinGetResponseDto>>> GetAll()
    {
        var coins = await _coinService.GetCoinsAsync();
        return Ok(coins);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CoinGetResponseDto>> GetById(Guid id)
    {
        var coin = await _coinService.GetCoinByIdAsyncAsNoTracking(id);
        if (coin is null) return NotFound();
        return Ok(coin);
    }

    [HttpGet("{denomination:int}")]
    public async Task<ActionResult<CoinGetResponseDto>> GetByDenomination(int denomination)
    {
        var coin = await _coinService.GetByDenominationAsync(denomination);
        if (coin is null) return NotFound();

        return Ok(coin);
    }

    [HttpPost]
    public async Task<ActionResult<CoinGetResponseDto>> Create([FromBody] CoinPostResponseDto dto)
    {
        var created = await _coinService.CreateCoinAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CoinGetResponseDto>> Update(Guid id, CoinPostResponseDto dto)
    {
        if (!await _coinService.CoinExistAsync(id)) return NotFound();

        await _coinService.UpdateCoinAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        if (!await _coinService.CoinExistAsync(id)) return NotFound();

        await _coinService.DeleteCoinAsync(id);
        return NoContent();
    }
}