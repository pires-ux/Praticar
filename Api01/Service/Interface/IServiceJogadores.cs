using Api01.DTOs;
using Api01.Models;

namespace Api01.Service.Interface
{
    public interface IServiceJogadores
    {
        public Task<Jogadores> Criar(CriarDTO criar);
    }
}
