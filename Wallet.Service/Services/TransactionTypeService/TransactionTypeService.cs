using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Wallet.Shared.Contract.ViewModels.TransactionTypeVm;
using Wallet.Shared.Contract.WalletTransaction;

namespace Wallet.Service.Services.TransactionTypeService
{
    public  class TransactionTypeService: ITransactionTypeService
    {
        readonly HttpClient _client = new HttpClient();

        public async Task<int> Create(string serverName, TransactionTypeVm data)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(data);

                var response = await _client.PostAsync("api/TransactionType/AddWalletTransactionType", new StringContent(json, Encoding.UTF8, "application/json"));

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

        public async Task<TransactionTypeVm> GetById(string serverName, Guid Id)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // آدرس اصلاح شده: api/TransactionType/{Id} (بدون کلمه GetById)
                var response = await _client.GetAsync($"api/TransactionType/GetWalletTransactionTypeById/{Id}");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TransactionTypeVm>(responseContent);
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

                var response = await _client.PostAsync($"api/TransactionType/DeleteWalletTransactionType/{Id}", null);

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

        public async Task<List<TransactionTypeVm>> GetAll(string serverName)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var response = await _client.GetAsync("api/TransactionType/GetAllWalletTransactionsType");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TransactionTypeVm>>(responseContent);
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

        public async Task<bool> Update(string serverName, TransactionTypeVm data)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(data);

                var response = await _client.PostAsync("api/TransactionType/UpdateWalletTransactionsType", new StringContent(json, Encoding.UTF8, "application/json"));

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

        public async Task<List<TransactionTypeForWalletVm>> GetTransactionForWallet(string serverName)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var response = await _client.GetAsync("api/TransactionType/GetForWallet");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TransactionTypeForWalletVm>>(responseContent);
                }
                else
                {
                    throw new Exception("Error in GetForWallet");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
