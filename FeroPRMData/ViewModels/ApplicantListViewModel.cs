using System;
using System.Collections.Generic;
using System.Text;

namespace Fero.Data.ViewModels
{
    public class ApplicantListViewModel
    {
        public ApplicantListViewModel()
        {
            BodyPart = new HashSet<ModelDetailBodyPartViewModel>();
        }
        public string Name { get; set; }
        public byte Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<ModelDetailBodyPartViewModel> BodyPart { get; set; }
    }
}
