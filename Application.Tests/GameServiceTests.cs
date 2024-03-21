using CrpgP.Application;
using CrpgP.Application.Cache;
using CrpgP.Domain;
using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using CrpgP.Domain.Errors;
using NSubstitute.ReturnsExtensions;
using Serilog;

namespace Application.UnitTests;

public class GameServiceTests
{
    private IGameRepository _mockRepository = null!;

    [SetUp]
    public void Setup()
    {
        _mockRepository = Substitute.For<IGameRepository>();
    }
    
    
    [Test]
    public async Task GetGameByIdAsync_ExistingId_ResultSuccessAndGame()
    {
        // Arrange
        var game = new Game { Name = "Test Game", PortraitSize = new Size() };
        _mockRepository.FindByIdAsync(1).ReturnsForAnyArgs(game);
        var gameService = new GameService(
            _mockRepository,
            Substitute.For<ICacheService>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.GetGameByIdAsync(1);

        // Assert
        sut.Should().Match<Result>(result => 
            result.IsSuccess == true
            && result.Value!.Equals(game));
    }
    
    [Test]
    public async Task GetGameByIdAsync_ExistingId_ResultNotSuccessAndError()
    {
        // Arrange
        _mockRepository.FindByIdAsync(1).ReturnsNullForAnyArgs();
        var gameService = new GameService(
            _mockRepository,
            Substitute.For<ICacheService>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.GetGameByIdAsync(1);

        // Assert
        sut.Should().Match<Result>(result => !result.IsSuccess && result.Error == GameErrors.NotFound(1));
    }

    [Test]
    public async Task GetGameByNameAsync_NameExists_ResultSuccessAndGame()
    {
        // Arrange
        var game = new Game { Name = "Test Game", PortraitSize = new Size() };
        _mockRepository.FindByNameAsync("Test Game").ReturnsForAnyArgs(game);
        var gameService = new GameService(
            _mockRepository,
            Substitute.For<ICacheService>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.GetGameByNameAsync("Test Game");

        // Assert
        sut.Should().Match<Result>(result => 
            result.IsSuccess == true
            && result.Value!.Equals(game));
    }
    
    [Test]
    public async Task GetGameByNameAsync_NameDoesNotExists_ResultNotSuccessAndError()
    {
        // Arrange
        _mockRepository.FindByNameAsync("Test Game").ReturnsNullForAnyArgs();
        var gameService = new GameService(
            _mockRepository,
            Substitute.For<ICacheService>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.GetGameByNameAsync("Test Game");

        // Assert
        sut.Should().Match<Result>(result => !result.IsSuccess && result.Error == GameErrors.NotFoundByName("Test Game"));
    }

    [Test]
    public async Task CreateGameAsync_CreateSuccess_ResultSuccessAndId()
    {
        // Arrange
        var game = new Game { Name = "Test Game", PortraitSize = new Size() };
        _mockRepository.InsertAsync(game).ReturnsForAnyArgs(1);
        
        var gameService = new GameService(
            _mockRepository,
            Substitute.For<ICacheService>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.CreateGameAsync(game);

        // Assert
        sut.Should().Match<Result>(result => 

            result.IsSuccess == true
            && result.Value!.Equals(1));
    }
    
    [Test]
    public async Task UpdateGameAsync_UpdateSuccess_ResultSuccess()
    {
        // Arrange
        var game = new Game { Name = "Test Game", PortraitSize = new Size() };
        var gameService = new GameService(
            _mockRepository,
            Substitute.For<ICacheService>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.UpdateGameAsync(game);

        // Assert
        sut.Should().Match<Result>(result => result.IsSuccess == true);
    }
    
    [Test]
    public async Task DeleteGameAsync_DeleteSuccess_ResultSuccess()
    {
        // Arrange
        var gameService = new GameService(
            _mockRepository,
            Substitute.For<ICacheService>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.DeleteGameAsync(1);

        // Assert
        sut.Should().Match<Result>(result => result.IsSuccess == true);
    }
    
}