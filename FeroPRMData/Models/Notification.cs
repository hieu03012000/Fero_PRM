using System;

namespace FeroPRMData.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public DateTime? Time { get; set; }
        public LINK_OBJECT_TYPE? LinkObjectType { get; set; }
        public int? LinkObjectId { get; set; }
        public string UserId { get; set; }
    }

    public enum LINK_OBJECT_TYPE
    {
        CASTING,
        OFFER
    }
}
