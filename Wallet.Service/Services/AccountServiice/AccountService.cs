using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Wallet.Shared.Contract.Dtos;
using Wallet.Shared.Contract.ViewModels.LoginVm;
using Wallet.Shared.Contract.ViewModels.TransactionVm;

namespace Wallet.Service.Services.AccountServiice
{
    public class AccountService : IAccountService
    {
        readonly HttpClient _client = new HttpClient();

        public async Task<LoginResponseDto> Login(string serverName, LoginRequestDto data)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(data);
                var response = await _client.PostAsync("api/Account/Login", new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<LoginResponseDto>(responseContent);
                }
                else
                {
                    throw new Exception("Error in Login");
                }
            }
            catch (Exception)
            {
                throw;
            }












        }
    }
}
