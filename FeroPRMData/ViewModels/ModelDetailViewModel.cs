using System;
using System.Collections.Generic;

namespace Fero.Data.ViewModels
{
    public class ModelDetailViewModel
    {
        public ModelDetailViewModel()
        {
            Image = new HashSet<ModelDetailImageViewModel>();
            ModelStyle = new HashSet<ModelDetailModelStyleViewModel>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? Gender { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Bust { get; set; }
        public decimal? Waist { get; set; }
        public decimal? Hip { get; set; }
        public string SocialNetworkLink { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public string Gmail { get; set; }

        public virtual ICollection<ModelDetailImageViewModel> Image { get; set; }
        public virtual ICollection<ModelDetailModelStyleViewModel> ModelStyle { get; set; }
    }
   
    public partial class ModelDetailModelStyleViewModel
    {
        public int StyleId { get; set; }
        public string StyleName { get; set; }
    }

    public partial class ModelDetailImageViewModel
    {
        public string Link { get; set; }

    }
}


