using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Data;
using TodoApp.Services.DTOs;

namespace TodoApp.Services.Interfaces
{
	interface ITodoService
	{
		public Task<List<TodoDto>> GetAllTodosAsync();
		public Task<TodoDto> GetTodoByIdAsync(int id);
		public Task AddTodoAsync(TodoDto todo);
		public Task UpdateTodoAsync(TodoDto todo);
		public Task DeleteTodoAsync(int id);
	}
}
