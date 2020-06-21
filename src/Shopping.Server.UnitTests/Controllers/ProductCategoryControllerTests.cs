using Microsoft.AspNetCore.Mvc;
using Moq;
using Shopping.Server.Controllers;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Results;
using Shopping.Shared.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Shopping.Server.UnitTests.Controllers
{
    public class ProductCategoryControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsOkObjectResult_With_ProductCategoryResult()
        {
            var testList = new List<ProductCategory>()
            {
                new ProductCategory()
                {
                    Id="abc",
                    Name = "Test"
                }
            };

            var repoMock = new Mock<IProductCategories>();
            repoMock.Setup(p => p.GetAllAsync())
                .Returns(Task.FromResult(testList));

            var categoryController = new ProductCategoryController(repoMock.Object,null);

            var response = await categoryController.GetProductCategories();

            Assert.IsType<OkObjectResult>(response.Result);

            var result = response.Result as OkObjectResult;

            Assert.IsType<ProductCategoryResult>(result.Value);

            var resultData = (result.Value as ProductCategoryResult).ResultData;

            Assert.Single(resultData);
        }

        [Fact]
        public async Task Get_ItemWithIdDoesNotExist_ReturnsNotFoundResult()
        {
            var repoMock = new Mock<IProductCategories>();
            repoMock.Setup(p => p.GetAsync(It.IsAny<string>()))
                .Throws(new ItemNotFoundException());

            var categoryController = new ProductCategoryController(repoMock.Object, null);

            var result = await categoryController.GetProductCategory("abc");

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
        [Fact]
        public async Task Get_ItemWithIdDoesExist_ReturnsOkResult_WithCorrectItem()
        {
            var item = new ProductCategory()
            {
                Id = "123-abc",
                Name = "TestCategory"
            };

            var repoMock = new Mock<IProductCategories>();
            repoMock.Setup(p => p.GetAsync(item.Id))
                .Returns(Task.FromResult(item));

            var categoryController = new ProductCategoryController(repoMock.Object, null);
            var response = await categoryController.GetProductCategory(item.Id);

            Assert.IsType<OkObjectResult>(response.Result);

            var objectResult = response.Result as OkObjectResult;

            Assert.IsType<ProductCategoryResult>(objectResult.Value);

            Assert.Equal(item.Name, (objectResult.Value as ProductCategoryResult).ResultData.FirstOrDefault().Name);
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

            var categoryController = new ProductCategoryController(repoMock.Object, null);
            var response = await categoryController.PostProductCategory(item);

            Assert.IsType<CreatedResult>(response.Result);
        }
        [Fact]
        public async Task Post_ItemCreationThrowsAlreadyExistException_ReturnsConflictResult()
        {
            var repoMock = new Mock<IProductCategories>();
            repoMock.Setup(p => p.CreateAsync(It.IsAny<ProductCategory>()))
                .Throws(new ItemAlreadyExistsException());

            var categoryController = new ProductCategoryController(repoMock.Object, null);
            var response = await categoryController.PostProductCategory(new ProductCategory());

            Assert.IsType<ConflictObjectResult>(response.Result);
        }
        [Fact]
        public async Task Put_RequestIdNotEqualtoDataId_ReturnsBadRequestResult()
        {
            var categoryController = new ProductCategoryController(null, null);
            var response = await categoryController.UpdateProductCategory("abc", new ProductCategory()
            {
                Id = "123"
            });

            Assert.IsType<BadRequestObjectResult>(response.Result);
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
            repoMock.Setup(p => p.UpdateAsync(item.Id, item))
                .Throws(new ItemNotFoundException());

            var categoryController = new ProductCategoryController(repoMock.Object, null);
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

            var categoryController = new ProductCategoryController(repoMock.Object, null);
            var response = await categoryController.UpdateProductCategory(item.Id, item);

            Assert.IsType<ConflictObjectResult>(response.Result);
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

            var categoryController = new ProductCategoryController(repoMock.Object, null);
            var response = await categoryController.UpdateProductCategory(item.Id, item);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public async Task Delete_ItemDoesNotExist_ReturnsNotFoundResult()
        {
            var repoMock = new Mock<IProductCategories>();
            repoMock.Setup(p => p.DeleteByIdAsync(It.IsAny<string>()))
                .Throws(new ItemNotFoundException());

            var categoryController = new ProductCategoryController(repoMock.Object, null);
            var response = await categoryController.DeleteProductCategory(It.IsAny<string>());

            Assert.IsType<NotFoundObjectResult>(response.Result);
        }
    }
}
