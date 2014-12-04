using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wey.Domain
{
    public class BenefitState : IEntity
    {
        [Key]
        public Guid Key { get; set; }

        public Guid BenefitKey { get; set; }

        public BenefitStatus BenefitStatus { get; set; }
    }


    public enum BenefitStatus
    {
        Ordered = 1,
        Scheduled = 2,
        InTransit = 3,
        Delivered = 4
    }
}
