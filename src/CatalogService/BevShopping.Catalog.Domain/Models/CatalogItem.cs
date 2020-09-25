using BevShopping.Catalog.Domain.Enum;
using BevShopping.Shared.Core.Domain;
using System;

namespace BevShopping.Catalog.Domain.Models
{
    public class CatalogItem : Entity<Guid>, IAggregateRoot
    {

        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }
        public string PictureUri { get; private set; }
        public CatalogType CatalogType { get; private set; }
        public DateTime CreatedUtc { get; private set; }

        private CatalogItem(string name, string description, double price, string pictureUri, CatalogType catalogType)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            PictureUri = pictureUri;
            CatalogType = catalogType;
            CreatedUtc = DateTime.UtcNow;
        }

        public class Factory
        {
            public static CatalogItem CreateItem(string name, string description, double price, string pictureUri, CatalogType catalogType)
            {
                return new CatalogItem(name, description, price, pictureUri, catalogType);
            }
        }

        public void UpdateDetails(string name, string description, double price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public void UpdatePicture(string pictureUri)
        {
            if (string.IsNullOrWhiteSpace(pictureUri))
            {
                PictureUri = string.Empty;
                return;
            }

            PictureUri = pictureUri;
        }

    }
}