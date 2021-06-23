using System;
using System.Collections.Generic;

namespace FeroPRMData.ViewModels
{
    public class GetModelViewModel
    {
        public GetModelViewModel()
        {
            Images = new HashSet<GetModelImageViewModel>();
            Styles = new HashSet<GetModelStyleViewModel>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? Gender { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public double? Bust { get; set; }
        public double? Waist { get; set; }
        public double? Hip { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public string Gmail { get; set; }
        public int? Status { get; set; }
        public string SocialNetworkLink { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public virtual ICollection<GetModelStyleViewModel> Styles { get; set; }
        public virtual ICollection<GetModelImageViewModel> Images { get; set; }
    }

    public partial class GetModelStyleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public partial class GetModelImageViewModel
    {
        public int Id { get; set; }
        public string Link { get; set; }

    }

}
