namespace ResilentAPI.Dtos;

/// <summary>
/// Represents a log request containing the message and logging level.
/// </summary>
/// <remarks>
/// This class is used to encapsulate information about a log message, such as the message content
/// and the severity level (e.g., Information, Warning, Error). It is typically used in scenarios
/// where logging operations are required, such as capturing and recording application events.
/// </remarks>
public class LogRequest
{
	/// <summary>
	/// Represents the message to be logged.
	/// </summary>
	/// <remarks>
	/// The Message property contains the content of the log entry. It is used to convey information, warnings, errors, or other textual data
	/// that needs to be documented in the system logs. This property is typically set when creating a new log entry as part of a request to the logging endpoint.
	/// </remarks>
	public string Message { get; set; } = string.Empty;

	/// <summary>
	/// Represents the severity or priority level associated with the log message.
	/// </summary>
	public string Level { get; set; } = string.Empty;

}