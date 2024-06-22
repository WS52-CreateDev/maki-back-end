using AutoMapper;
using Maki.Domain.Product.Models.Queries;
using Maki.Domain.Product.Models.Response;
using Maki.Domain.Product.Services;
using Maki.Presentation.Product.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Maki.Presentation.Test;

public class ProductControllerUnitTest
{
    [Fact]
    public async Task GetAsync_ResultOk()
    {
        //Arrange
        var mockMapper = new Mock<IMapper>();
        var mockProductQueryService = new Mock<IProductQueryService>();
        var mockProductCommandService = new Mock<IProductCommandService>();

        var fakeList = new List<ProductResponse>()
        {
            new ProductResponse()
        };
        mockProductQueryService.Setup(p=>p.Handle(new GetAllProductsQuery())).ReturnsAsync(fakeList);
        
        var controller = new ProductController(mockProductCommandService.Object, mockProductQueryService.Object, mockMapper.Object);
        
        //Act
        var result = controller.GetAsync();

        //Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetAsync_ResultNotFound()
    {
        //Arrange
        var mockMapper = new Mock<IMapper>();
        var mockProductQueryService = new Mock<IProductQueryService>();
        var mockProductCommandService = new Mock<IProductCommandService>();

        var fakeList = new List<ProductResponse>();
        mockProductQueryService.Setup(p=>p.Handle(new GetAllProductsQuery())).ReturnsAsync(fakeList);
        
        var controller = new ProductController(mockProductCommandService.Object, mockProductQueryService.Object, mockMapper.Object);
        
        //Act
        var result = controller.GetAsync();

        //Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}