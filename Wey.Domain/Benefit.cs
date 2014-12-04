using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wey.Domain
{
    public class Benefit : IEntity
    {
        public Guid AffiliateKey { get; set; }

        public Guid BenefitTypeKey { get; set; }

        public decimal Value { get; set; }

        [StringLength(50)]
        public string ReceiverName { get; set; }

        [Required]
        [StringLength(50)]
        public string ReceiverSurname { get; set; }

        [Required]
        [StringLength(50)]
        public string ReceiverAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string ReceiverPostCode { get; set; }

        [Required]
        [StringLength(50)]
        public string ReceiverCity { get; set; }

        [Required]
        [StringLength(50)]
        public string ReceiverCountry { get; set; }

        [Required]
        [StringLength(50)]
        public string ReceiverTelephone { get; set; }

        [Required]
        [StringLength(320)]
        public string ReceiverEmail { get; set; }

        public DateTime CreatedOn { get; set; }

        public Affiliate Affiliate { get; set; }

        public BenefitType BenefitType { get; set; }

        public ICollection<BenefitState> BenefitStates { get; set; }

        [Key]
        public Guid Key { get; set; }

        public Benefit()
        {
            BenefitStates = new HashSet<BenefitState>();
        }
    }
}