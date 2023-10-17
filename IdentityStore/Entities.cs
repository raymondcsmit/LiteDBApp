using Microsoft.AspNetCore.Identity;

namespace LiteDBApp.IdentityStore
{
	public class LiteDbUser : IdentityUser<int>
	{
		//public string Id { get; set; }
		public string UserName { get; set; }
		public string NormalizedUserName { get; set; }
		// Add other user properties as needed
	}

	public class LiteDbRole : IdentityRole<int>
	{
		//public string Id { get; set; }
		public string Name { get; set; }
		public string NormalizedName { get; set; }
		// Add other role properties as needed
	}

}
