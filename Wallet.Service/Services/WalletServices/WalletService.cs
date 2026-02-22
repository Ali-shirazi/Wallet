using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Wallet.Shared.Contract.Dtos;
using Wallet.Shared.Contract.ViewModels.WalletVm;
using Wallet.Shared.Contract.WalletTransaction;

namespace Wallet.Service.Services.WalletServices
{
    public  class WalletService: IWalletService
    {
        readonly HttpClient _client = new HttpClient();

        public async Task<int> Create(string serverName, WalletVm data)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(data);

                var response = await _client.PostAsync("api/Wallet/AddWallet", new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<int>(responseContent);
                }
                else
                {
                    throw new Exception("Error in Create");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> CreateTransaction(string serverName, CreateWalletTransactionDto data )
        {
            try
            {
                var _client = new HttpClient();
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(data);

                var response = await _client.PostAsync("api/Wallet/CreateTransaction", new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<bool>(responseContent);
                }
                else
                {
                    throw new Exception("Error in CreateTransaction");
                }
        }
            catch (Exception)
            {
                throw;
            }
}
        public async Task<bool> Transactionwithdrawal(string serverName, CreateWalletTransactionDto data)
        {
            try
            {
                var _client = new HttpClient();
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(data);

                var response = await _client.PostAsync("api/Wallet/Transactionwithdrawal", new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(responseContent);
                    return jObject["data"].Value<bool>();
                }
                else
                {
                    throw new Exception("Error in Transactionwithdrawal");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<WalletVm> GetById(string serverName, Guid Id)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var response = await _client.GetAsync($"api/Wallet/GetByIdWallet/{Id}");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<WalletVm>(responseContent);
                }
                else
                {
                    throw new Exception("Error in GetById");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(string serverName, Guid Id)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await _client.PostAsync($"api/Wallet/DeleteWallet/{Id}", null);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<bool>(responseContent);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<WalletVm>> GetAll(string serverName)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var response = await _client.GetAsync("api/Wallet/GetAllWallets");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<WalletVm>>(responseContent);
                }
                else
                {
                    throw new Exception("Error in GetAll");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(string serverName, WalletVm data)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(data);

                var response = await _client.PostAsync("api/Wallet/UpdateWallet", new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<bool>(responseContent);
                }
                else
                {
                    throw new Exception("Error in Update");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

      
    }
}
