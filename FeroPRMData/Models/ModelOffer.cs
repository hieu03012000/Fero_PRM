using System;
using System.Collections.Generic;

namespace FeroPRMData.Models
{
    public partial class ModelOffer
    {
        public string ModelId { get; set; }
        public int OfferId { get; set; }
        public int? Status { get; set; }
        public DateTime? Time { get; set; }
        public virtual Model Model { get; set; }
        public virtual Offer Offer { get; set; }
    }

}
