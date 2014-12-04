using System.Data.Entity;

namespace Wey.Domain
{
    public class EntitiesContext : DbContext
    {
        public EntitiesContext() : base("Wey")
        {
        }

        public IDbSet<User> Users { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<UserInRole> UserInRoles { get; set; }
        public IDbSet<Affiliate> Affiliates { get; set; }
        public IDbSet<Benefit> Benefits { get; set; }
        public IDbSet<BenefitType> BenefitTypes { get; set; }
    }
}