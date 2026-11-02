using Api01.DTOs;
using Api01.Models;
using Api01.Repository.Interface;
using Api01.Service.Interface;

namespace Api01.Service
{
    public class ServiceJogadores: IServiceJogadores
    {
        private readonly JWTservice _jwt;
        private readonly IRepositoryJogadores _repo;
        private readonly ILogger<ServiceJogadores> _log;
        public ServiceJogadores(ILogger<ServiceJogadores> log, IRepositoryJogadores repo, JWTservice jwt)
        {
            _log = log;
            _repo = repo;
            _jwt = jwt;
        }

        public async Task<ResponseDTO> Criar(CriarDTO criar)
        {
            var senha = BCrypt.Net.BCrypt.HashPassword(criar.Senha);
            criar.Senha = senha;
            var result = await _repo.Criar(criar);
            return new ResponseDTO
            {
                Jogadores = result,
                Token = _jwt.GenerateToken(result.Id)
            };
        }

        public async Task<ResponseDTO> Login(LoginDTO login)
        {
            var jogador = await _repo.Login(login);
            if(jogador == null)
            {
                _log.LogInformation("Login inválido");
                return null;
            }
            bool igual = BCrypt.Net.BCrypt.Verify(login.Senha, jogador.Senha);
            if(!igual)
            {
                _log.LogInformation("Senha inválida");
                return null;
            }

            var token = _jwt.GenerateToken(jogador.Id);
            _log.LogInformation("Login realizado com sucesso");
            return new ResponseDTO
            {
                Jogadores = jogador,
                Token = token
            };

        }
        public async Task<bool> VerificarEmail(string email)
        { 
            return await _repo.VerificarEmail(email);
        }

        public async Task<Jogadores> Buscar(int id)
        {
            var jogador = await _repo.Buscar(id);
            if (jogador == null)
            {
                _log.LogInformation("Jogador não encontrado");
                return null;
            }
            return jogador;
        }
    }
}
