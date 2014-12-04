using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Wey.Domain
{
    public class Affiliate : IEntity
    {
        public virtual ICollection<Benefit> Benefits { get; set; }

        [Required]
        [StringLength(50)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(50)]
        public string TelephoneNumber { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public User User { get; set; }

        [Key]
        public Guid Key { get; set; }

        public Affiliate()
        {
            Benefits = new HashSet<Benefit>();
        }
    }
}