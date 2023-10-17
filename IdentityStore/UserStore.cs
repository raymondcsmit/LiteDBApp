namespace LiteDBApp.IdentityStore
{
	using LiteDBApp.DB;
	using Microsoft.AspNetCore.Identity;
	using System.Threading;
	using System.Threading.Tasks;

	public class LiteDbUserStore : IUserStore<LiteDbUser>
	{
		private readonly IIdentityRepository<LiteDbUser> _userRepository;

		public LiteDbUserStore(IIdentityRepository<LiteDbUser> userRepository)
		{
			_userRepository = userRepository;
		}

		public Task<IdentityResult> CreateAsync(LiteDbUser user, CancellationToken cancellationToken)
		{
			_userRepository.Insert(user);
			return Task.FromResult(IdentityResult.Success);
		}

		public Task<IdentityResult> DeleteAsync(LiteDbUser user, CancellationToken cancellationToken)
		{
			_userRepository.Delete(user.Id);
			return Task.FromResult(IdentityResult.Success);
		}

		public void Dispose()
		{

		}

		public Task<LiteDbUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
		{
			int id = Convert.ToInt32(userId);
			var doc = _userRepository.GetById(id);
			return Task.FromResult(doc);
		}
		public Task<LiteDbUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
		{
			// Implement the logic to find a user by normalized username
			var user = _userRepository.GetAll().FirstOrDefault(u => u.NormalizedUserName == normalizedUserName);
			return Task.FromResult(user);
		}

		public Task<string> GetNormalizedUserNameAsync(LiteDbUser user, CancellationToken cancellationToken)
		{
			// Implement the logic to get the normalized username
			return Task.FromResult(user.NormalizedUserName);
		}

		public Task<string> GetUserIdAsync(LiteDbUser user, CancellationToken cancellationToken)
		{
			// Implement the logic to get the user's ID
			return Task.FromResult(user.Id.ToString());
		}

		public Task<string> GetUserNameAsync(LiteDbUser user, CancellationToken cancellationToken)
		{
			// Implement the logic to get the username
			return Task.FromResult(user.UserName);
		}

		public Task SetNormalizedUserNameAsync(LiteDbUser user, string normalizedName, CancellationToken cancellationToken)
		{
			// Implement the logic to set the normalized username
			user.NormalizedUserName = normalizedName;
			return Task.CompletedTask;
		}

		public Task SetUserNameAsync(LiteDbUser user, string userName, CancellationToken cancellationToken)
		{
			// Implement the logic to set the username
			user.UserName = userName;
			return Task.CompletedTask;
		}

		public Task<IdentityResult> UpdateAsync(LiteDbUser user, CancellationToken cancellationToken)
		{
			// Implement the logic to update the user
			_userRepository.Update(user);
			return Task.FromResult(IdentityResult.Success);
		}

	}

}
