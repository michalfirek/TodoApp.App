using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Data.Context;
using TodoApp.Data.Model;

namespace TodoApp.Data.Repositories
{
	class ToDoRepository
	{
		private readonly AppDbContext _context;

		public ToDoRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<List<Todo>> GetAllAsync()
		{
			return await _context.Todos.ToListAsync(); 
		}

		public async Task<Todo> GetByIdAsync(int id)
		{
			return await _context.Todos.FindAsync(id);
		}

		public async Task AddAsync(Todo todo)
		{
			_context.Todos.Add(todo);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Todo todo)
		{
			_context.Todos.Update(todo);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var todo = await GetByIdAsync(id);
			if (todo != null)
			{
				_context.Todos.Remove(todo);
				await _context.SaveChangesAsync();
			}
		}
	}
}
