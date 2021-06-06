using System.Collections.Generic;

namespace Fero.Data.ViewModels
{
    public class MakeOfferViewModel
    {
        public MakeOfferViewModel()
        {
            ModelCasting = new HashSet<MakeOfferModelCastingViewModel>();
        }
        public string Name { get; set; }    
        public string Description { get; set; }
        public int? MonopolyTime { get; set; }
        public string CustomerId { get; set; }
        public decimal? Salary { get; set; }
        public virtual ICollection<MakeOfferModelCastingViewModel> ModelCasting { get; set; }
    }

    public class MakeOfferModelCastingViewModel
    {
        public string ModelId { get; set; }
    }
}
