using MauiTransaction.Httph;
using MauiTransaction.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;

using Transaction = MauiTransaction.Models.Transaction;

namespace MauiTransaction.Data
{
    public class CRUDService : ICRUDService
    {
        private readonly HttpClient _cli;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public CRUDService(IHttpCli httpCli)
        {
            _cli = httpCli._cli;
            _jsonSerializerOptions = httpCli._jsonSerializerOptions;
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            Uri uri = new Uri(string.Format($"https://localhost:7044/Transaction/{id}"));

            try
            {
                HttpResponseMessage response = await _cli.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                    return true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            return false;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {

            IEnumerable<Category> categories = new List<Category>();

            Uri uri = new Uri(string.Format("https://localhost:7044/categories"));

            try
            {
                HttpResponseMessage response = await _cli.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    categories = JsonSerializer.Deserialize<IEnumerable<Category>>(content, _jsonSerializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.InnerException);
            }

            return categories;
        }

        public async Task<decimal> GetTotal(int mode)
        {
            decimal value = default;
            Uri uri;

            switch (mode)
            {
                case 1:
                    uri = new Uri(string.Format("https://localhost:7044/moneyincome"));
                    break;
                case 2:
                    uri = new Uri(string.Format("https://localhost:7044/moneyoutcome"));
                    break;

                default:
                    uri = new Uri(string.Format("https://localhost:7044/money"));
                    break;
            }

            HttpResponseMessage response = await _cli.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                value = JsonSerializer.Deserialize<decimal>(content, _jsonSerializerOptions);
            }

            return value;
        }

        public async Task<TransactionDTO> GetTransactionAsync(int id)
        {
            TransactionDTO transactionDTO = null;

            Uri uri = new Uri(string.Format($"https://localhost:7044/Transaction/{id}"));

            try
            {
                HttpResponseMessage response = await _cli.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    transactionDTO = JsonSerializer.Deserialize<TransactionDTO>(content, _jsonSerializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.InnerException);
            }

            return transactionDTO;
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsAsync()
        {
            IEnumerable<TransactionDTO> transactionDTOs = new List<TransactionDTO>();

            Uri uri = new Uri(string.Format("https://localhost:7044/Transaction", string.Empty));

            try
            {
                HttpResponseMessage response = await _cli.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    transactionDTOs = JsonSerializer.Deserialize<IEnumerable<TransactionDTO>>(content, _jsonSerializerOptions);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.InnerException);
            }

            return transactionDTOs;
        }

        public async Task<bool> SaveTransactionAsync(int id, Transaction transaction)
        {
            Uri uri = new Uri(string.Format($"https://localhost:7044/Transaction/{id}"));

            try
            {
                string json = JsonSerializer.Serialize<Transaction>(transaction, _jsonSerializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _cli.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                    return true;
            
            }catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            return false;
        }
    }
}
