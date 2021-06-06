using System;
using System.Collections.Generic;

namespace Fero.Data.ViewModels
{
    public class CreateModelAccountViewModel
    {
        public CreateModelAccountViewModel()
        {
            BodyPart = new HashSet<CreateAccountBodyPartViewModel>();
            ModelStyle = new HashSet<CreateAccountModelStyleViewModel>();
            Product = new HashSet<CreateAccountProductViewModel>();
        }
        public string Name { get; set; }
        public byte Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public string SubAddress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gifted { get; set; }
        public virtual ICollection<CreateAccountBodyPartViewModel> BodyPart { get; set; }
        public virtual ICollection<CreateAccountModelStyleViewModel> ModelStyle { get; set; }
        public virtual ICollection<CreateAccountProductViewModel> Product { get; set; }
    }

    public class CreateAccountBodyPartViewModel
    {
        public CreateAccountBodyPartViewModel()
        {
            BodyAttribute = new HashSet<CreateAccountBodyAttributeViewModel>();
            CollectionImage = new HashSet<CreateAccountCollectionImageViewModel>();
        }
        public int? BodyPartTypeId { get; set; }
        public virtual ICollection<CreateAccountBodyAttributeViewModel> BodyAttribute { get; set; }
        public virtual ICollection<CreateAccountCollectionImageViewModel> CollectionImage { get; set; }
    }

    public class CreateAccountBodyAttributeViewModel
    {
        public decimal? Value { get; set; }
        public int? BodyAttTypeId { get; set; }
    }

    public partial class CreateAccountProductViewModel
    {
        public string Link { get; set; }
    }

    public partial class CreateAccountModelStyleViewModel
    {
        public int StyleId { get; set; }
    }

    public partial class CreateAccountCollectionImageViewModel
    {
        public CreateAccountCollectionImageViewModel()
        {
            Image = new HashSet<CreateAccountImageViewModel>();
        }

        public string Name { get; set; }

        public virtual ICollection<CreateAccountImageViewModel> Image { get; set; }
    }

    public partial class CreateAccountImageViewModel
    {
        public string Extension { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }

    }
}
