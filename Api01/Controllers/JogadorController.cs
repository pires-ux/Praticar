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
            if (await _serv.VerificarEmail(criar.Email))
                return BadRequest("Email já cadastrado");
            var result = await _serv.Criar(criar);
            if (result != null)
                return Ok(result);

            throw new Exception("Erro ao tentar criar conta");
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseDTO>> Login(LoginDTO login)
        {
            try
            {
                var result = await _serv.Login(login);
                if (result != null)
                    return Ok(result);
                return BadRequest("Email ou senha incorretos");
            }
            catch
            {
                throw new Exception("Erro ao fazer login");
            }
        }
    }
}
