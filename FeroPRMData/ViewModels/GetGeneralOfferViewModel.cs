using System;

namespace FeroPRMData.ViewModels
{
    public class GetGeneralOfferViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? MonopolisticTime { get; set; }
        public double? Salary { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CustomerName { get; set; }
        public int? ModelOfferStatus { get; set; }
    }
}
