using Api01.DTOs;
using Api01.Models;
using Api01.Repository.Interface;
using Api01.Service.Interface;

namespace Api01.Service
{
    public class ServiceJogadores: IServiceJogadores
    {
        private readonly IRepositoryJogadores _repo;
        private readonly ILogger<ServiceJogadores> _log;
        public ServiceJogadores(ILogger<ServiceJogadores> log, IRepositoryJogadores repo)
        {
            _log = log;
            _repo = repo;
        }

        public async Task<Jogadores> Criar(CriarDTO criar)
        {
            var senha = BCrypt.Net.BCrypt.HashPassword(criar.Senha);
            criar.Senha = senha;
            var result = await _repo.Criar(criar);
            return result;
        }

        public async Task<Jogadores> Login(LoginDTO login)
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
            _log.LogInformation("Login realizado com sucesso");
            return jogador;

        }
        public async Task<bool> VerificarEmail(string email)
        { 
            return await _repo.VerificarEmail(email);
        }
    }
}
