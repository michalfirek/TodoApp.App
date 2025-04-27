using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TodoApp.App.ViewModels;
using TodoApp.Services.Interfaces;

namespace TodoApp.App.Views;

public partial class MainWindow : Window
{
	public MainWindow(ITodoService todoService)
	{
		InitializeComponent();
		DataContext = new MainViewModel(todoService);
	}
}