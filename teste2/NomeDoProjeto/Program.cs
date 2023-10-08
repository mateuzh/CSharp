//VERSAO WEBAPI COM DATABASE

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Trabalho
{
	class Usuario
    {
    	public int id { get; set; }
		public string? nome { get; set; }
    	public string? email { get; set; }
    }
	
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
			var builder = WebApplication.CreateBuilder(args);
			
			var connectionString = builder.Configuration.GetConnectionString("Usuarios") ?? "Data Source=Usuarios.db";
			builder.Services.AddSqlite<MinhaBase>(connectionString);
			
			var app = builder.Build();
			
			//listar todos os usuarios
			app.MapGet("/usuarios", (MinhaBase MinhaBase) => {
				return MinhaBase.Usuarios.ToList();
			});
			
			//listar usuario especifico (por email)
			app.MapGet("/usuario/{id}", (MinhaBase MinhaBase, int id) => {
				return MinhaBase.Usuarios.Find(id);
			});
			
			//cadastrar usuario
			app.MapPost("/cadastrar", (MinhaBase MinhaBase, Usuario usuario) =>
			{
				MinhaBase.Usuarios.Add(usuario);
				MinhaBase.SaveChanges();
				return "usuario adicionado";
			});
			
			//atualizar usuario
			app.MapPost("/atualizar/{id}", (MinhaBase MinhaBase, Usuario usuarioAtualizado, int id) =>
			{
				var usuario = MinhaBase.Usuarios.Find(id);
				usuario.nome = usuarioAtualizado.nome;
				usuario.email = usuarioAtualizado.email;
				MinhaBase.SaveChanges();
				return "usuario atualizado";
			});
						
			//deletar usuario
			app.MapPost("/deletar/{id}", (MinhaBase MinhaBase, int id) =>
			{
				var usuario = MinhaBase.Usuarios.Find(id);
				MinhaBase.Remove(usuario);
				MinhaBase.SaveChanges();
				return "usuario atualizado";
			});
						
			app.Run();
		}
	}
}
