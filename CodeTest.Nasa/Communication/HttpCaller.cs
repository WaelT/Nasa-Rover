using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CodeTest.Nasa.Models;
using Newtonsoft.Json;
namespace CodeTest.Nasa.Communication
{
    public class HttpCaller
    {
        public CallResponse Call(string url, string method, Dictionary<string, string> parameters)
        {
            var httpClient = new HttpClient { BaseAddress = new Uri(url) };
            string paramString = string.Empty;
            foreach (var pair in parameters)
            {
                paramString += $"{pair.Key}={pair.Value}";
                if (parameters.Last().Key != pair.Key)
                    paramString += "&";
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{method}?{paramString}");
            var sendTask = httpClient.SendAsync(request);
            sendTask.Wait();
            var response = sendTask.Result;
            var contentTask = response.Content.ReadAsStringAsync();
            contentTask.Wait();
            stopWatch.Stop();
            var callResponse = JsonConvert.DeserializeObject<CallResponse>(contentTask.Result);
            callResponse.CallDurationInMs = stopWatch.ElapsedMilliseconds;
            return callResponse;
        }
    }
}
