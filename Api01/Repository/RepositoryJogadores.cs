using Api01.Data;
using Api01.DTOs;
using Api01.Models;
using Api01.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Api01.Repository
{
    public class RepositoryJogadores : IRepositoryJogadores
    {
        private readonly Banco _db;
        private readonly ILogger<RepositoryJogadores> _log;
        public RepositoryJogadores(Banco banco, ILogger<RepositoryJogadores> log)
        {
            _log = log;
            _db = banco;
        }

        public async Task<Jogadores> Criar(CriarDTO criar)
        {
            var jogador = new Jogadores
            {
                Id = 0,
                Nome = criar.Nome,
                Usuario = criar.Usuario,
                Email = criar.Email,
                Senha = criar.Senha
            };
            _db.jogadores.Add(jogador);
            _db.SaveChanges();
            var result = _db.jogadores.FirstOrDefault(c => jogador.Email == c.Email);
            if (result != null) _log.LogDebug("erro ao criar jogador");
            return result;


        }

        public Task<Jogadores> Login(LoginDTO login)
        {
            return null;
        }
    }
}