using System;
using System.Collections.Generic;

namespace FeroPRMData.Models
{
    public partial class Offer
    {
        public Offer()
        {
            ModelOffer = new HashSet<ModelOffer>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double? Salary { get; set; }
        public DateTime? Time { get; set; }
        public string CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<ModelOffer> ModelOffer { get; set; }
    }
    //isc301
    public class ShowOffer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double? Salary { get; set; }
        public DateTime? Time { get; set; }
        public string CustomerId { get; set; }
        public int? OfferStatus { get; set; }
    }

    public class OfferWithListModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double? Salary { get; set; }
        public DateTime? Time { get; set; }
        public string CustomerId { get; set; }
        public List<ModelForOffer> Model { get; set; }
    }

    public class CreateOffer
    {
        public Offer Offer { get; set; }
        public List<string> ListModelId { get; set; }
    }
}
