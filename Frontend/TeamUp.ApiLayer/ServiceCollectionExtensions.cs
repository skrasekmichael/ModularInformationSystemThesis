using Microsoft.Extensions.DependencyInjection;

namespace TeamUp.ApiLayer;

public static class ServiceCollectionExtensions
{
	public static void AddApiClient(this IServiceCollection services, string api)
	{
		services
			.AddHttpClient<ApiClient>("ApiHttpClient")
			.ConfigureHttpClient(client =>
			{
				client.BaseAddress = new Uri(api);
			});
	}
}
