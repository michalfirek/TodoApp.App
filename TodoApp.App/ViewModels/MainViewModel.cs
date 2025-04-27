using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoApp.Services.DTOs;
using TodoApp.Services.Interfaces;

namespace TodoApp.App.ViewModels
{
	class MainViewModel: INotifyPropertyChanged
	{
		private readonly ITodoService _todoService;
		private TodoDto _selectedTodo;

		public event PropertyChangedEventHandler? PropertyChanged;

		public ObservableCollection<TodoDto> Todos { get; set; } = new ObservableCollection<TodoDto>();

		public TodoDto SelectedTodo
		{
			get => _selectedTodo;
			set
			{
				_selectedTodo = value;
				OnPropertyChanged();
			}
		}

		// Komendy
		public ICommand AddCommand { get; }
		public ICommand UpdateCommand { get; }
		public ICommand DeleteCommand { get; }

		public MainViewModel(ITodoService todoService)
		{
			_todoService = todoService;
			AddCommand = new RelayCommand(async () => await AddTodo());
			UpdateCommand = new RelayCommand(async () => await UpdateTodo(), () => SelectedTodo != null);
			DeleteCommand = new RelayCommand(async () => await DeleteTodo(), () => SelectedTodo != null);

			// Wczytaj dane przy starcie
			_ = LoadTodos();
		}

		private async Task LoadTodos()
		{
			var items = await _todoService.GetAllTodosAsync();
			Todos.Clear();
			foreach (var item in items)
			{
				Todos.Add(item);
			}
		}

		private async Task AddTodo()
		{
			var newTodo = new TodoDto
			{
				Title = "Nowe zadanie",
				Description = "Opis",
				IsDone = false
			};
			await _todoService.AddTodoAsync(newTodo);
			await LoadTodos();
		}

		private async Task UpdateTodo()
		{
			if (SelectedTodo != null)
			{
				await _todoService.UpdateTodoAsync(SelectedTodo);
				await LoadTodos();
			}
		}

		private async Task DeleteTodo()
		{
			if (SelectedTodo != null)
			{
				await _todoService.DeleteTodoAsync(SelectedTodo.Id);
				await LoadTodos();
			}
		}

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	// Przykładowa implementacja komendy
	public class RelayCommand : ICommand
	{
		private readonly Func<Task> _executeAsync;
		private readonly Func<bool> _canExecute;

		public RelayCommand(Func<Task> executeAsync, Func<bool> canExecute = null)
		{
			_executeAsync = executeAsync;
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

		public async void Execute(object parameter) => await _executeAsync();

		public event System.EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
	}
}
