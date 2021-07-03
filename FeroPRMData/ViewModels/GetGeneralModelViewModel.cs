using System;
using System.Collections.Generic;

namespace FeroPRMData.ViewModels
{
    public class GetGeneralModelViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? Gender { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<GetStyleViewModel> Styles { get; set; }
    }
}
