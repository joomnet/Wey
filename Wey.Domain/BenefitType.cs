using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wey.Domain
{
    public class BenefitType : IEntity
    {
        public BenefitType()
        {
            Benefits = new HashSet<Benefit>();
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public decimal Value { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Benefit> Benefits { get; set; }

        [Key]
        public Guid Key { get; set; }
    }
}