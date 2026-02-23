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
    public class TransactionService: ITransactionService
    {
        readonly HttpClient _client = new HttpClient();

        public async Task<int> Create(string serverName, TransactionVm data)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(data);

                var response = await _client.PostAsync("api/Transaction/AddTransaction", new StringContent(json, Encoding.UTF8, "application/json"));

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

        public async Task<ResponseDto<TransactionVm>> GetById(string serverName, Guid Id)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // آدرس اصلاح شده: api/Transaction/{Id} (بدون کلمه GetById)
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

        public async Task<bool> Delete(string serverName, Guid Id)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await _client.PostAsync($"api/Transaction/DeleteTransactionById/{Id}", null);

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

        public async Task<List<TransactionVm>> GetAll(string serverName)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var response = await _client.GetAsync("api/Transaction/GetAllTransactions");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TransactionVm>>(responseContent);
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

        public async Task<List<TransactionVm>> GetTransactionByWalletId(string serverName, Guid Id)
        {
            //try
            //{
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // آدرس اصلاح شده: api/Transaction/{Id} (بدون کلمه GetById)
                var response = await _client.GetAsync($"api/Transaction/GetTransationByWalletId/{Id}");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TransactionVm>>(responseContent);
                }
                else
                {
                    throw new Exception("GetTransactionByWalletId");
                }
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }



    }
}
