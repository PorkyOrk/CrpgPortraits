using System.Text.Json;
using CrpgP.Application;
using CrpgP.Application.Options;
using CrpgP.Application.Result;
using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using Serilog;

namespace Application.UnitTests;

public class GameServiceTests
{
    private IGameRepository _mockRepository = null!;
    private IOptions<MemoryCacheOptions> _mockIOption = null!;

    [SetUp]
    public void Setup()
    {
        _mockRepository = Substitute.For<IGameRepository>();
        _mockIOption = Substitute.For<IOptions<MemoryCacheOptions>>();
        
        // Cache disabled by default
        _mockIOption.Value.Returns(new MemoryCacheOptions { Enabled = false, EntryExpiryInSeconds = 1 });
    }
    
    
    [Test]
    public async Task GetGameByIdAsync_ExistingId_ResultSuccessAndGame()
    {
        // Arrange
        var game = new Game { Name = "Test Game", PortraitSize = new Size() };
        _mockRepository.FindByIdAsync(1).Returns(game);
        _mockIOption.Value.Returns(new MemoryCacheOptions { Enabled = false, EntryExpiryInSeconds = 1 });
        
        var gameService = new GameService(
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.GetGameByIdAsync(1);

        // Assert
        sut.Should().Match<Result<Game>>(result => 
            result.IsSuccess == true
            && result.Value!.Equals(game));
    }
    
    [Test]
    public async Task GetGameByIdAsync_ExistingId_ResultNotSuccessAndMessage()
    {
        // Arrange
        _mockRepository.FindByIdAsync(1).ReturnsNullForAnyArgs();
        _mockIOption.Value.Returns(new MemoryCacheOptions { Enabled = false, EntryExpiryInSeconds = 1 });
        
        var gameService = new GameService(
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.GetGameByIdAsync(1);

        // Assert
        sut.Should().Match<Result<Game>>(result =>
            result.IsSuccess == false
            && result.Messages!.Length > 0);
    }

    [Test]
    public async Task GetGameByNameAsync_NameExists_ResultSuccessAndGame()
    {
        // Arrange
        var game = new Game { Name = "Test Game", PortraitSize = new Size() };
        _mockRepository.FindByNameAsync("Test Game").Returns(game);
        _mockIOption.Value.Returns(new MemoryCacheOptions { Enabled = false, EntryExpiryInSeconds = 1 });
        
        var gameService = new GameService(
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.GetGameByNameAsync("Test Game");

        // Assert
        sut.Should().Match<Result<Game>>(result => 
            result.IsSuccess == true
            && result.Value!.Equals(game));
    }
    
    [Test]
    public async Task GetGameByNameAsync_NameExists_ResultNotSuccessAndMessage()
    {
        // Arrange
        _mockRepository.FindByNameAsync("Test Game").ReturnsNullForAnyArgs();
        _mockIOption.Value.Returns(new MemoryCacheOptions { Enabled = false, EntryExpiryInSeconds = 1 });
        
        var gameService = new GameService(
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.GetGameByNameAsync("Test Game");

        // Assert
        sut.Should().Match<Result<Game>>(result => 
            result.IsSuccess == false
            && result.Messages!.Length > 0);
    }

    [Test]
    public async Task CreateGameAsync_CreateSuccess_ResultSuccessAndGame()
    {
        // Arrange
        var game = new Game { Name = "Test Game", PortraitSize = new Size() };
        _mockRepository.InsertAsync(game).ReturnsForAnyArgs(1);
        
        var gameService = new GameService(
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.CreateGameAsync(JsonSerializer.Serialize(game));

        // Assert
        sut.Should().Match<Result<int>>(result => 
            result.IsSuccess == true
            && result.Value!.Equals(1));
    }
    
    
    // TODO: Mock exception from repository
    // [Test]
    // public async Task CreateGameAsync_NotCreated_ResultSNotSuccessAndMessage()
    // {
    //     // Arrange
    //     var game = new Game { Name = "Test Game", PortraitSize = new Size() };
    //     // _mockRepository.InsertAsync(game).Returns(x => throw new Exception("Test creation failed exception"));
    //     _mockRepository.When(x => x.InsertAsync(game))
    //         .Do(x => throw new Exception("Test creation failed exception"));
    //     
    //     var gameService = new GameService(
    //         _mockIOption,
    //         _mockRepository,
    //         Substitute.For<IMemoryCache>(),
    //         Substitute.For<ILogger>());
    //     
    //     // Act
    //     var sut = await gameService.CreateGameAsync(JsonSerializer.Serialize(game));
    //
    //     // Assert
    //     sut.Should().Match<Result<int>>(result => 
    //         result.IsSuccess == false
    //         && result.Messages!.Length > 0);
    // }
    
    
    // UpdateGameAsync
    [Test]
    public async Task UpdateGameAsync_UpdateSuccess_ResultSuccess()
    {
        // Arrange
        var game = new Game { Name = "Test Game", PortraitSize = new Size() };
        var gameService = new GameService(
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.UpdateGameAsync(JsonSerializer.Serialize(game));

        // Assert
        sut.Should().Match<Result<object>>(result => result.IsSuccess == true);
    }
    
    // DeleteGameAsync
    [Test]
    public async Task DeleteGameAsync_DeleteSuccess_ResultSuccess()
    {
        // Arrange
        var gameService = new GameService(
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await gameService.DeleteGameAsync(1);

        // Assert
        sut.Should().Match<Result<object>>(result => result.IsSuccess == true);
    }
    
}