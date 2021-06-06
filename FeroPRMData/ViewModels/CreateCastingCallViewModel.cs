using System;
using System.Collections.Generic;
using System.Text;

namespace Fero.Data.ViewModels
{
    public class CreateCastingCallViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? MonopolyTime { get; set; }
        public DateTime? OpenTime { get; set; }
        public DateTime? CloseTime { get; set; }
        public byte Status { get; set; }
        public string CustomerId { get; set; }
        public decimal? Salary { get; set; }
    }
}
