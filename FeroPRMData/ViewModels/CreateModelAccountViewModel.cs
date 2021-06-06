using System;
using System.Collections.Generic;

namespace Fero.Data.ViewModels
{
    public class CreateModelAccountViewModel
    {
        public CreateModelAccountViewModel()
        {
            Image = new HashSet<CreateAccountImageViewModel>();
            ModelStyle = new HashSet<CreateAccountModelStyleViewModel>();
        }
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

        public virtual ICollection<CreateAccountImageViewModel> Image { get; set; }
        public virtual ICollection<CreateAccountModelStyleViewModel> ModelStyle { get; set; }
    }

    public partial class CreateAccountModelStyleViewModel
    {
        public int StyleId { get; set; }
    }

    public partial class CreateAccountImageViewModel
    {
        public string Link { get; set; }

    }
}
