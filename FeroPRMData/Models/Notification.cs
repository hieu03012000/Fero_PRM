using System;

namespace FeroPRMData.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public string Description { get; set; }
        // 0 : casting, 1: offer
        public int? LinkObjectType { get; set; }
        public int? LinkObjectId { get; set; }
        public int? Status { get; set; }
        public DateTime? Time { get; set; }
        public string UserId { get; set; }
    }
}
