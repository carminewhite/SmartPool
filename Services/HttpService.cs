using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class HttpService
{
    private static HttpClient _httpClient;
    public HttpService(HttpClient client)
    {
        _httpClient = client;
    }
        public static async Task<List<string>> GetGeoCode(string address)  
        {  
            // address = "1600+Amphitheatre+Parkway,+Mountain+View,+CA";
            address = address.Replace(" ", "+");

            string baseApiURl = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key=AIzaSyDhc_4rfCm9kufSuafhtXU56C_gaeayl1o";
            var uri = new Uri(Uri.EscapeUriString(baseApiURl));

            var result = await _httpClient.GetAsync(uri);
            string resultString = result.Content.ReadAsStringAsync().Result;

            dynamic data = JObject.Parse(resultString);

            if(data.status == "ZERO_RESULTS")
            {
                List<string> failed = new List<string>();
                failed.Add("ERROR");

                return failed;
            }

            System.Console.WriteLine(data);

            string lat = data["results"][0]["geometry"]["location"]["lat"];
            string lng = data["results"][0]["geometry"]["location"]["lng"];

            List<string> output = new List<string>();
            output.Add(lat);
            output.Add(lng);
            // System.Console.WriteLine( lat +" "+ lng);

            return output;
        }
}