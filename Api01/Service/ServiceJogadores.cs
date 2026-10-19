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
    }
}
