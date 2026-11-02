using Api01.DTOs;
using Api01.Models;
using Api01.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<ActionResult<Jogadores>> Criar(CriarDTO criar)
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

        [Authorize (Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Jogadores>> Buscar(int id)
        {
            try
            {
                var idUsuario = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (!int.TryParse(idUsuario, out var idAutenticado))
                    return Unauthorized();

                if (idAutenticado != id)
                    return Forbid();

                var result = await _serv.Buscar(id);
                if (result != null)
                    return Ok(result);
                return NotFound("Jogador não encontrado");
            }
            catch
            {
                throw new Exception("Erro ao buscar jogador");
            }
        }
    }
}
