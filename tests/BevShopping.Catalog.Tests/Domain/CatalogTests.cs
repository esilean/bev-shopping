using BevShopping.Catalog.Domain.Enum;
using BevShopping.Catalog.Domain.Models;
using Xunit;

namespace BevShopping.Catalog.Tests.Domain
{
    public class CatalogTests
    {
        [Fact(DisplayName = "Should create a catalog item")]
        public void CatalogItem_ShouldCreateACatalogItem()
        {
            //Arrange
            var name = "shorts";
            var description = "shorts description";
            var price = 10;
            var pictureUri = "/image/pic";
            var catalogType = CatalogType.Man;

            //Act
            var catalogItem = CatalogItem.Factory.CreateItem(name, description, price, pictureUri, catalogType);

            //Assert
            Assert.NotNull(catalogItem);
            Assert.Equal(name, catalogItem.Name);
            Assert.Equal(description, catalogItem.Description);
            Assert.Equal(price, catalogItem.Price);
            Assert.Equal(pictureUri, catalogItem.PictureUri);
            Assert.Equal(catalogType, catalogItem.CatalogType);
        }


        [Fact(DisplayName = "Should update details of the item")]
        public void CatalogItem_ShouldUpdateItemDetails()
        {
            //Arrange
            var name = "shorts";
            var description = "shorts description";
            var price = 10;
            var pictureUri = "/image/pic";
            var catalogType = CatalogType.Man;

            var catalogItem = CatalogItem.Factory.CreateItem(name, description, price, pictureUri, catalogType);

            var newName = "new shorts";
            var newDescription = "new description";
            var newPrice = 99.99;
            //Act
            catalogItem.UpdateDetails(newName, newDescription, newPrice);

            //Assert
            Assert.NotNull(catalogItem);
            Assert.Equal(newName, catalogItem.Name);
            Assert.Equal(newDescription, catalogItem.Description);
            Assert.Equal(newPrice, catalogItem.Price);
        }

        [Fact(DisplayName = "Should update the picture of the item")]
        public void CatalogItem_ShouldUpdateItemPicture()
        {
            //Arrange
            var name = "shorts";
            var description = "shorts description";
            var price = 10;
            var pictureUri = "/image/pic";
            var catalogType = CatalogType.Man;

            var catalogItem = CatalogItem.Factory.CreateItem(name, description, price, pictureUri, catalogType);

            var newPicture = "/image/newpicture";
            //Act
            catalogItem.UpdatePicture(newPicture);

            //Assert
            Assert.NotNull(catalogItem);
            Assert.Equal(newPicture, catalogItem.PictureUri);
        }

        [Fact(DisplayName = "Should update the picture of the empty")]
        public void CatalogItem_ShouldUpdateItemPictureToEmpty()
        {
            //Arrange
            var name = "shorts";
            var description = "shorts description";
            var price = 10;
            var pictureUri = "/image/pic";
            var catalogType = CatalogType.Man;

            var catalogItem = CatalogItem.Factory.CreateItem(name, description, price, pictureUri, catalogType);
            var newPicture = "";

            //Act
            catalogItem.UpdatePicture(newPicture);

            //Assert
            Assert.NotNull(catalogItem);
            Assert.Equal(newPicture, catalogItem.PictureUri);
        }
    }
}