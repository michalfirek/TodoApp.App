using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using TodoApp.App.Views;
using TodoApp.Data.Context;
using TodoApp.Data.Repositories;
using TodoApp.Services.Implementations;
using TodoApp.Services.Interfaces;

namespace TodoApp.App;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	private ServiceProvider _serviceProvider;

	protected override void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);

		var services = new ServiceCollection();

		// Rejestracja wszystkich serwisów:
		services.AddDbContext<AppDbContext>();
		services.AddTransient<ToDoRepository>();
		services.AddTransient<ITodoService, TodoService>();

		// Rejestracja MainWindow z wstrzykiwaniem ITodoService
		services.AddTransient<MainWindow>();

		_serviceProvider = services.BuildServiceProvider();

		var mainWindow = _serviceProvider.GetService<MainWindow>();
		mainWindow.Show();
	}

}