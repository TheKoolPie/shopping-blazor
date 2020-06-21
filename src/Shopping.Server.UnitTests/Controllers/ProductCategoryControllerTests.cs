using Microsoft.AspNetCore.Mvc;
using Moq;
using Shopping.Server.Controllers;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Services;
using System.Threading.Tasks;
using Xunit;

namespace Shopping.Server.UnitTests.Controllers
{
    public class ProductCategoryControllerTests
    {
        [Fact]
        public async Task Get_ItemWithIdDoesNotExist_ReturnsNotFoundResult()
        {
            var repoMock = new Mock<IProductCategories>();
            repoMock.Setup(p => p.GetAsync(It.IsAny<string>()))
                .Throws(new ItemNotFoundException());

            var categoryController = new ProductCategoryController(repoMock.Object);

            var result = await categoryController.GetProductCategory("abc");

            Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public async Task Get_ItemWithIdDoesExist_ReturnsOkResult()
        {
            var item = new ProductCategory()
            {
                Id = "123-abc",
                Name = "TestCategory"
            };

            var repoMock = new Mock<IProductCategories>();
            repoMock.Setup(p => p.GetAsync(item.Id))
                .Returns(Task.FromResult(item));

            var categoryController = new ProductCategoryController(repoMock.Object);
            var response = await categoryController.GetProductCategory(item.Id);

            Assert.IsType<OkObjectResult>(response.Result);

            var objectResult = response.Result as OkObjectResult;

            Assert.IsType<ProductCategory>(objectResult.Value);

            Assert.Equal(item.Name, (objectResult.Value as ProductCategory).Name);
        }

        [Fact]
        public async Task Get_ItemWithIdDoesExist_ReturnsProductCategoryObject()
        {
            var item = new ProductCategory()
            {
                Id = "123-abc",
                Name = "TestCategory"
            };

            var repoMock = new Mock<IProductCategories>();
            repoMock.Setup(p => p.GetAsync(item.Id))
                .Returns(Task.FromResult(item));

            var categoryController = new ProductCategoryController(repoMock.Object);
            var response = await categoryController.GetProductCategory(item.Id);
            var objectResult = response.Result as OkObjectResult;

            Assert.IsType<ProductCategory>(objectResult.Value);
        }

        [Fact]
        public async Task Get_ItemWithIdDoesExist_ReturnsCorrectItem()
        {
            var item = new ProductCategory()
            {
                Id = "123-abc",
                Name = "TestCategory"
            };

            var repoMock = new Mock<IProductCategories>();
            repoMock.Setup(p => p.GetAsync(item.Id))
                .Returns(Task.FromResult(item));

            var categoryController = new ProductCategoryController(repoMock.Object);
            var response = await categoryController.GetProductCategory(item.Id);
            var objectResult = response.Result as OkObjectResult;

            Assert.Equal(item.Name, (objectResult.Value as ProductCategory).Name);
        }

        [Fact]
        public async Task Post_ItemGetsCreated_ReturnsOkObjectResult()
        {
            var item = new ProductCategory()
            {
                Id = "123-abc",
                Name = "TestCategory"
            };

            var repoMock = new Mock<IProductCategories>();
            repoMock.Setup(p => p.CreateAsync(item))
                .Returns(Task.FromResult(item));

            var categoryController = new ProductCategoryController(repoMock.Object);
            var response = await categoryController.PostProductCategory(item);

            Assert.IsType<OkObjectResult>(response.Result);
        }
        [Fact]
        public async Task Post_ItemCreationThrowsAlreadyExistException_ReturnsConflictResult()
        {
            var repoMock = new Mock<IProductCategories>();
            repoMock.Setup(p => p.CreateAsync(It.IsAny<ProductCategory>()))
                .Throws(new ItemAlreadyExistsException());

            var categoryController = new ProductCategoryController(repoMock.Object);
            var response = await categoryController.PostProductCategory(new ProductCategory());

            Assert.IsType<ConflictResult>(response.Result);
        }
        [Fact]
        public async Task Put_RequestIdNotEqualtoDataId_ReturnsBadRequestResult()
        {
            var categoryController = new ProductCategoryController(null);
            var response = await categoryController.UpdateProductCategory("abc", new ProductCategory()
            {
                Id = "123"
            });

            Assert.IsType<BadRequestResult>(response.Result);
        }
        [Fact]
        public async Task Put_ItemNotFound_ReturnsNotFoundObjectResult()
        {
            var item = new ProductCategory()
            {
                Id = "123-abc",
                Name = "TestCategory"
            };

            var repoMock = new Mock<IProductCategories>();
            repoMock.Setup(p => p.UpdateAsync(item.Id,item))
                .Throws(new ItemNotFoundException());

            var categoryController = new ProductCategoryController(repoMock.Object);
            var response = await categoryController.UpdateProductCategory(item.Id, item);

            Assert.IsType<NotFoundObjectResult>(response.Result);
        }
        [Fact]
        public async Task Put_ItemAlreadyExists_ReturnsNotConflictResult()
        {
            var item = new ProductCategory()
            {
                Id = "123-abc",
                Name = "TestCategory"
            };

            var repoMock = new Mock<IProductCategories>();
            repoMock.Setup(p => p.UpdateAsync(item.Id, item))
                .Throws(new ItemAlreadyExistsException());

            var categoryController = new ProductCategoryController(repoMock.Object);
            var response = await categoryController.UpdateProductCategory(item.Id, item);

            Assert.IsType<ConflictResult>(response.Result);
        }

        [Fact]
        public async Task Put_ItemGetsUpdated_ReturnsOkObjectResult()
        {
            var item = new ProductCategory()
            {
                Id = "123-abc",
                Name = "TestCategory"
            };

            var repoMock = new Mock<IProductCategories>();
            repoMock.Setup(p => p.UpdateAsync(item.Id, item))
                .Returns(Task.FromResult(item));

            var categoryController = new ProductCategoryController(repoMock.Object);
            var response = await categoryController.UpdateProductCategory(item.Id, item);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public async Task Delete_ItemDoesNotExist_ReturnsNotFoundResult()
        {
            var repoMock = new Mock<IProductCategories>();
            repoMock.Setup(p => p.DeleteByIdAsync(It.IsAny<string>()))
                .Throws(new ItemNotFoundException());

            var categoryController = new ProductCategoryController(repoMock.Object);
            var response = await categoryController.DeleteProductCategory(It.IsAny<string>());

            Assert.IsType<NotFoundResult>(response);
        }
    }
}
