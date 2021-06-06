﻿using System;
namespace Fero.Data.ViewModels
{
    public class DetailCastingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? MonopolyTime { get; set; }
        public DateTime? OpenTime { get; set; }
        public DateTime? CloseTime { get; set; }
        public byte Status { get; set; }
        public string CustomerId { get; set; }
        public int? BrandId { get; set; }
        public decimal Salary { get; set; }
        public string CustomerName { get; set; }
    }
}
