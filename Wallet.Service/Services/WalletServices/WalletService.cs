using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Wallet.Shared.Contract.Dtos;
using Wallet.Shared.Contract.ResultDtos;
using Wallet.Shared.Contract.ViewModels.TransactionVm;
using Wallet.Shared.Contract.ViewModels.WalletVm;
using Wallet.Shared.Contract.WalletTransaction;

namespace Wallet.Service.Services.WalletServices
{
    public  class WalletService: IWalletService
    {
        readonly HttpClient _client = new HttpClient();
        public async Task<ResponseDto<List<SubSysVM>>> GetAllSubSystem(string serverName)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var response = await _client.GetAsync("api/Wallet/GetAllSubSys");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject <ResponseDto<List<SubSysVM>>>(responseContent);
                }
                else
                {
                    throw new Exception("Error in GetAllSubSys");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseDto<int>> Create(string serverName, WalletVm data)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(data);

                var response = await _client.PostAsync("api/Wallet/AddWallet", new StringContent(json, Encoding.UTF8, "application/json"));

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


        public async Task<ResponseDto<bool>> CreateTransaction(string serverName, CreateWalletTransactionDto data )
        {
            try
            {
                var _client = new HttpClient();
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(data);

                var response = await _client.PostAsync("api/Wallet/CreateTransaction", new StringContent(json, Encoding.UTF8, "application/json"));


                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    try
                    {
                        return JsonConvert.DeserializeObject<ResponseDto<bool>>(responseContent);
                    }
                    catch
                    {
                        var resultId = JsonConvert.DeserializeObject<bool>(responseContent);
                        return new ResponseDto<bool> { Data = resultId };
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
        public async Task<ResponseDto<bool>> Transactionwithdrawal(string serverName, CreateWalletTransactionDto data)
        {
            try
            {
                var _client = new HttpClient();
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(data);

                var response = await _client.PostAsync("api/Wallet/Transactionwithdrawal", new StringContent(json, Encoding.UTF8, "application/json"));


                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    try
                    {
                        return JsonConvert.DeserializeObject<ResponseDto<bool>>(responseContent);
                    }
                    catch
                    {
                        var resultId = JsonConvert.DeserializeObject<bool>(responseContent);
                        return new ResponseDto<bool> { Data = resultId };
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

        public async Task<ResponseDto<WalletVm>> GetById(string serverName, Guid Id)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var response = await _client.GetAsync($"api/Wallet/GetByIdWallet/{Id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ResponseDto<WalletVm>>(responseContent);
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

                var response = await _client.PostAsync($"api/Wallet/DeleteWallet/{Id}", null);
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

        public async Task<ResponseDto<List<WalletVm>>> GetAll(string serverName)
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
                    // چون API آرایه برمی‌گرداند، ابتدا به لیست تبدیل می‌کنیم
                    var data = JsonConvert.DeserializeObject<List<WalletVm>>(responseContent);
                    return new ResponseDto<List<WalletVm>> { Data = data };
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

        public async Task<ResponseDto<bool>> Update(string serverName, WalletVm data)
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

      
    }
}
