using System;
using System.Collections.Generic;

namespace FeroPRMData.Models
{
    public partial class FavoriteModel
    {
        public string ModelId { get; set; }
        public string CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Model Model { get; set; }
    }
}
