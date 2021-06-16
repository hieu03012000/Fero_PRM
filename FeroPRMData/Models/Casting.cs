using System;
using System.Collections.Generic;

namespace FeroPRMData.Models
{
    public partial class Casting
    {
        public Casting()
        {
            ApplyCasting = new HashSet<ApplyCasting>();
            ModelCasting = new HashSet<ModelCasting>();
            SubscribeCasting = new HashSet<SubscribeCasting>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Salary { get; set; }
        public int? MonopolisticTime { get; set; }
        public long? OpenTime { get; set; }
        public long? CloseTime { get; set; }
        public int? Status { get; set; }
        public string CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<ApplyCasting> ApplyCasting { get; set; }
        public virtual ICollection<ModelCasting> ModelCasting { get; set; }
        public virtual ICollection<SubscribeCasting> SubscribeCasting { get; set; }
    }
}
