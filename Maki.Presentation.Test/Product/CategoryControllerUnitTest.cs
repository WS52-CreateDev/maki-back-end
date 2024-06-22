using AutoMapper;
using Maki.Domain.Product.Models.Queries;
using Maki.Domain.Product.Models.Response;
using Maki.Domain.Product.Services;
using Maki.Presentation.Product.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Maki.Presentation.Test;

public class CategoryControllerUnitTest
{
    [Fact]
    public async Task GetAsync_ResultOk()
    {
        //Arrange
        var mockMapper = new Mock<IMapper>();
        var mockCategoryQueryService = new Mock<ICategoryQueryService>();
        var mockCategoryCommandService = new Mock<ICategoryCommandService>();

        var fakeList = new List<CategoryResponse>()
        {
            new CategoryResponse()
        };

        mockCategoryQueryService.Setup(p=>p.Handle(new GetAllCategoriesQuery())).ReturnsAsync(fakeList);

        var controller = new CategoryController(mockCategoryCommandService.Object, mockCategoryQueryService.Object, mockMapper.Object);
        
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
        var mockCategoryQueryService = new Mock<ICategoryQueryService>();
        var mockCategoryCommandService = new Mock<ICategoryCommandService>();
  
        var fakeList = new List<CategoryResponse>();
        mockCategoryQueryService.Setup(p=>p.Handle(new GetAllCategoriesQuery())).ReturnsAsync(fakeList);
        
        var controller = new CategoryController(mockCategoryCommandService.Object, mockCategoryQueryService.Object, mockMapper.Object);

        //Act
        var result = controller.GetAsync();

        //Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}