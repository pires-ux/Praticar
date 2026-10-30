using Api01.DTOs;
using Moq;
using Api01.Repository.Interface;
using Api01.Service.Interface;
using Xunit;
using Api01.Models;

namespace Teste_Api01
{
    public class UnitTest1
    {

        public async Task<Jogadores> Sql()
        {
            var jogador = new Jogadores
            {
                Id = 1,
                Nome = "Adriano",
                Usuario = "Pires",
                Email = "adriano@gmail.com",
                Senha = "123456"
            };

            return jogador;
        }

        [Fact]
        public async Task Test1()
        {
            var jogadores = await Sql();
            var criar = new CriarDTO
            {
                Nome = "Adriano",
                Usuario = "Pires",
                Email = "adriano@gmail.com",
                Senha = "123456"
            };

            var mock = new Mock<IServiceJogadores>();

            mock.Setup(m => m.Criar(criar)).ReturnsAsync(jogadores);

            var resultado = mock.Object.Criar(criar);



            Assert.NotNull(resultado);

            mock.Verify(m => m.Criar(criar), Times.Once);
        }

        [Fact]
        public async Task Login_Teste()
        {
            var sql = new ResponseDTO
            {
                Jogadores = await Sql(),
                Token = "eyJhbG",
            };

            var jogador = new LoginDTO
            {
                Email = "adriano@gmail.com",
                Senha = "123456",
            };

            var mock = new Mock<IServiceJogadores>();
            mock.Setup(m => m.Login(jogador)).ReturnsAsync(sql);

            var resultado = mock.Object.Login(jogador);

            Assert.NotNull(resultado);
            Assert.Equal(jogador.Email, sql.Jogadores.Email);
            Assert.True(resultado.IsCompletedSuccessfully);

            mock.Verify(m => m.Login(jogador), Times.Once);
            
        }

        [Fact]
        public async Task Busca()
        {
            var sql = new ResponseDTO
            {
                Jogadores = await Sql(),
                Token = "eyJhbG",
            };

            var jogadores = Sql();

            var mock = new Mock<IServiceJogadores>();
            mock.Setup(m => m.Buscar(1)).Returns(jogadores);
            var resultado = mock.Object.Buscar(1);

            Assert.True(resultado.IsCompletedSuccessfully);
            Assert.Equal(sql.Jogadores.Id, resultado.Result.Id);

            mock.Verify(m => m.Buscar(1), Times.Once);
        }
            
    }
}
