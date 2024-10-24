using Microsoft.AspNetCore.Mvc;
using Models;
using Repository.Aplication;

namespace Controllers;

[ApiController]
[Route("api/clientes")]
public class ClientesController : ControllerBase
{
    private readonly IClienteRepository _clienteRepository;
    public ClientesController(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> Get()
    {
        return Ok(await _clienteRepository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> Get(int id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);
        if (cliente == null)
        {
            return NotFound();
        }
        return Ok(cliente);
    }

    [HttpPost]
    public async Task<ActionResult<Cliente>> Post(ClienteDto clienteDto)
    {
        var cliente = new Cliente
        {
            Cli_Id = clienteDto.Cli_Id,
            Per_Id = clienteDto.Per_Id
        };
        await _clienteRepository.AddAsync(cliente);
        return CreatedAtAction(nameof(Get), new { id = cliente.Cli_Id }, cliente);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Cliente>> Delete(int id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);
        if (cliente == null)
        {
            return NotFound();
        }
        await _clienteRepository.DeleteAsync(id);
        return Ok();
    }
}