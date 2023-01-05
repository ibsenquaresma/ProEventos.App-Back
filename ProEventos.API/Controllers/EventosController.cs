using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Data;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventosController : ControllerBase
{
    private readonly DataContext _context;

    public EventosController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<Evento> GetAll()
    {
        return _context.Eventos;
    }

    [HttpGet("{id}")]
    public IEnumerable<Evento> GetById(int id)
    {
        return _context.Eventos.Where(x=> x.EventoId == id);
    }

    [HttpPost]
    public string Post()
    {
        return "Post";
    }

    [HttpPut("{id}")]
    public string Put(int id)
    {
        return "Put " + id;
    }

    [HttpDelete("{id}")]
    public string Delete(int id)
    {
        return "Delete " + id;
    }
}
