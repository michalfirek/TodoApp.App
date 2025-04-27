using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Services.DTOs;
using TodoApp.Services.Interfaces;
using TodoApp.Data.Repositories;
using TodoApp.Data.Model;

namespace TodoApp.Services.Implementations
{
	class TodoService : ITodoService
	{
		private readonly ToDoRepository _todoRepository;

		public TodoService(ToDoRepository todoRepository)
		{
			_todoRepository = todoRepository;
		}

		public async Task AddTodoAsync(TodoDto todo)
		{
			var entity = new Todo
			{
				Title = todo.Title,
				Description = todo.Description,
				IsDone = todo.IsDone
			};
			await _todoRepository.AddAsync(entity);
		}

		public async Task DeleteTodoAsync(int id)
		{
			await _todoRepository.DeleteAsync(id);
		}

		public async Task<List<TodoDto>> GetAllTodosAsync()
		{
			var todos = await _todoRepository.GetAllAsync();
			// Mapowanie Model -> DTO
			return todos.Select(e => new TodoDto
			{
				Id = e.Id,
				Title = e.Title,
				Description = e.Description,
				IsDone = e.IsDone
			}).ToList();
		}

		public async Task<TodoDto> GetTodoByIdAsync(int id)
		{
			var entity = await _todoRepository.GetByIdAsync(id);
			if (entity == null) return null;

			return new TodoDto
			{
				Id = entity.Id,
				Title = entity.Title,
				Description = entity.Description,
				IsDone = entity.IsDone
			};
		}

		public Task UpdateTodoAsync(TodoDto todo)
		{
			throw new NotImplementedException();
		}
	}
}
