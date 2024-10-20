using Microsoft.AspNetCore.Mvc;
using Models;
using Repository.Aplication;

namespace Controllers;

[ApiController]
[Route("api/mascotas")]
public class MascotasController : ControllerBase
{
    private readonly IMascotaRepository _mascotaRepository;
    public MascotasController(IMascotaRepository mascotaRepository)
    {
        _mascotaRepository = mascotaRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Mascota>>> Get()
    {
        return Ok(await _mascotaRepository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Mascota>> Get(int id)
    {
        var mascota = await _mascotaRepository.GetByIdAsync(id);
        if (mascota == null)
        {
            return NotFound();
        }
        return Ok(mascota);
    }

    [HttpPost]
    public async Task<ActionResult<Mascota>> Post(Mascota mascota)
    {
        await _mascotaRepository.AddAsync(mascota);
        return CreatedAtAction(nameof(Get), new { id = mascota.Mas_Id }, mascota);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Mascota mascota)
    {
        var mascotaOriginal = await _mascotaRepository.GetByIdAsync(id);
        if (mascotaOriginal == null)
        {
            return NotFound();
        }
        await _mascotaRepository.UpdateAsync(mascota);
        return Ok(mascota);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mascotaRepository.DeleteAsync(id);
        return Ok();
    }
}