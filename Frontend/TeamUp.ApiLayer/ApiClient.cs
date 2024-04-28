using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using RailwayResult;

using TeamUp.ApiLayer.Internal;
using TeamUp.Contracts;

namespace TeamUp.ApiLayer;

public sealed partial class ApiClient
{
	private readonly HttpClient _client;

	public ApiClient(HttpClient client)
	{
		_client = client;
	}

	private Task<Result<TResponse>> SendAsync<TRequest, TResponse>(HttpMethod method, string uri, TRequest payload, CancellationToken ct) =>
		SendAsync<TRequest, TResponse>(method, uri, payload, null, ct);

	private async Task<Result<TResponse>> SendAsync<TRequest, TResponse>(HttpMethod method, string uri, TRequest payload, Action<HttpRequestMessage>? configure, CancellationToken ct)
	{
		var json = JsonSerializer.Serialize(payload);
		var request = new HttpRequestMessage
		{
			Method = method,
			RequestUri = new Uri(uri, UriKind.Relative),
			Content = new StringContent(json, Encoding.UTF8, "application/json"),
		};

		if (configure is not null)
		{
			configure(request);
		}

		var response = await _client.SendAsync(request, ct);

		if (!response.IsSuccessStatusCode)
		{
			if (response.StatusCode == HttpStatusCode.Unauthorized)
			{
				//logout
			}

			var contentType = response.Content.Headers.ContentType?.MediaType;
			if (contentType != "application/problem+json")
			{
				return new ApiError("Api.UnexpectedError", $"Unexpected response with content type '{contentType}'.", response.StatusCode);
			}

			var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>(ct);
			if (problemDetails is null)
			{
				return new ApiError("Api.SerializationError", "Failed to deserialize error response.", response.StatusCode);
			}

			var title = problemDetails.Title ?? "Undefined Title";
			var detail = problemDetails.Detail ?? "Undefined Detail";

			if (problemDetails.Errors.Count > 0)
			{
				return new ApiValidationError("Api.ValidationError", "One or more validation errors occurred.", problemDetails.Errors);
			}

			return new ApiError("Api.Error", detail, response.StatusCode);
		}

		return await response.Content.ReadFromJsonAsync<TResponse>(ct);
	}
}
