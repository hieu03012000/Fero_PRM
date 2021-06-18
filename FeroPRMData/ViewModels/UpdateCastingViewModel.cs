using System;
using System.Collections.Generic;
using System.Text;

namespace FeroPRMData.ViewModels
{
    public class UpdateCastingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Salary { get; set; }
        public int? MonopolisticTime { get; set; }
        public DateTime? OpenTime { get; set; }
        public DateTime? CloseTime { get; set; }
        public int? Status { get; set; }
        public string CustomerId { get; set; }
    }

    public class PublicCastingViewModel
    {
        public DateTime? OpenTime { get; set; }
        public DateTime? CloseTime { get; set; }
    }
}
