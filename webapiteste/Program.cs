//VERSAO WEBAPI

namespace Trabalho
{
    class Pessoa
    {
        string ?nome;
        DateTime ?nascimento;
        }

	class Program
	{
		//exemplo de funcoes estatica que pode ser mapeadas em algum endpoint da api
		static string welcome()
		{
			return "hello world";
		}
	
		
		static void Main(string[] args)
		{
			//cria o criador (builder) de aplicacoes
			var builder = WebApplication.CreateBuilder(args);
			
			//cria a aplicacao usando o builder
			var app = builder.Build();
			
			//mapeia a raiz da nossa url para a funcao "welcome" via metodo GET (observe que a propria funcao esta sendo enviada como argumento, e nao o retorno dela)
			app.MapGet("/", welcome);
			
			//inicia a aplicacao
			app.Run();
		}
		
		//esse record possui o nome das chaves do JSON que o POST deve enviar e que a funcao chamada pelo POST quer receber
		record ChavesUsuario(string nome, string email, string password);
	}
}
