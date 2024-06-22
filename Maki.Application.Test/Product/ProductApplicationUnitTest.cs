using AutoMapper;
using Maki.Application.Product.CommandServices;
using Maki.Domain.Product.Models.Aggregates;
using Maki.Domain.Product.Models.Commands;
using Maki.Domain.Product.Repositories;
using Moq;
using NSubstitute;

namespace Maki.Application.Test.Product;

public class ProductApplicationUnitTest
{
    [Fact]
    public async Task SaveAsync_ValidInput_ReturnValidId()
    {
        //Arrange
        var mock = new Mock<IProductRepository>();
        var mockMapper = new Mock<IMapper>();

        CreateProductCommand command = new CreateProductCommand
        {
            Name = "Product 1",
            Description = "Description",
            Price = 100,
            Image = "string",
            CategoryId = 1,
            ArtisanId = 1,
            Width = "string",
            Height = "string",
            Depth = "string",
            Material = "string"
        };

        mockMapper.Setup(m => m.Map<CreateProductCommand, ProductA>(command)).Returns(new ProductA
        {
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            Image = command.Image,
            CategoryId = command.CategoryId,
            ArtisanId = command.ArtisanId,
            Width = command.Width,
            Height = command.Height,
            Depth = command.Depth,
            Material = command.Material
        });

        mock.Setup(data => data.GetByNameAsync(command.Name)).ReturnsAsync((ProductA)null);
        mock.Setup(data => data.GetAllAsync()).ReturnsAsync(new List<ProductA>());
        mock.Setup(data => data.SaveAsync(It.IsAny<ProductA>())).ReturnsAsync(1);

        ProductCommandService productCommandService = new ProductCommandService(mock.Object, mockMapper.Object);


        //ACt
        var result = await productCommandService.Handle(command);

        //Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task DeletAsync_ExistingId_ReturnsTrue()
    {
        //Arrange
        var id = 10;
        var productDataMock = Substitute.For<IProductRepository>();
        var mockMapper = Substitute.For<IMapper>();

        productDataMock.GetByIdAsync(id).Returns(new ProductA());
        productDataMock.DeleteAsync(id).Returns(true);

        DeleteProductCommand command = new DeleteProductCommand
        {
            Id = id
        };

        ProductCommandService productCommandService = new ProductCommandService(productDataMock, mockMapper);

        //Act
        var result = await productCommandService.Handle(command);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeletAsync_NotExistingId_ReturnsFalse()
    {
        //Arrange
        var id = 10;
        var productDataMock = Substitute.For<IProductRepository>();
        var mockMapper = Substitute.For<IMapper>();

        productDataMock.GetByIdAsync(id).Returns((ProductA)null);
        productDataMock.DeleteAsync(id).Returns(true);

        DeleteProductCommand command = new DeleteProductCommand
        {
            Id = id
        };

        ProductCommandService productCommandService = new ProductCommandService(productDataMock, mockMapper);


        //Act and Assert
        Assert.ThrowsAsync<Exception>(() => productCommandService.Handle(command));
    }
}