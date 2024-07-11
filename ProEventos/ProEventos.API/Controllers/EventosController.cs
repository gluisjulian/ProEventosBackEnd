using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;


namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if(eventos == null) return NotFound("Nenhum evento encontrado.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro:{ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventoById(int id)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id, true);
                if (evento == null) return NotFound("Nenhum evento encontrado.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o evento. Erro:{ex.Message}");
            }
        }


        [HttpGet("/tema/{tema}")]
        public async Task<IActionResult> GetEventoByTema(string tema)
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosByTemaAsync(tema, true);
                if (eventos == null) return NotFound("Eventos por tema não encontrados.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o evento. Erro:{ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEvento(Evento model)
        {
            try
            {
                var evento = await _eventoService.AddEvento(model);
                if (evento == null) return BadRequest("Erro ao tentar adicionar o evento.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possivel adicionar o evento. Erro:{ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvento(int id, Evento model)
        {
            try
            {
                var evento = await _eventoService.UpdateEvento(id, model);
                if (evento == null) return BadRequest("Erro ao tentar atualizar o evento.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possivel atualizar o evento. Erro:{ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            try
            {
                //IF TERNARIO
                //return await _eventoService.DeleteEvento(id) ? Ok("Deletado") : BadRequest("Evendo não deletado");


                if (await _eventoService.DeleteEvento(id)) 
                    return Ok("Evento deletado.");

                return BadRequest("Evento nao deletado.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possivel deletar o evento. Erro:{ex.Message}");
            }
        }
    }
}
