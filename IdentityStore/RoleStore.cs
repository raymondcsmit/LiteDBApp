using LiteDBApp.DB;
using Microsoft.AspNetCore.Identity;

namespace LiteDBApp.IdentityStore
{
	public class LiteDbRoleStore : IRoleStore<LiteDbRole>
	{
		private readonly IIdentityRepository<LiteDbRole> _roleRepository;

		public LiteDbRoleStore(IIdentityRepository<LiteDbRole> roleRepository)
		{
			_roleRepository = roleRepository;
		}

		public Task<IdentityResult> CreateAsync(LiteDbRole role, CancellationToken cancellationToken)
		{
			_roleRepository.Insert(role);
			return Task.FromResult(IdentityResult.Success);
		}
		public Task<IdentityResult> DeleteAsync(LiteDbRole role, CancellationToken cancellationToken)
		{
			// Implement the logic to delete a role
			_roleRepository.Delete(role.Id);
			return Task.FromResult(IdentityResult.Success);
		}

		public void Dispose()
		{
			// Implement the dispose logic if necessary
		}

		public Task<LiteDbRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
		{
			// Implement the logic to find a role by ID
			int id = Convert.ToInt32(roleId);
			var role = _roleRepository.GetById(id);
			return Task.FromResult(role);
		}

		public Task<LiteDbRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
		{
			// Implement the logic to find a role by normalized role name
			var role = _roleRepository.GetAll().FirstOrDefault(r => r.NormalizedName == normalizedRoleName);
			return Task.FromResult(role);
		}

		public Task<string> GetNormalizedRoleNameAsync(LiteDbRole role, CancellationToken cancellationToken)
		{
			// Implement the logic to get the normalized role name
			return Task.FromResult(role.NormalizedName);
		}

		public Task<string> GetRoleIdAsync(LiteDbRole role, CancellationToken cancellationToken)
		{
			// Implement the logic to get the role's ID
			return Task.FromResult(role.Id.ToString());
		}

		public Task<string> GetRoleNameAsync(LiteDbRole role, CancellationToken cancellationToken)
		{
			// Implement the logic to get the role name
			return Task.FromResult(role.Name);
		}

		public Task SetNormalizedRoleNameAsync(LiteDbRole role, string normalizedName, CancellationToken cancellationToken)
		{
			// Implement the logic to set the normalized role name
			role.NormalizedName = normalizedName;
			return Task.CompletedTask;
		}

		public Task SetRoleNameAsync(LiteDbRole role, string roleName, CancellationToken cancellationToken)
		{
			// Implement the logic to set the role name
			role.Name = roleName;
			return Task.CompletedTask;
		}

		public Task<IdentityResult> UpdateAsync(LiteDbRole role, CancellationToken cancellationToken)
		{
			// Implement the logic to update a role
			_roleRepository.Update(role);
			return Task.FromResult(IdentityResult.Success);
		}
	}
}
