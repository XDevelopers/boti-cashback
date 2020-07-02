using Boticario.EuRevendedor.Interfaces.Service;
using Boticario.EuRevendedor.Interfaces.Models;
using Boticario.EuRevendedor.Interfaces.Repository;

namespace Boticario.EuRevendedor.Service
{
    public class UserBusiness : IUserBusiness
    {
        private IUserRepository userRepository;

        public UserBusiness(IUserRepository repository)
        {
            userRepository = repository;
        }

        public IUser GetUser(string email, string password)
        {

            var hashPassword = password; // encript password
            return userRepository.Find(u => u.Email == email && password == hashPassword);
        }
    }
}
