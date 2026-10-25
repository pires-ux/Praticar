using Api01.DTOs;
using Api01.Models;

namespace Api01.Repository.Interface
{
    public interface IRepositoryJogadores
    {
        Task<Jogadores> Criar(CriarDTO criar);
        Task<Jogadores> Login(LoginDTO login);
        Task<bool> VerificarEmail(string email);
        Task<Jogadores> Buscar(int id);

    }
}
