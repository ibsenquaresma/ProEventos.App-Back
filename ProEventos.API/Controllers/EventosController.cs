using Microsoft.AspNetCore.Mvc;
using ProEventos.Api.Helpers;
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
    private readonly IUtil _util;

    private readonly string _destino = "Images";


    public EventosController(ProEventosContext context, 
                             IEventoService eventoService, 
                             IUtil util
                             )
    {
        _context = context;
        _eventoService = eventoService;
        _util = util;
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

    [HttpPost("upload-image/{eventoId}")]
    public async Task<IActionResult> UploadImage(int eventoId)
    {
        try
        {
            var evento = await _eventoService.GetEventoByIdAsync(eventoId, true);
            if (evento == null) return NoContent();

            var file = Request.Form.Files[0];
            if (file.Length > 0)
            {
                _util.DeleteImage(evento.ImagemURL, _destino);
                evento.ImagemURL = await _util.SaveImage(file, _destino);
            }
            var EventoRetorno = await _eventoService.UpdateEvento(eventoId, evento);

            return Ok(EventoRetorno);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar adicionar eventos. Erro: {ex.Message}");
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
                _util.DeleteImage(evento.ImagemURL, _destino);
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
