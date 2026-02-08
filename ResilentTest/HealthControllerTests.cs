using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ResilentAPI.Controllers;
using ResilentAPI.Dtos;
using Xunit;

namespace ResilentAPI.Tests.Controllers;

/// <summary>
/// Contains unit tests for the HealthController class.
/// </summary>
public class HealthControllerTests
{
    private readonly Mock<ILogger<HealthController>> _mockLogger;
    private readonly HealthController _sut;

    /// <summary>
    /// Initializes a new instance of the HealthControllerTests class.
    /// Sets up the mock logger and creates the system under test (SUT).
    /// </summary>
    public HealthControllerTests()
    {
        _mockLogger = new Mock<ILogger<HealthController>>();
        _sut = new HealthController(_mockLogger.Object);
    }

    /// <summary>
    /// Verifies that the Log method returns an OkObjectResult when called with a valid LogRequest.
    /// </summary>
    [Fact]
    public void Log_WithValidLogRequest_ReturnsOkObjectResult()
    {
        // Arrange
        var logRequest = new LogRequest
        {
            Message = "Test log message"
        };

        // Act
        var result = _sut.Log(logRequest);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    /// <summary>
    /// Verifies that the Log method returns a success response with the expected structure
    /// when called with a valid LogRequest.
    /// </summary>
    [Fact]
    public void Log_WithValidLogRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var logRequest = new LogRequest
        {
            Message = "Test log message"
        };

        // Act
        var result = _sut.Log(logRequest) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        var response = result.Value;
        var successProperty = response?.GetType().GetProperty("success");
        Assert.NotNull(successProperty);
        Assert.True((bool)successProperty.GetValue(response)!);
    }

    /// <summary>
    /// Verifies that the Log method logs the message at Information level
    /// when called with a valid LogRequest.
    /// </summary>
    [Fact]
    public void Log_WithValidLogRequest_LogsMessageAtInformationLevel()
    {
        // Arrange
        var expectedMessage = "Test log message";
        var logRequest = new LogRequest
        {
            Message = expectedMessage
        };

        // Act
        _sut.Log(logRequest);

        // Assert
        _mockLogger.Verify(
            logger => logger.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()! == expectedMessage),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    /// <summary>
    /// Verifies that the Log method logs the exact message provided in the LogRequest
    /// when called with a specific message string.
    /// </summary>
    [Theory]
    [InlineData("Application started successfully")]
    [InlineData("User authentication completed")]
    [InlineData("Data processing finished")]
    public void Log_WithDifferentMessages_LogsCorrectMessage(string message)
    {
        // Arrange
        var logRequest = new LogRequest
        {
            Message = message
        };

        // Act
        _sut.Log(logRequest);

        // Assert
        _mockLogger.Verify(
            logger => logger.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()! == message),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }
}

