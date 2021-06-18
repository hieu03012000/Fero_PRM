using System;
using System.Collections.Generic;
using System.Text;

namespace FeroPRMData.ViewModels
{
    public class ApplicantListViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? Gender { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Bust { get; set; }
        public decimal? Waist { get; set; }
        public decimal? Hip { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public string Gmail { get; set; }
        public string SocialNetworkLink { get; set; }
    }
}
