using DeliveryService.Domain.Commons;
using DeliveryService.Domain.Entities.Categories;
using DeliveryService.Domain.Entities.Compositions;
using DeliveryService.Domain.Entities.TimeCategories;
using DeliveryService.Domain.Enums;
using DeliveryService.Domain.ILocalizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeliveryService.Domain.Entities.Products
{
    public class Product : IAuditable, ILocalizationName, ILocalizationDescription
    {
        public Guid Id { get; set; }

        [NotMapped]
        public string Name { get; set; }
        public string NameUz { get; set; }
        public  string NameRu { get; set; }
        public string NameEn { get; set; }

        [NotMapped]
        public string Description { get; set; }       
        public string DescriptionUz { get; set; }
        public string DescriptionRu { get; set; }
        public string DescriptionEn { get; set; }

        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }
        [JsonIgnore]
        public virtual  Category Category { get; set; }
        [ForeignKey(nameof(TimeCategory))]
        public Guid TimeCategoryId { get; set; }
        [JsonIgnore ]
        public virtual TimeCategory TimeCategory { get; set; }

        public virtual ICollection<Composition> Compositions { get; set; }
        public Product()
        {
            Compositions = new List<Composition>();
        }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }
        public ItemState State { get; set; }


        public void Create()
        {
            CreateDate = DateTime.Now;
            State = ItemState.Created;
        }
        public void Update()
        {
            UpdateDate = DateTime.Now;
            State = ItemState.Updated;
        }
        public void Delete()
        {
            State = ItemState.Deleted;
        }
    }
}
