using System;
namespace FeroPRMData.ViewModels
{
    public class DetailCastingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Salary { get; set; }
        public int? MonopolisticTime { get; set; }
        public DateTime? OpenTime { get; set; }
        public DateTime? CloseTime { get; set; }
        public int? Status { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}
