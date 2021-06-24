using System;
using System.Collections.Generic;

namespace FeroPRMData.Models
{
    public partial class ModelStyle
    {
        public string ModelId { get; set; }
        public int StyleId { get; set; }

        public virtual Model Model { get; set; }
        public virtual Style Style { get; set; }
    }

    public  class ModelStyleGeneral
    {
        public string ModelId { get; set; }
        public int StyleId { get; set; }
    }
}
