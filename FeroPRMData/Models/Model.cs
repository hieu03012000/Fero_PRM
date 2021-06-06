using System;
using System.Collections.Generic;

namespace FeroPRMData.Models
{
    public partial class Model
    {
        public Model()
        {
            ApplyCasting = new HashSet<ApplyCasting>();
            Image = new HashSet<Image>();
            ModelCasting = new HashSet<ModelCasting>();
            ModelStyle = new HashSet<ModelStyle>();
            Notification = new HashSet<Notification>();
            SubscribeCasting = new HashSet<SubscribeCasting>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? Gender { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Bust { get; set; }
        public decimal? Waist { get; set; }
        public decimal? Hip { get; set; }
        public string SocialNetworkLink { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public string Gmail { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<ApplyCasting> ApplyCasting { get; set; }
        public virtual ICollection<Image> Image { get; set; }
        public virtual ICollection<ModelCasting> ModelCasting { get; set; }
        public virtual ICollection<ModelStyle> ModelStyle { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual ICollection<SubscribeCasting> SubscribeCasting { get; set; }
    }
}
