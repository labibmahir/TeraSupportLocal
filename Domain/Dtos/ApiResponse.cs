using System.ComponentModel;

namespace Domain.Dtos;

/// <summary>
/// Represents a basic API response with a success flag and an optional message.
/// </summary>
/// <summary>
/// Represents a basic API response with a success flag and an optional message.
/// </summary>
public record ApiResponse(bool IsSuccess, string? Message = null)
{
    /// <summary>
    /// Creates a successful API response.
    /// </summary>
    /// <param name="message">An optional success message.</param>
    /// <returns>An <see cref="ApiResponse"/> indicating success.</returns>
    public static ApiResponse Success(string? message = null) => new(true, message);

    /// <summary>
    /// Creates a failed API response.
    /// </summary>
    /// <param name="message">A message indicating the reason for failure.</param>
    /// <returns>An <see cref="ApiResponse"/> indicating failure.</returns>
    public static ApiResponse Fail(string? message) => new(false, message);
}

/// <summary>
/// Represents an API response with a success flag, data, and an optional message.
/// </summary>
/// <typeparam name="TData">The type of data included in the response.</typeparam>
public record ApiResponse<TData>(bool IsSuccess, TData? Data, string? Message = null)
{
    /// <summary>
    /// Creates a successful API response with data.
    /// </summary>
    /// <param name="data">The data included in the response.</param>
    /// <returns>An <see cref="ApiResponse{TData}"/> indicating success and containing the data.</returns>
    public static ApiResponse<TData> Success(TData data) => new(true, data, null);

    /// <summary>
    /// Creates a failed API response.
    /// </summary>
    /// <param name="message">A message indicating the reason for failure.</param>
    /// <returns>An <see cref="ApiResponse{TData}"/> indicating failure.</returns>
    public static ApiResponse<TData> Fail(string? message) => new(false, default, message);
}
public class PagedResultDto<T>
{
    /// <summary>
    /// Gets or sets the list of data items for the current page.
    /// </summary>
    [Description("The list of data items for the current page.")]
    public List<T> Data { get; set; } = new List<T>();

    /// <summary>
    /// Gets or sets the total number of items across all pages.
    /// </summary>
    [Description("The total number of items across all pages.")]
    public int TotalItems { get; set; }

    /// <summary>
    /// Gets or sets the current page number.
    /// </summary>
    [Description("The current page number.")]
    public int PageNumber { get; set; }

    /// <summary>
    /// Gets or sets the number of items per page.
    /// </summary>
    [Description("The number of items per page.")]
    public int PageSize { get; set; }
}