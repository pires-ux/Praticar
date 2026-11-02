using Api01.DTOs;
using Api01.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Teste_Api01
{
    public class Teste_Integrado : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public Teste_Integrado(CustomWebApplicationFactory factory)
        {
            factory.LimparBanco();
            _client = factory.CreateClient();
        }

        public Jogadores Jogador()
        {
            return new Jogadores
            {
                Nome = "Teste",
                Usuario = "teste",
                Email = "teste@gmail.com",
                Senha = "123456"
            };
        }


        // Teste de criação de jogador --------------------------------------------------------------------
        [Fact]
        public async Task CriarJogador()
        {
            var criar = Jogador();

            var response = await _client.PostAsJsonAsync("/api/Jogador", criar);
            var resultado = await response.Content.ReadFromJsonAsync<ResponseDTO>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(resultado.Jogadores);
        }

        [Fact]
        public async Task CriarJogadorComEmailExistente()
        {
            var criar = Jogador();
            var response = await _client.PostAsJsonAsync("/api/Jogador", criar);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            // Tenta criar novamente com o mesmo email
            response = await _client.PostAsJsonAsync("/api/Jogador", criar);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Email já cadastrado", await response.Content.ReadAsStringAsync());

        }

        


        // Teste de login --------------------------------------------------------------------
        [Fact]
        public async Task Login()
        {

            var criar = Jogador();
            var response = await _client.PostAsJsonAsync("/api/Jogador", criar);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var login = new LoginDTO
            {
                Email = "teste@gmail.com",
                Senha = "123456"
            };

            response = await _client.PostAsJsonAsync("/api/Jogador/login", login);

            Console.WriteLine(await response.Content.ReadAsStringAsync() + "--------------------------");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
        }



        [Fact]
        public async Task Get()
        {
            var criar = Jogador();
            var response = await _client.PostAsJsonAsync("/api/Jogador", criar);
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jogadorCriado = await response.Content.ReadFromJsonAsync<ResponseDTO>();
            var id = jogadorCriado.Jogadores.Id;

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", jogadorCriado.Token);

            response = await _client.GetAsync($"/api/Jogador/{id}");
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(await response.Content.ReadFromJsonAsync<Jogadores>());
            
        }
    }
}
