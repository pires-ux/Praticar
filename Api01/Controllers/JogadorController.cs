using Api01.DTOs;
using Api01.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Api01.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class JogadorController : Controller
    {
        private readonly IServiceJogadores _serv;
        private readonly ILogger<JogadorController> _log;
        public JogadorController(IServiceJogadores service, ILogger<JogadorController> log)
        {
            _log = log;
            _serv = service;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> Criar(CriarDTO criar)
        {
            _log.LogInformation("Começar comando");
            Console.WriteLine("começar comando");
            
            var result = await _serv.Criar(criar);
            if (result != null)
                return Ok(result);

            return BadRequest("falha ao criar jogador");
        }
    }
}
