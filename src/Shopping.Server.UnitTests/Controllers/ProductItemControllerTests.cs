using Microsoft.AspNetCore.Mvc;
using Moq;
using Shopping.Server.Controllers;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Results;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Shopping.Server.UnitTests.Controllers
{
    public class ProductItemControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsOkObjectResult_With_ProductItemResult()
        {
            var testList = new List<ProductItem>()
            {
                new ProductItem()
                {
                    Id="abc",
                    Name = "Test"
                }
            };

            var repoMock = new Mock<IProducts>();
            repoMock.Setup(p => p.GetAllAsync())
                .Returns(Task.FromResult(testList));

            var productController = new ProductController(repoMock.Object, null);

            var response = await productController.GetProducts();

            Assert.IsType<OkObjectResult>(response.Result);

            var result = response.Result as OkObjectResult;

            Assert.IsType<ProductItemResult>(result.Value);

            var resultData = (result.Value as ProductItemResult).ResultData;

            Assert.Single(resultData);
        }

        [Fact]
        public async Task Get_ItemWithIdDoesNotExist_ReturnsNotFoundResult()
        {
            var repoMock = new Mock<IProducts>();
            repoMock.Setup(p => p.GetAsync(It.IsAny<string>()))
                .Throws(new ItemNotFoundException());

            var controller = new ProductController(repoMock.Object, null);

            var result = await controller.GetProduct("abc");

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
        [Fact]
        public async Task Get_ItemWithIdDoesExist_ReturnsOkResult_WithCorrectItem()
        {
            var item = new ProductItem()
            {
                Id = "123-abc",
                Name = "Test"
            };

            var repoMock = new Mock<IProducts>();
            repoMock.Setup(p => p.GetAsync(item.Id))
                .Returns(Task.FromResult(item));

            var controller = new ProductController(repoMock.Object, null);
            var response = await controller.GetProduct(item.Id);

            Assert.IsType<OkObjectResult>(response.Result);

            var objectResult = response.Result as OkObjectResult;

            Assert.IsType<ProductItemResult>(objectResult.Value);

            Assert.Equal(item.Name, (objectResult.Value as ProductItemResult).ResultData.FirstOrDefault().Name);
        }

        [Fact]
        public async Task Post_ItemGetsCreated_ReturnsOkObjectResult()
        {
            var item = new ProductItem()
            {
                Id = "123-abc",
                Name = "TestCategory"
            };

            var repoMock = new Mock<IProducts>();
            repoMock.Setup(p => p.CreateAsync(item))
                .Returns(Task.FromResult(item));

            var controller = new ProductController(repoMock.Object, null);
            var response = await controller.PostProduct(item);

            Assert.IsType<CreatedResult>(response.Result);
        }
        [Fact]
        public async Task Post_ItemCreationThrowsAlreadyExistException_ReturnsConflictResult()
        {
            var repoMock = new Mock<IProducts>();
            repoMock.Setup(p => p.CreateAsync(It.IsAny<ProductItem>()))
                .Throws(new ItemAlreadyExistsException());

            var controller = new ProductController(repoMock.Object, null);
            var response = await controller.PostProduct(new ProductItem());

            Assert.IsType<ConflictObjectResult>(response.Result);
        }
        [Fact]
        public async Task Put_RequestIdNotEqualtoDataId_ReturnsBadRequestResult()
        {
            var controller = new ProductController(null, null);
            var response = await controller.UpdateProduct("abc", new ProductItem()
            {
                Id = "123"
            });

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }
        [Fact]
        public async Task Put_ItemNotFound_ReturnsNotFoundObjectResult()
        {
            var item = new ProductItem()
            {
                Id = "123-abc",
                Name = "TestCategory"
            };

            var repoMock = new Mock<IProducts>();
            repoMock.Setup(p => p.UpdateAsync(item.Id, item))
                .Throws(new ItemNotFoundException());

            var controller = new ProductController(repoMock.Object, null);
            var response = await controller.UpdateProduct(item.Id, item);

            Assert.IsType<NotFoundObjectResult>(response.Result);
        }
        [Fact]
        public async Task Put_ItemAlreadyExists_ReturnsNotConflictResult()
        {
            var item = new ProductItem()
            {
                Id = "123-abc",
                Name = "TestCategory"
            };

            var repoMock = new Mock<IProducts>();
            repoMock.Setup(p => p.UpdateAsync(item.Id, item))
                .Throws(new ItemAlreadyExistsException());

            var controller = new ProductController(repoMock.Object, null);
            var response = await controller.UpdateProduct(item.Id, item);

            Assert.IsType<ConflictObjectResult>(response.Result);
        }

        [Fact]
        public async Task Put_ItemGetsUpdated_ReturnsOkObjectResult()
        {
            var item = new ProductItem()
            {
                Id = "123-abc",
                Name = "TestCategory"
            };

            var repoMock = new Mock<IProducts>();
            repoMock.Setup(p => p.UpdateAsync(item.Id, item))
                .Returns(Task.FromResult(item));

            var controller = new ProductController(repoMock.Object, null);
            var response = await controller.UpdateProduct(item.Id, item);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public async Task Delete_ItemDoesNotExist_ReturnsNotFoundResult()
        {
            var repoMock = new Mock<IProducts>();
            repoMock.Setup(p => p.DeleteByIdAsync(It.IsAny<string>()))
                .Throws(new ItemNotFoundException());
            
            var controller = new ProductController(repoMock.Object, null);
            var response = await controller.DeleteProduct(It.IsAny<string>());

            Assert.IsType<NotFoundObjectResult>(response.Result);
        }
    }
}
