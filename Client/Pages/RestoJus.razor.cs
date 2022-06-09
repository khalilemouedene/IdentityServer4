using API.Models;
using Client.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Client.Pages
{
    public partial class RestoJus
    {
        private List<RestoJusModel> jusModels = new();
        [Inject] private HttpClient HttpClient { get; set; }
        [Inject] private IConfiguration _configuration { get; set; }
        [Inject] private ITokenService  _tokenService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var token = await _tokenService.GetToken("JusAPI.read");

            HttpClient.SetBearerToken(token.AccessToken);
            var result = await HttpClient.GetAsync(_configuration["apiUrl"] + "/api/RestoJus");
            if (result.IsSuccessStatusCode)
            {
                jusModels = await result.Content.ReadFromJsonAsync<List<RestoJusModel>>();
            }
        }
    }
}
