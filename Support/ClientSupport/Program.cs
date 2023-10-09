//VERSAO WEBAPI COM DATABASE

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace trabalho1
{
	
	class Database : DbContext
	{
		public Database(DbContextOptions options) : base(options)
		{
		}
		
		public DbSet<User> Users { get; set; } = null!;
        public DbSet<SupportAgent> Agents { get; set; } = null!;
        public DbSet<ServiceRequest> ServicesRequest { get; set; } = null!;
	}
	
	class Program
	{
		static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			
			var connectionString = builder.Configuration.GetConnectionString("Database") ?? "Data Source=Database.db";
			builder.Services.AddSqlite<Database>(connectionString);
			
			var app = builder.Build();
			
            //---------------------- CRUD DO FUNCIONÁRIO

			//listar todos os usuarios
			app.MapGet("/users", (Database Database) => {
				return Database.Users.ToList();
			});
			
			//listar usuario especifico (por id)
			app.MapGet("/user/{id}", (Database Database, int id) => {
				return Database.Users.Find(id);
			});
			
			//cadastrar usuario
			app.MapPost("/userRegister", (Database Database, User user) =>
			{
                if(user.Name == null || user.Email == null)
                {
                    return "Nome ou E-mail não podem ser vazios!";
                }
				Database.Users.Add(user);
				Database.SaveChanges();
				return "Usuario adicionado com sucesso!\n" + user.ToString();
			});
			
			//atualizar usuario
			app.MapPost("/updateUser/{id}", (Database Database, User updatedUser, int id) =>
			{
				var user = Database.Users.Find(id);
				user!.Name = updatedUser.Name;
				user.Email = updatedUser.Email;
                user.Cellphone = updatedUser.Cellphone;
				Database.SaveChanges();
				return "Usuario atualizado com sucesso!";
			});
						
			//deletar usuario
			app.MapPost("/deleteUser/{id}", (Database Database, int id) =>
			{
				var usuario = Database.Users.Find(id);
				Database.Remove(usuario);
				Database.SaveChanges();
				return "Usuario atualizado com sucesso!";
			});

            //---------------------- CRUD DO AGENTE DE SUPORTE

            //listar todos os agentes de suporte
			app.MapGet("/agents", (Database Database) => 
            {
				return Database.Agents.ToList();
			});
			
			//listar agente especifico (por id)
			app.MapGet("/agent/{id}", (Database Database, int id) => 
            {
				return Database.Agents.Find(id);
			});
			
			//cadastrar agente de suporte
			app.MapPost("/agentRegister", (Database Database, SupportAgent agent) =>
			{
                if(agent.Name == null)
                {
                    return "Nome do agente não pode ser vazio!";
                }
                if(agent.Email == null)
                {
                    return "E-mail não pode ser vazio!";
                }
				Database.Agents.Add(agent);
				Database.SaveChanges();
				return "Agente adicionado com sucesso!";
			});
			
			//atualizar usuario
			app.MapPost("/update/{id}", (Database Database, SupportAgent updatedAgent, int id) =>
			{
                if(updatedAgent.Id.Equals(false) || updatedAgent.Name == null || updatedAgent.Email == null || updatedAgent.Cellphone.Equals(false))
                {
                    return "Os campos não podem estar vazios!";
                }
				var agent = Database.Agents.Find(id);
				agent.Name = updatedAgent.Name;
				agent.Email = updatedAgent.Email;
                agent.Cellphone = updatedAgent.Cellphone;
				Database.SaveChanges();
				return "Agente atualizado com sucesso!";
			});
						
			//deletar usuario
			app.MapPost("/delete/{id}", (Database Database, int id) =>
			{
                if(Database.Agents.Find(id) == null)
                {
                    return "Agente não localizado!";
                }
				var agent = Database.Agents.Find(id);
				Database.Remove(agent);
				Database.SaveChanges();
				return "Agente atualizado com sucesso!";
			});

            //---------------------- ENTIDADE DE RELACIONAMENTO - ABERTURA DE CHAMADO

            //listar todos os tickets
			app.MapGet("/tickets", (Database Database) => {
				return Database.ServicesRequest.ToList();
			});

            //listar todos os tickets
			app.MapGet("/closedTickets", (Database Database) => 
            {
                var tickets = Database.ServicesRequest.ToList();
                var closedTickets = new List<ServiceRequest>();
                foreach (var t in tickets)
                {
                    if(t.CloseTime.Year != 0001)
                    {
                        closedTickets.Add(t);
                    }
                }
                return closedTickets;
			});
			
			//listar ticket especifico (por id)
			app.MapGet("/ticket/{id}", (Database Database, int id) => 
            {
				return Database.ServicesRequest.Find(id);
			});
			
			//abrindo o chamado
			app.MapPost("/openTicket", (Database Database, ServiceRequest serviceRequest) =>
			{
                if(Database.Users.Find(serviceRequest.UserId) == null)
                {
                    return "Usuário não localizado!";
                }
                if (Database.Agents.Find(serviceRequest.AgentId) == null)
                {
                    return "Agente não localizado para atribuir chamado!";
                }
                serviceRequest.OpeningTime = DateTime.Now;
				Database.ServicesRequest.Add(serviceRequest);
				Database.SaveChanges();
				return "Chamado aberto com sucesso! Número: " + serviceRequest.Id;
			});
			
			//atualizar agente do chamado
			app.MapPost("/reatribuirChamado/{id}", (Database Database, ServiceRequest updatedTask, int id) =>
			{
                if(updatedTask.AgentId == 0 || updatedTask.AgentId == null)
                {
                    return "Não é possível alterar o agente para vazio.";
                }
				var task = Database.ServicesRequest.Find(id);
				task.AgentId = updatedTask.AgentId;
				Database.SaveChanges();
				return "Chamado " + updatedTask.Id + " atribuído ao agente " + Database.Agents.Find(updatedTask.AgentId).Name + " com sucesso!";
			});
						
			//encerrar chamado
			app.MapPost("/closeTicket/{id}", (Database Database, int id) =>
			{
                if(Database.ServicesRequest.Find(id) == null)
                {
                    return "Chamado não localizado!";
                }
				var serviceRequest = Database.ServicesRequest.Find(id);
				serviceRequest.CloseTime = DateTime.Now;
				Database.SaveChanges();
				return "Chamado encerrado com sucesso!";
			});
						
			app.Run();
		}
	}
}
