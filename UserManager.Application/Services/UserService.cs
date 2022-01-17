using System.Collections.Generic;
using System.Threading.Tasks;
using UserManager.Domain;
using UserManager.Infrastructure.Persistance;

namespace UserManager.Application.Services
{
    public class UserService
    {
        public UserRepository userRepository { get; set; } = new UserRepository();

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var filter = new UserFilter();
            var users = await userRepository.GetAll(filter);

            return users;
        }
        public async Task<IEnumerable<User>> GetAllUsers(UserFilter filter)
        {
            var users = await userRepository.GetAll(filter);

            return users;
        }

        public async Task<User> GetUser(UserFilter filter)
        {
            var user = await userRepository.Get(filter);

            return user;
        }

        public async Task AddUser(User entity)
        {
            await userRepository.Insert(entity);
        }

        public async Task UpdateUser(User entity)
        {
            await userRepository.Update(entity);
        }
    }
}
