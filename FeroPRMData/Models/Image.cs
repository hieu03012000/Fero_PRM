using System;
using System.Collections.Generic;

namespace FeroPRMData.Models
{
    public partial class Image
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string ModelId { get; set; }
        public virtual Model Model { get; set; }
    }
}
