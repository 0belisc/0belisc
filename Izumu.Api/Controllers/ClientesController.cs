using Microsoft.AspNetCore.Mvc;
using Izumu.Shared.Models;
using Izumu.Api.Services;

namespace Izumu.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase {
    private readonly IClienteService _service;
    public ClientesController(IClienteService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _service.GetAll());

    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> GetById(int id)
    {
        // 1. Llamamos al servicio para buscar el cliente
        var cliente = await _service.GetById(id);

        // 2. Si no existe, retornamos 404 NotFound (Importante para el manejo de errores en el Front)
        if (cliente == null)
        {
            return NotFound(new { Message = $"El cliente con ID {id} no fue encontrado." });
        }

        // 3. Si existe, retornamos el objeto con un 200 OK
        return Ok(cliente);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Cliente cliente) {
        var success = await _service.Create(cliente);
        if (!success) return BadRequest(new { message = "El cliente ya existe o datos inválidos" });
        return Ok(new { message = "Cliente adicionado con éxito" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Cliente cliente) {
        if (id != cliente.Id) return BadRequest();
        var success = await _service.Update(cliente);
        return success ? Ok(new { message = "Información modificada con éxito" }) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
        var success = await _service.Delete(id);
        return success ? Ok(new { message = "Cliente eliminado correctamente" }) : NotFound();
    }
}