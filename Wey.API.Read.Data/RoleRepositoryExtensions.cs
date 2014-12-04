using System.Linq;
using Wey.Domain;

namespace Wey.API.Read
{
    public static class RoleRepositoryExtensions
    {
        /// <summary>
        ///     Gets the name of the single by role.
        /// </summary>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        public static Role GetSingleByRoleName(this IEntityRepository<Role> roleRepository, string roleName)
        {
            return roleRepository.FindBy(x => x.Name == roleName).FirstOrDefault();
        }
    }
}