using System;

namespace FeroPRMData.ViewModels
{
    public class CastingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Salary { get; set; }
        public int? MonopolisticTime { get; set; }
        public DateTime? OpenTime { get; set; }
        public DateTime? CloseTime { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CustomerId { get; set; }
    }

    public class CastingModelSearchViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Salary { get; set; }
        public DateTime? OpenTime { get; set; }
        public DateTime? CloseTime { get; set; }
        public string CustomerName { get; set; }
    }

    public class CastingModelGetViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Salary { get; set; }
        public int? MonopolisticTime { get; set; }
        public DateTime? OpenTime { get; set; }
        public DateTime? CloseTime { get; set; }
        public string CustomerName { get; set; }
        public bool IsSubscribe { get; set; }
        public bool IsApply { get; set; }
    }

    public class CastingImportViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CastingPublicViewModel
    {
        public DateTime? OpenTime { get; set; }
        public DateTime? CloseTime { get; set; }
    }
}
