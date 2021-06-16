using System;
using System.Collections.Generic;

namespace FeroPRMData.Models
{
    public partial class Offer
    {
        public Offer()
        {
            ModelOffer = new HashSet<ModelOffer>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double? Salary { get; set; }
        public long? Time { get; set; }
        public string CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<ModelOffer> ModelOffer { get; set; }
    }
}
