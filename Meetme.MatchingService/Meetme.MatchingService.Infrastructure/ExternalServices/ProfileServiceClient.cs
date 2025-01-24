using Meetme.MatchingService.Application.Common.Interfaces;
using Meetme.MatchingService.Domain.DataTransferObjects;
using Meetme.MatchingService.Infrastructure.Common;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Meetme.MatchingService.Infrastructure.ExternalServices;


public class ProfileServiceClient : IProfileServiceClient
{
	private readonly HttpClient _httpClient;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public ProfileServiceClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
	{
		_httpClient = httpClient;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task<ProfileDto?> GetProfileAsync(Guid id)
    {
		var authorizationToken = GetAuthorizationToken();
		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(ProfileServiceKeys.AuthenticationHeaderScheme, authorizationToken);

        var profileResponse = await _httpClient.GetAsync(ProfileServiceKeys.GetPreferenceByIdUrl + id);

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
