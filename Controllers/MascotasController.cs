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
}