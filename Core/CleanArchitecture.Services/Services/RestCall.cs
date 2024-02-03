using CleanArchitecture.Application.Commons.Results;
using CleanArchitecture.Domain.Contracts.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Services
{
    public class RestCall : IRestCall
    {
        private readonly HttpClient _client = new HttpClient();

        public QueryOrCommandResult<T> DeleteRestcall<T>(string uri, string endpoints, Guid id)
        {
            
            var myuri = new Uri(uri+endpoints+$"/{id}");
 
            var response = _client.DeleteAsync(myuri).Result;


            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };


                var responseContent = response.Content.ReadAsStringAsync().Result;

                var DelereResponse = JsonSerializer.Deserialize<QueryOrCommandResult<T>>(responseContent, options);

                return DelereResponse;
            }

            return new QueryOrCommandResult<T>();

            
        }

        public QueryOrCommandResult<T> GetRestcall<T>(string uri, string endpoints)
        {
            var myuri = new Uri(uri);

            var response = _client.GetAsync(myuri + endpoints).Result;


            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };


                var responseContent = response.Content.ReadAsStringAsync().Result;

                var GetResponse = JsonSerializer.Deserialize<QueryOrCommandResult<T>>(responseContent,options);

                return GetResponse;
            }
            else
            {

                return new QueryOrCommandResult<T>();
            }
            
            

        }

      

        public QueryOrCommandResult<T> PostRestcall<T>(string uri, string endpoints, object dto)
        {
            _client.BaseAddress = new Uri(uri);

            var json = JsonSerializer.Serialize(dto);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = _client.PostAsync(endpoints, content).Result;

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };


                var responseContent = response.Content.ReadAsStringAsync().Result;

                var PostResponse = JsonSerializer.Deserialize<QueryOrCommandResult<T>>(responseContent,options);

                return PostResponse;
            }

            return new  QueryOrCommandResult<T>();
        }

        public QueryOrCommandResult<T> PutRestcall<T>(string uri, string endpoints, object dto)
        {
            _client.BaseAddress = new Uri(uri);

            var json = JsonSerializer.Serialize(dto);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = _client.PutAsync(endpoints, content).Result;

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };


                var responseContent = response.Content.ReadAsStringAsync().Result;

                var PutResponse = JsonSerializer.Deserialize<QueryOrCommandResult<T>>(responseContent, options);

                return PutResponse;
            }

            return new QueryOrCommandResult<T>();
        }
    }
}
