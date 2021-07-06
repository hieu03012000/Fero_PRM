using System;
using System.Collections.Generic;

namespace FeroPRMData.ViewModels
{
    public class OfferViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? MonopolisticTime { get; set; }
        public double? Salary { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CustomerId { get; set; }
    }

    public class OfferCustomerGetViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? MonopolisticTime { get; set; }
        public double? Salary { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CustomerId { get; set; }
        public List<ModelOfferCustomerGetViewModel> ModelOffers { get; set; }
    }
}
