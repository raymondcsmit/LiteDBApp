using LiteDBApp.DB;

namespace LiteDBApp.Services
{
	public class UserService
	{
		private readonly ILiteDbRepository<User> _userRepository;

		public UserService(ILiteDbRepository<User> userRepository)
		{
			_userRepository = userRepository;
		}

		public User GetUserById(int id)
		{
			return _userRepository.GetById(id);
		}

		public IEnumerable<User> GetAllUsers()
		{
			return _userRepository.GetAll();
		}

		public void CreateUser(User user)
		{
			// You can perform validation or other business logic here
			_userRepository.Insert(user);
		}

		public void UpdateUser(User user)
		{
			// You can perform validation or other business logic here
			_userRepository.Update(user);
		}

		public void DeleteUser(int id)
		{
			_userRepository.Delete(id);
		}
	}

}
