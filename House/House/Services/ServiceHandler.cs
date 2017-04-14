using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace House
{
    public class ServiceHandler
    {
        static HttpClient client = new HttpClient();
        
        public ServiceHandler()
        {
            
        }

        public static async Task<T> GetDataAsync<T>(string endPoint)
        {
            client.DefaultRequestHeaders.ExpectContinue = false;
            T returnResult = default(T);
            var uri = new Uri(string.Format("{0}{1}", Constants.RestUrl, endPoint));
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                returnResult = JsonConvert.DeserializeObject<T>(content);

            }
            return returnResult;
        }

        public static async Task<T> PostData<T,Tr>(string endPoint, HttpMethod method, Tr content)
        {
            client.DefaultRequestHeaders.ExpectContinue = false;
            T returnResult = default(T);

            var uri = new Uri(string.Format("{0}{1}", Constants.RestUrl, endPoint));
            try
            {
                string jsonString = string.Empty;
                if (content != null)
                {
                    jsonString = JsonConvert.SerializeObject(content);
                }
                var httpcontent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, httpcontent);// HttpHelpers.Service.InvokeRequest(uri, method.Method, headers, jsonString);


                if (response.IsSuccessStatusCode)
                {
                    var content1 = response.Content.ReadAsStringAsync().Result;
                    returnResult = JsonConvert.DeserializeObject<T>(content1);
                }
            }
            catch (Exception ex)
            {
                string e = ex.InnerException.ToString();
            }
            return returnResult;
        }


        public static async Task<string> getall()
        {
            try
            {
                //var req = "http://api.openweathermap.org/data/2.5/forecast?id=524901&APPID=db2c108d9df98106c8cfba4060d05ebf ";// calling the string from the url 
                var req = "http://192.168.1.6:8085/ApiService/api/";
                client.DefaultRequestHeaders.ExpectContinue = false;
                Task<string> getStringTask = client.GetStringAsync(req);
                client.Timeout = TimeSpan.FromMinutes(60);
                string urlContents = await getStringTask;
            }
            catch (HttpRequestException e)
            {
                string ex = e.InnerException.Message;
            }
            catch (Exception ex)
            {
                string e = ex.InnerException.ToString();
            }

            return "True";

        }
    }
}
