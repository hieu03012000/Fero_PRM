using System;
using System.Collections.Generic;

namespace FeroPRMData.Models
{
    public partial class Model
    {
        public Model()
        {
            ApplyCasting = new HashSet<ApplyCasting>();
            FavoriteModel = new HashSet<FavoriteModel>();
            Image = new HashSet<Image>();
            ModelOffer = new HashSet<ModelOffer>();
            ModelStyle = new HashSet<ModelStyle>();
            SubscribeCasting = new HashSet<SubscribeCasting>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? Gender { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public double? Bust { get; set; }
        public double? Waist { get; set; }
        public double? Hip { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public string Gmail { get; set; }
        public int? Status { get; set; }
        public string SocialNetworkLink { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string DeviceToken { get; set; }

        public virtual ICollection<ApplyCasting> ApplyCasting { get; set; }
        public virtual ICollection<FavoriteModel> FavoriteModel { get; set; }
        public virtual ICollection<Image> Image { get; set; }
        public virtual ICollection<ModelOffer> ModelOffer { get; set; }
        public virtual ICollection<ModelStyle> ModelStyle { get; set; }
        public virtual ICollection<SubscribeCasting> SubscribeCasting { get; set; }
    }

    public class ListModelCasting
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? Gender { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string Avatar { get; set; }
    }

    public class ModelForOffer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public int? OfferStatus { get; set; }
    }
}
