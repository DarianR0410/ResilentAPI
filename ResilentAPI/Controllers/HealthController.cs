using Microsoft.AspNetCore.Mvc;
using ResilentAPI.Dtos;

namespace ResilentAPI.Controllers;

/// A controller class responsible for handling health-related endpoints and logging operations.
public class HealthController : ControllerBase
{
	/// <summary>
	/// Represents the logger responsible for capturing and managing log messages within the HealthController.
	/// </summary>
	/// <remarks>
	/// This logger is used to log informational, warning, error, or debug messages specific to the HealthController's operations,
	/// such as health checks, logging requests, or error handling scenarios.
	/// </remarks>
	private readonly ILogger<HealthController> _logger;

	/// Provides health-related endpoints for the application, including status checks, logging, and error simulation.
	/// This controller includes methods to retrieve the application's health status, log informational messages,
	/// and simulate an error response for testing purposes.
	public HealthController(ILogger<HealthController> logger)
	{
		_logger = logger;
	}

	/// Retrieves the health status of the application.
	/// Responds with information on the application's current health status, version, and environment.
	/// <return>
	/// An IActionResult containing the health status, version, and environment of the application in the response body.
	/// </return>
	[HttpGet]
	[Route("/health")]
	public IActionResult Health()
	{
		return Ok(new
		{
			status = "healthy",
			version = "1.0.0",
			environment = "Production"
		});
	}

	/// Logs an informational message received in the request body.
	/// Writes the message contained within the LogRequest object to the application's logging system.
	/// <param name="logRequest">
	/// The request object containing the log message and level of logging.
	/// </param>
	/// <return>
	/// An IActionResult confirming the logging operation was successful with a success response.
	/// </return>
	[HttpPost]
	[Route("/health/log")]
	public IActionResult Log([FromBody] LogRequest logRequest)
	{
		_logger.LogInformation(logRequest.Message);
		return Ok(new { success = true });
	}

	/// Handles the health error endpoint and simulates an unhandled error response.
	/// Logs an error message and returns a response with a 500 Internal Server Error status code containing
	/// details about the error, such as an error flag, a description message, and a timestamp.
	/// <return>
	/// An IActionResult representing a 500 Internal Server Error with error details.
	/// </return>
	[HttpGet]
	[Route("/health/error")]
	public IActionResult HealthError()
	{
		_logger.LogError("error test called");
		return StatusCode(500, new
		{
			error = true,
			message = "Unhandled exception, testing ex for apps insights",
			timestamp = DateTimeOffset.UtcNow
			
		});
	}
}