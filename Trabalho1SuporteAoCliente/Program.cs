

using System;

namespace Trabalho1SuporteAoCliente;
class MinhaBase : DbContext
	{
		public MinhaBase(DbContextOptions options) : base(options)
		{
		}
		
		public DbSet<Usuario> Usuarios { get; set; } = null!;
	}
class Program
{
    static void Main(string[] args)
    {
        SupportControll support = new SupportControll();

        support.UserRegister("Joao", "joao@email.com", 987654321);
        support.UserRegister("Maria", "maria@email.com", 988654321);
        support.UserRegister("Jose", "jose@email.com", 987754321);
        support.UserRegister("Marcos", "marcos@email.com", 988772321);
        support.UserRegister("Lucas", "lucas@email.com", 99874321);

        support.AgentRegister("Enzo", "enzo@email.clm", 98776655);
        support.AgentRegister("Sergio", "sergio@email.clm", 87776655);
        support.AgentRegister("Mateus", "mateus@email.clm", 78666655);
        support.AgentRegister("Vinicius", "vinicius@email.clm", 68555555);
        support.AgentRegister("Gustavo", "gustavo@email.clm", 58776655);

        support.OpenTicket(1, 1, new DateTime(2022, 09, 26, 13, 0, 0));

        Console.WriteLine(support.GetUsersList());
        Console.WriteLine(support.GetAgentsList());
        Console.WriteLine(support.GetTicketList());
    }
}

