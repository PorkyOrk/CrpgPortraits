using CrpgP.Application;
using CrpgP.Application.Cache;
using CrpgP.Domain;
using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using CrpgP.Domain.Errors;
using NSubstitute.ReturnsExtensions;
using Serilog;

namespace Application.UnitTests;

public class SizeServiceTests
{
    private ISizeRepository _mockRepository = null!;

    [SetUp]
    public void Setup()
    {
        _mockRepository = Substitute.For<ISizeRepository>();
    }

    [Test]
    public async Task GetSizeByIdAsync_IdExists_ResultSuccessAndSize()
    {
        // Arrange
        var size = new Size();
        _mockRepository.FindByIdAsync(1).ReturnsForAnyArgs(size);
        var sizeService = new SizeService(
            _mockRepository,
            Substitute.For<ICacheService>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await sizeService.GetSizeByIdAsync(1);

        // Assert
        sut.Should().Match<Result>(result => 
            result.IsSuccess == true
            && result.Value!.Equals(size));
    }
    
    [Test]
    public async Task GetSizeByDimensionsAsync_SizeExists_ResultSuccessAndSize()
    {
        // Arrange
        var size = new List<Size> { new() };
        _mockRepository.FindByDimensionsAsync(100,200).ReturnsForAnyArgs(size);
        var sizeService = new SizeService(
            _mockRepository,
            Substitute.For<ICacheService>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await sizeService.GetSizeByDimensionsAsync(100,200);

        // Assert
        sut.Should().Match<Result>(result => 
            result.IsSuccess == true
            && result.Value!.Equals(size));
    }
    
    [Test]
    public async Task GetSizeByDimensionsAsync_SizeDoesNotExists_ResultNotSuccessAndError()
    {
        // Arrange
        _mockRepository.FindByDimensionsAsync(100,200).ReturnsNullForAnyArgs();
        var sizeService = new SizeService(
            _mockRepository,
            Substitute.For<ICacheService>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await sizeService.GetSizeByDimensionsAsync(100,200);

        // Assert
        sut.Should().Match<Result>(result => !result.IsSuccess && result.Error == SizeErrors.NotFoundByDimensions(100,200));
    }
    
    [Test]
    public async Task CreateSizeAsync_CreateSuccess_ResultSuccessAndId()
    {
        // Arrange
        var size = new Size();
        _mockRepository.InsertAsync(size).ReturnsForAnyArgs(1);
        var sizeService = new SizeService(
            _mockRepository,
            Substitute.For<ICacheService>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await sizeService.CreateSizeAsync(size);

        // Assert
        sut.Should().Match<Result>(result => 
            result.IsSuccess == true
            && result.Value.Equals(1));
    }

    [Test]
    public async Task UpdateSizeAsync_UpdateSuccess_ResultSuccess()
    {
        // Arrange
        var size = new Size();
        var sizeService = new SizeService(
            _mockRepository,
            Substitute.For<ICacheService>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await sizeService.UpdateSizeAsync(size);

        // Assert
        sut.Should().Match<Result>(result => result.IsSuccess == true);
    }
    
    [Test]
    public async Task DeleteSizeAsync_DeleteSuccess_ResultSuccess()
    {
        // Arrange
        var sizeService = new SizeService(
            _mockRepository,
            Substitute.For<ICacheService>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await sizeService.DeleteSizeAsync(1);

        // Assert
        sut.Should().Match<Result>(result => result.IsSuccess == true);
    }
    
}