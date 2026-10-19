using Api01.DTOs;
using Api01.Models;

namespace Api01.Repository.Interface
{
    public interface IRepositoryJogadores
    {
        public Task<Jogadores> Criar(CriarDTO criar);

        public Task<Jogadores> Login(LoginDTO login);
    }
}
