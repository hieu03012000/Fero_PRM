using System;
using System.Collections.Generic;

namespace FeroPRMData.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Casting = new HashSet<Casting>();
            Notification = new HashSet<Notification>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Web { get; set; }
        public string TaxCode { get; set; }
        public string Phone { get; set; }
        public string Gmail { get; set; }
        public string Fanpage { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<Casting> Casting { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
    }
}
