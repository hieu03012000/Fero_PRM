namespace FeroPRMData.ViewModels
{
    public class ModelOfferViewModel
    {
        public string ModelId { get; set; }
        public int OfferId { get; set; }
        public int? Status { get; set; }       
    }

    public class ModelOfferCustomerGetViewModel
    {
        public string ModelId { get; set; }
        public string ModelName { get; set; }
        public string ModelAvatar { get; set; }
        public int? Status { get; set; }
    }
}
