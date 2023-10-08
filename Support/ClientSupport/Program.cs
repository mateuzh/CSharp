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
			app.MapPost("/userRegister", (Database Database, User usuario) =>
			{
				Database.Users.Add(usuario);
				Database.SaveChanges();
				return "Usuario adicionado com sucesso!";
			});
			
			//atualizar usuario
			app.MapPost("/update/{id}", (Database Database, User usuarioAtualizado, int id) =>
			{
				var user = Database.Users.Find(id);
				user.Name = usuarioAtualizado.Name;
				user.Email = usuarioAtualizado.Email;
                user.Cellphone = usuarioAtualizado.Cellphone;
				Database.SaveChanges();
				return "Usuario atualizado com sucesso!";
			});
						
			//deletar usuario
			app.MapPost("/delete/{id}", (Database Database, int id) =>
			{
				var usuario = Database.Users.Find(id);
				Database.Remove(usuario);
				Database.SaveChanges();
				return "Usuario atualizado com sucesso!";
			});

            //---------------------- CRUD DO AGENTE DE SUPORTE

            //listar todos os agentes de suporte
			app.MapGet("/agents", (Database Database) => {
				return Database.Agents.ToList();
			});
			
			//listar agente especifico (por id)
			app.MapGet("/agent/{id}", (Database Database, int id) => {
				return Database.Agents.Find(id);
			});
			
			//cadastrar agente de suporte
			app.MapPost("/agentRegister", (Database Database, SupportAgent agent) =>
			{
				Database.Agents.Add(agent);
				Database.SaveChanges();
				return "Agente adicionado com sucesso!";
			});
			
			//atualizar usuario
			app.MapPost("/update/{id}", (Database Database, SupportAgent updatedAgent, int id) =>
			{
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
			
			//listar ticket especifico (por id)
			app.MapGet("/ticket/{id}", (Database Database, int id) => {
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
			app.MapPost("/update/{id}", (Database Database, ServiceRequest updatedTask, int id) =>
			{
				var task = Database.ServicesRequest.Find(id);
				task.AgentId = updatedTask.AgentId;
				Database.SaveChanges();
				return "Chamado " + updatedTask.Id + " atribuído ao agente " + Database.Agents.Find(updatedTask.AgentId).Name + " com sucesso!";
			});
						
			//encerrar chamado
			app.MapPost("/closeTicket/{id}", (Database Database, int id) =>
			{

				var serviceRequest = Database.ServicesRequest.Find(id);
				serviceRequest.CloseTime = DateTime.Now;
				Database.SaveChanges();
				return "Chamado encerrado com sucesso!";
			});
						
			app.Run();
		}
	}
}
