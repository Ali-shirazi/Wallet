using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Wallet.Shared.Contract.ResultDtos;
using Wallet.Shared.Contract.ViewModels.TransactionTypeVm;
using Wallet.Shared.Contract.ViewModels.TransactionVm;
using Wallet.Shared.Contract.WalletTransaction;

namespace Wallet.Service.Services.TransactionTypeService
{
    public class TransactionTypeService : ITransactionTypeService
    {
        readonly HttpClient _client = new HttpClient();

        public async Task<ResponseDto<int>> Create(string serverName, TransactionTypeVm data)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(data);
                var response = await _client.PostAsync("api/TransactionType/AddWalletTransactionType", new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        return JsonConvert.DeserializeObject<ResponseDto<int>>(responseContent);
                    }
                    catch
                    {
                        var resultId = JsonConvert.DeserializeObject<int>(responseContent);
                        return new ResponseDto<int> { Data = resultId };
                    }
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



        public async Task<ResponseDto<TransactionTypeVm>> GetById(string serverName, Guid Id)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await _client.GetAsync($"api/TransactionType/GetWalletTransactionTypeById/{Id}");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ResponseDto<TransactionTypeVm>>(responseContent);
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



        public async Task<ResponseDto<bool>> Delete(string serverName, Guid Id)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync($"api/TransactionType/DeleteWalletTransactionType/{Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        return JsonConvert.DeserializeObject<ResponseDto<bool>>(responseContent);
                    }
                    catch
                    {
                        var result = JsonConvert.DeserializeObject<bool>(responseContent);
                        return new ResponseDto<bool> { Data = result };
                    }
                }
                else
                {
                    throw new Exception("Error in Delete");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseDto<List<TransactionTypeVm>>> GetAll(string serverName)
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
                    // الگوی GetAll: فرض بر این است API آرایه برمی‌گرداند
                    var data = JsonConvert.DeserializeObject<List<TransactionTypeVm>>(responseContent);
                    return new ResponseDto<List<TransactionTypeVm>> { Data = data };
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

        public async Task<ResponseDto<bool>> Update(string serverName, TransactionTypeVm data)
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
                    return JsonConvert.DeserializeObject<ResponseDto<bool>>(responseContent);
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

        public async Task<ResponseDto<List<TransactionTypeForWalletVm>>> GetTransactionForWallet(string serverName)
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
                    var data = JsonConvert.DeserializeObject<List<TransactionTypeForWalletVm>>(responseContent);
                    return new ResponseDto<List<TransactionTypeForWalletVm>> { Data = data };
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