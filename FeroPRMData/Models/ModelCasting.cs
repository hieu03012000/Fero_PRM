using System;
using System.Collections.Generic;

namespace FeroPRMData.Models
{
    public partial class ModelCasting
    {
        public string ModelId { get; set; }
        public int CastingId { get; set; }
        public int? Status { get; set; }
        public DateTime? Date { get; set; }

        public virtual Casting Casting { get; set; }
        public virtual Model Model { get; set; }
    }
}
