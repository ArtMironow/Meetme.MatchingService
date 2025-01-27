using Meetme.MatchingService.Application.Common.Interfaces;
using Meetme.MatchingService.Domain.DataTransferObjects;
using Meetme.MatchingService.Infrastructure.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Meetme.MatchingService.Infrastructure.ExternalServices;

public class ProfileServiceClient : IProfileServiceClient
{
	private readonly HttpClient _httpClient;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly ProfileServiceRoutes _profileServiceRoutes;

	public ProfileServiceClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IOptions<ProfileServiceRoutes> profileServiceRoutesAccessor)
	{
		_httpClient = httpClient;
		_httpContextAccessor = httpContextAccessor;
		_profileServiceRoutes = profileServiceRoutesAccessor.Value;
	}

	public async Task<ProfileDto?> GetProfileAsync(Guid id, CancellationToken cancellationToken)
    {
		var authorizationToken = GetAuthorizationToken();
		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(ProfileServiceKeys.AuthenticationHeaderScheme, authorizationToken);

		var profileResponse = await _httpClient.GetAsync(_profileServiceRoutes.BaseUrl + id, cancellationToken);

		profileResponse.EnsureSuccessStatusCode();

		var profile = JsonConvert.DeserializeObject<ProfileDto>(await profileResponse.Content.ReadAsStringAsync());

		return profile;
    }

	private string? GetAuthorizationToken()
	{
		var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers[ProfileServiceKeys.AuthorizationHeaderName].ToString();
		return authorizationHeader.Remove(0, ProfileServiceKeys.AuthenticationHeaderScheme.Length + 1);
	}
}
