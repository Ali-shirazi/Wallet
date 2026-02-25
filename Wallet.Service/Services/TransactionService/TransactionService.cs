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

namespace Wallet.Service.Services.TransactionService
{
    public class TransactionService : ITransactionService
    {
        readonly HttpClient _client = new HttpClient();

        public async Task<ResponseDto<int>> Create(string serverName, TransactionVm data)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(data);
                var response = await _client.PostAsync("api/Transaction/AddTransaction", new StringContent(json, Encoding.UTF8, "application/json"));
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
                        return new ResponseDto<int> { Data = resultId};
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

        public async Task<ResponseDto<TransactionVm>> GetById(string serverName, Guid Id)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await _client.GetAsync($"api/Transaction/GetTransactionById/{Id}");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ResponseDto<TransactionVm>>(responseContent);
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
                var response = await _client.PostAsync($"api/Transaction/DeleteTransactionById/{Id}", content);


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
                        return new ResponseDto<bool> { Data = result};
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

        public async Task<ResponseDto<List<TransactionVm>>> GetAll(string token,string serverName)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _client.GetAsync("api/Transaction/GetAllTransactions");
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    // چون API آرایه برمی‌گرداند، ابتدا به لیست تبدیل می‌کنیم
                    var data = JsonConvert.DeserializeObject<List<TransactionVm>>(responseContent);
                    return new ResponseDto<List<TransactionVm>> { Data = data };
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

        public async Task<ResponseDto<bool>> Update(string serverName, TransactionVm data)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(data);
                var response = await _client.PostAsync("api/Transaction/UpdateTransaction", new StringContent(json, Encoding.UTF8, "application/json"));

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

        public async Task<ResponseDto<List<TransactionVm>>> GetTransactionByWalletId(string serverName, Guid Id)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await _client.GetAsync($"api/Transaction/GetTransationByWalletId/{Id}");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    // چون API آرایه برمی‌گرداند، ابتدا به لیست تبدیل می‌کنیم
                    var data = JsonConvert.DeserializeObject<List<TransactionVm>>(responseContent);
                    return new ResponseDto<List<TransactionVm>> { Data = data };
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
    }
}