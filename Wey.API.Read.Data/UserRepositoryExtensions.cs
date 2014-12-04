using System.Linq;
using Wey.Domain;

namespace Wey.API.Read
{
    public static class UserRepositoryExtensions
    {
        /// <summary>
        /// Gets the single by username.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public static User GetSingleByUsername(this IEntityRepository<User> userRepository, string username)
        {
            return userRepository.FindBy(u => u.Name == username).FirstOrDefault();
        }
    }
}