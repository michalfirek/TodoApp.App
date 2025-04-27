using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Services.DTOs
{
	public class TodoDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public bool IsDone { get; set; }
	}
}