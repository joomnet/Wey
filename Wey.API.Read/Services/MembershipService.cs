using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Wey.API.Read.Services.Interfaces;
using Wey.Common;
using Wey.Domain;

namespace Wey.API.Read.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IEntityRepository<User> _userRepository;
        private readonly IEntityRepository<Role> _roleRepository;
        private readonly IEntityRepository<UserInRole> _userInRoleRepository;
        private readonly ICryptoService _cryptoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="userInRoleRepository">The user in role repository.</param>
        /// <param name="cryptoService">The crypto service.</param>
        public MembershipService(IEntityRepository<User> userRepository, IEntityRepository<Role> roleRepository, IEntityRepository<UserInRole> userInRoleRepository, ICryptoService cryptoService)
        {
            Throw.IfNull<ArgumentNullException>(userRepository, "userRepository");
            Throw.IfNull<ArgumentNullException>(roleRepository, "roleRepository");
            Throw.IfNull<ArgumentNullException>(userInRoleRepository, "userInRoleRepository");
            Throw.IfNull<ArgumentNullException>(cryptoService, "cryptoService");
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userInRoleRepository = userInRoleRepository;
            _cryptoService = cryptoService;
        }

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public ValidUserContext ValidateUser(string username, string password)
        {
            var userCtx = new ValidUserContext();
            var user = _userRepository.GetSingleByUsername(username);

            if (user == null || !IsValidUser(user, password)) return userCtx;

            var userRoles = GetUserRoles(user.Key);
            userCtx.User = new UserWithRoles()
            {
                User = user,
                Roles = userRoles
            };

            var identity = new GenericIdentity(user.Name);
            userCtx.Principal = new GenericPrincipal(identity, userRoles.Select(x => x.Name).ToArray());
            return userCtx;
        }

        /// <summary>
        /// Gets the user roles.
        /// </summary>
        /// <param name="userKey">The user key.</param>
        /// <returns></returns>
        private IEnumerable<Role> GetUserRoles(Guid userKey)
        {
            var userInRoles = _userInRoleRepository.FindBy(x => x.UserKey == userKey).ToList();

            if (userInRoles.Any())
            {
                var userRoleKeys = userInRoles.Select(x => x.RoleKey).ToArray(); 
                var userRoles = _roleRepository .FindBy(x => userRoleKeys.Contains( x.Key)); 
                return userRoles;
            }
            return Enumerable.Empty<Role>();

        }

        private bool IsValidUser(User user, string password)
        {
            return IsValidPassword(user, password) && !user.IsLocked;
        }

        private bool IsValidPassword(User user, string password)
        {
            return string.Equals(_cryptoService.EncryptPassword(password, user.Salt), user.HashedPassword);
        }

        public OperationResult<UserWithRoles> CreateUser(string username, string email, string password)
        {
            return CreateUser(username, password, email, roles: null);
        }

        public OperationResult<UserWithRoles> CreateUser(string username, string email, string password, string role)
        {
            return CreateUser(username, password, email, new[] {role});
        }

        public OperationResult<UserWithRoles> CreateUser(string username, string email, string password, string[] roles)
        {
            var existingUser = _userRepository.GetAll().Any(x => x.Name == username);

            if (existingUser)
            {
                return new OperationResult<UserWithRoles>(false);
            }

            var passwordSalt = _cryptoService.GenerateSalt();

            var user = new User()
            {
                Name = username,
                Salt = passwordSalt,
                Email = email,
                IsLocked = false,
                HashedPassword = _cryptoService.EncryptPassword(password, passwordSalt),
                CreatedOn = DateTime.Now
            };

            _userRepository.Add(user);
            _userRepository.Save();

            if (roles != null || roles.Any())
            {
                foreach (var roleName in roles)
                {
                    AddUserToRole(user, roleName);
                }
            }

            return new OperationResult<UserWithRoles>(true)
            {
                Entity = GetUserWithRoles(user)
            };
        }

        private UserWithRoles GetUserWithRoles(User user)
        {
            throw new NotImplementedException();
        }

        private void AddUserToRole(User user, string roleName)
        {
            var role = _roleRepository.get
        }

        public UserWithRoles UpdateUser(User user, string username, string email)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public bool AddToRole(Guid userKey, string role)
        {
            throw new NotImplementedException();
        }

        public bool AddToRole(string username, string role)
        {
            throw new NotImplementedException();
        }

        public bool RemoveFromRole(string username, string role)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetRoles()
        {
            throw new NotImplementedException();
        }

        public Role GetRole(Guid key)
        {
            throw new NotImplementedException();
        }

        public Role GetRole(string name)
        {
            throw new NotImplementedException();
        }
    }
}
