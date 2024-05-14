using CommunityToolkit.Mvvm.Messaging;

using RailwayResult;
using RailwayResult.FunctionalExtensions;

using TeamUp.Contracts.Events;
using TeamUp.Contracts.Teams;
using TeamUp.DAL.Api;
using TeamUp.DAL.Cache;
using TeamUp.DAL.Messages;

namespace TeamUp.DAL.Services;

public sealed class EventService
{
	private readonly ApiClient _client;
	private readonly IMessenger _messenger;
	private readonly CacheFacade _cache;
	private readonly IAuthService _authService;

	public EventService(ApiClient client, IMessenger messenger, CacheFacade cache, IAuthService authService)
	{
		_client = client;
		_messenger = messenger;
		_cache = cache;
		_authService = authService;
	}

	public Task<Result<List<EventSlimResponse>>> GetEventsAsync(TeamId teamId, bool forceFetch, CancellationToken ct)
	{
		return _cache.GetAsync($"team-{teamId.Value}-events", () => _client.GetEventsAsync(teamId, ct), TimeSpan.FromMinutes(5), forceFetch, ct);
	}

	public Task<Result<EventResponse>> GetEventAsync(TeamId teamId, EventId eventId, bool forceFetch, CancellationToken ct)
	{
		return _cache.GetAsync($"event-{eventId.Value}", () => _client.GetEventAsync(teamId, eventId, ct), TimeSpan.FromMinutes(5), forceFetch, ct);
	}

	public async Task<Result<EventId>> CreateEventAsync(TeamId teamId, CreateEventRequest request, CancellationToken ct)
	{
		request.FromUtc = request.FromUtc.ToUniversalTime();
		request.ToUtc = request.ToUtc.ToUniversalTime();

		var result = await _client.CreateEventAsync(teamId, request, ct);
		return result.Tap(teamId =>
		{
			_messenger.Send(new EventCreatedMessage
			{
			});
		});
	}
}
