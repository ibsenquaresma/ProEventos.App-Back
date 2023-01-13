using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Persistence.Contextos;

namespace ProEventos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventosController : ControllerBase
{
    private readonly ProEventosContext _context;
    private readonly IEventoService _eventoService;

    public EventosController(ProEventosContext context, IEventoService eventoService)
    {
        _context = context;
        _eventoService = eventoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var eventos = await _eventoService.GetAllEventosAsync(true);
            if (eventos == null) return NoContent();

            return Ok(eventos);
        }
        catch (Exception ex)
        {
            return this.StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var eventos = await _eventoService.GetEventoByIdAsync(id, true);
            if (eventos == null) return NoContent();

            return Ok(eventos);
        }
        catch (Exception ex)
        {
            return this.StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(EventoDto model)
    {
        try
        {
            var evento = await _eventoService.AddEventos(model);
            if (evento == null) return NoContent();

            return Ok(evento);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar adicionar eventos. Erro: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EventoDto model)
    {
        try
        {
            var evento = await _eventoService.UpdateEvento(id, model);
            if (evento == null) return NoContent();

            return Ok(evento);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar atualizar eventos. Erro: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var evento = await _eventoService.GetEventoByIdAsync(id, true);
            if (evento == null) return NoContent();

            if (await _eventoService.DeleteEvento(id))
            {
                return Ok(new { message = "Deletado" });
            }
            else
            {
                throw new Exception("Ocorreu um problem não específico ao tentar deletar Evento.");
            }
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar deletar eventos. Erro: {ex.Message}");
        }
    }
}
