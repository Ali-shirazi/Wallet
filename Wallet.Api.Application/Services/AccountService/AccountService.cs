using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Wallet.Shared.Contract.Dtos;
using Wallet.Shared.Contract.ResultDtos;

namespace Wallet.Api.Application.Services.AccountService
{
    public class AccountService : IAccountService
    {
        readonly HttpClient _client = new HttpClient();
        public async Task<LoginResponseDto> Login(string serverName, LoginRequestDto dto)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await _client.GetAsync("Account/Login");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrEmpty(responseContent))
                    {
                        return new LoginResponseDto();
                    }

                    return JsonConvert.DeserializeObject<LoginResponseDto>(responseContent) ?? new LoginResponseDto();
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error calling SSO: {response.StatusCode} - {errorContent}");
                    return new LoginResponseDto();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new LoginResponseDto();
            }
        }
    }
}
