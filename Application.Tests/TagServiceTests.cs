using CrpgP.Application;
using CrpgP.Application.Options;
using CrpgP.Domain;
using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using CrpgP.Domain.Errors;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NSubstitute.ReturnsExtensions;
using Serilog;

namespace Application.UnitTests;

public class TagServiceTests
{
    private ITagRepository _mockRepository = null!;
    private IOptions<MemoryCacheOptions> _mockIOption = null!;

    [SetUp]
    public void Setup()
    {
        _mockRepository = Substitute.For<ITagRepository>();
        _mockIOption = Substitute.For<IOptions<MemoryCacheOptions>>();
        
        // Cache disabled by default
        _mockIOption.Value.Returns(new MemoryCacheOptions { Enabled = false, EntryExpiryInSeconds = 1 });
    }
    
    [Test]
    public async Task GetTagByIdAsync_ExistingId_ResultSuccessAndPortrait()
    {
        // Arrange
        var tag = new Tag {Name = "Test Tag"};
        _mockRepository.FindByIdAsync(1).ReturnsForAnyArgs(tag);
        var tagService = new TagService(
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
            Substitute.For<ILogger>());

        // Act
        var sut = await tagService.GetTagByIdAsync(1);

        // Assert
        sut.Should().Match<Result>(result => 
            result.IsSuccess == true
            && result.Value!.Equals(tag));
    }
    
    [Test]
    public async Task GetTagByNameAsync_NameExists_ResultSuccessAndGame()
    {
        // Arrange
        var tag = new Tag {Name = "Test Tag"};
        _mockRepository.FindByNameAsync("Test Tag").ReturnsForAnyArgs(tag);
        _mockIOption.Value.Returns(new MemoryCacheOptions { Enabled = false, EntryExpiryInSeconds = 1 });
        var tagService = new TagService(
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await tagService.GetTagByNameAsync("Test Tag");

        // Assert
        sut.Should().Match<Result>(result => result.IsSuccess && result.Value!.Equals(tag));
    }
    
    [Test]
    public async Task GetTagByNameAsync_NameDoesNotExists_ResultNotSuccessAndError()
    {
        // Arrange
        _mockRepository.FindByNameAsync("Test Tag").ReturnsNullForAnyArgs();
        _mockIOption.Value.Returns(new MemoryCacheOptions { Enabled = false, EntryExpiryInSeconds = 1 });
        var tagService = new TagService(
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await tagService.GetTagByNameAsync("Test Tag");

        // Assert
        sut.Should().Match<Result>(result => !result.IsSuccess && result.Error == TagErrors.NotFoundByName("Test Tag"));
    }
    
    [Test]
    public async Task CreateTagAsync_CreateSuccess_ResultSuccessAndId()
    {
        // Arrange
        var tag = new Tag {Name = "Test Tag"};
        _mockRepository.InsertAsync(tag).ReturnsForAnyArgs(1);
        
        var tagService = new TagService(
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await tagService.CreateTagAsync(tag);

        // Assert
        sut.Should().Match<Result>(result => result.IsSuccess && result.Value.Equals(1));
    }
    
    [Test]
    public async Task DeleteTagAsync_DeleteSuccess_ResultSuccess()
    {
        // Arrange
        var tagService = new TagService(
            _mockIOption,
            _mockRepository,
            Substitute.For<IMemoryCache>(),
            Substitute.For<ILogger>());
        
        // Act
        var sut = await tagService.DeleteTagAsync(1);

        // Assert
        sut.Should().Match<Result>(result => result.IsSuccess);
    }
}