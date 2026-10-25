using Api01.DTOs;
using Api01.Models;

namespace Api01.Service.Interface
{
    public interface IServiceJogadores
    {
        Task<Jogadores> Criar(CriarDTO criar);
        Task<ResponseDTO> Login(LoginDTO login);
        Task<bool> VerificarEmail(string email);
        Task<Jogadores> Buscar(int id);
    }
}
