using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace Client.Services
{
    public class TokenService : ITokenService
    {

        public readonly IOptions<IdentityServerSettings> options;
        public readonly DiscoveryDocumentResponse discoveryDocumentResponse;
        public readonly HttpClient _httpClient ;

        public TokenService(IOptions<IdentityServerSettings> options
            )
        {
            this.options = options;
            _httpClient = new HttpClient();
            this.discoveryDocumentResponse = _httpClient.GetDiscoveryDocumentAsync(
                this.options.Value.DiscoveryUrl).Result;


            if (discoveryDocumentResponse.IsError)
                throw new Exception("Unable to get discovery document", discoveryDocumentResponse.Exception);

        }

        public async Task<TokenResponse> GetToken(string scope)
        {
            var token = await _httpClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = discoveryDocumentResponse.TokenEndpoint,
                    ClientId = options.Value.ClientName,
                    ClientSecret = options.Value.ClientPassword,
                    Scope = scope
                });

            if(token.IsError)
                throw new Exception("Unable to get token ", token.Exception);

            return token;
        }
    }
}
