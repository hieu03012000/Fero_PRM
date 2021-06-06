using System;
using System.Collections.Generic;

namespace Fero.Data.ViewModels
{
    public class ModelDetailViewModel
    {
        public ModelDetailViewModel()
        {
            BodyPart = new HashSet<ModelDetailBodyPartViewModel>();
            ModelStyle = new HashSet<ModelDetailModelStyleViewModel>();
            Product = new HashSet<ModelDetailProductViewModel>();
        }
        public string Name { get; set; }
        public byte Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public string SubAddress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gifted { get; set; }
        public virtual ICollection<ModelDetailBodyPartViewModel> BodyPart { get; set; }
        public virtual ICollection<ModelDetailModelStyleViewModel> ModelStyle { get; set; }
        public virtual ICollection<ModelDetailProductViewModel> Product { get; set; }
    }

    public class ModelDetailBodyPartViewModel
    {
        public ModelDetailBodyPartViewModel()
        {
            BodyAttribute = new HashSet<ModelDetailBodyAttributeViewModel>();
            CollectionImage = new HashSet<ModelDetailCollectionImageViewModel>();
        }
        public int? BodyPartTypeId { get; set; }
        public string BodyPartTypeName { get; set; }
        public virtual ICollection<ModelDetailBodyAttributeViewModel> BodyAttribute { get; set; }
        public virtual ICollection<ModelDetailCollectionImageViewModel> CollectionImage { get; set; }
    }

    public class ModelDetailBodyAttributeViewModel
    {
        public decimal? Value { get; set; }
        public int? BodyAttTypeId { get; set; }
        public string BodyAttName { get; set; }
        public string Measure { get; set; }
    }

    public partial class ModelDetailProductViewModel
    {
        public string Link { get; set; }
    }

    public partial class ModelDetailModelStyleViewModel
    {
        public int StyleId { get; set; }
        public string StyleName { get; set; }
    }

    public partial class ModelDetailCollectionImageViewModel
    {
        public ModelDetailCollectionImageViewModel()
        {
            Image = new HashSet<ModelDetailImageViewModel>();
        }

        public string Name { get; set; }
        public virtual ICollection<ModelDetailImageViewModel> Image { get; set; }
    }

    public partial class ModelDetailImageViewModel
    {
        public string Extension { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }

    }
}


