using System;
using System.Collections.Generic;

namespace FeroPRMData.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public DateTime? Time { get; set; }
        public string UserId { get; set; }
    }
}
