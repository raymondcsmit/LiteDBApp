using LiteDBApp.DB;

namespace LiteDBApp.Services
{
	public class User : BaseEntity
	{
		public string UserName { get; set; }
		public string FirsName { get; set; }
		public string LastName { get; set; }
		public DateTime DateOfBirth { get; set; } = DateTime.Now.AddYears(-20);
	}
}
