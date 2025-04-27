using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Data.Model;

namespace TodoApp.Data.Context
{
	public class AppDbContext: DbContext
	{
		// Domyślne połączenie z SQLite
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// Plik bazy SQLite.db będzie w lokalizacji obok aplikacji
			optionsBuilder.UseSqlite("Data Source=todo.db");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Konfiguracja encji
			modelBuilder.Entity<Todo>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
			});
		}
		
		public DbSet<Todo> Todos { get; set; }
	}
}
