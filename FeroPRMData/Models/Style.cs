using System;
using System.Collections.Generic;

namespace FeroPRMData.Models
{
    public partial class Style
    {
        public Style()
        {
            ModelStyle = new HashSet<ModelStyle>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ModelStyle> ModelStyle { get; set; }
    }
}
