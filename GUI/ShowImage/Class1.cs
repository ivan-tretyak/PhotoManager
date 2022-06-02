namespace JSONSendGet
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using static System.Net.Mime.MediaTypeNames;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Globalization;
    public class ResponseDataJSON
    {
        [JsonProperty("class_name")]
        public string ClassName;
        [JsonProperty("class_number")]
        public long ClassNumber;
        [JsonProperty("type")]
        public string Type;
    }
    public partial class ResponseJSON
    {
        [JsonProperty("data")]
        public Dictionary<string, ResponseDataJSON> Data { get; set; }
        public static ResponseJSON FromJson(string json) => JsonConvert.DeserializeObject<ResponseJSON>(json, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    public class RequestDataJSON
    {
        public string type { get; set; }
        public string src { get; set; }
        public string decode { get; set; }
        public RequestDataJSON(string path)
        {
            type = "image";
            decode = "utf-8";
            src = Convert.ToBase64String(File.ReadAllBytes(path));
        }

        public RequestDataJSON(byte[] image)
        {
            type = "image";
            decode = "utf-8";
            src = Convert.ToBase64String(image);
        }
    }
    public class RequestJSON
    {
        public RequestDataJSON data { get; set; }
        public RequestJSON(string path)
        {
            data = new RequestDataJSON(path);
        }
        public RequestJSON(byte[] image)
        {
            data = new RequestDataJSON(image);
        }
    }
    public class KeywordsForImage
    {
        private static ResponseJSON SendAsync(string path)
        {
            string url = @"http://127.0.0.1:5000/classification";
            var json = new StringContent(System.Text.Json.JsonSerializer.Serialize(new RequestJSON(path)),
                Encoding.UTF8,
                Application.Json);

            using var client = new HttpClient();
            var msg = new HttpRequestMessage(HttpMethod.Post, url);
            msg.Headers.Add("Content_Type", "application/json");
            msg.Headers.Add("Accept", "application/json");
            msg.Content = json;
            var result = client.Send(msg);
            var content = result.Content.ReadAsStringAsync();
            content.Wait();
            Console.WriteLine(content.Result);
            return ResponseJSON.FromJson(content.Result);
        }
        private static ResponseJSON SendAsync(byte[] image)
        {
            string url = @"https://mainservicetretyak.herokuapp.com/classification";
            var json = new StringContent(System.Text.Json.JsonSerializer.Serialize(new RequestJSON(image)),
                Encoding.UTF8,
                Application.Json);

            using var client = new HttpClient();
            var msg = new HttpRequestMessage(HttpMethod.Post, url);
            msg.Headers.Add("Content_Type", "application/json");
            msg.Headers.Add("Accept", "application/json");
            msg.Content = json;
            var result = client.Send(msg);
            var content = result.Content.ReadAsStringAsync();
            content.Wait();
            Console.WriteLine(content.Result);
            return ResponseJSON.FromJson(content.Result);
        }
        public static List<string> GetKeywords(string path)
        {
            try
            {
                List<string> keywords = new List<string>();
                ResponseJSON response = SendAsync(path);
                foreach (var value in response.Data.Values)
                {
                    keywords.Add(value.ClassName);
                }
                return keywords;
            }
            catch
            {
                return null;
            }
        }
        public static string[] GetKeywords(byte[] image)
        {
            try
            {
                List<string> keywords = new List<string>();
                ResponseJSON response = SendAsync(image);
                foreach (var value in response.Data.Values)
                {
                    foreach (var word in value.ClassName.Split(", "))
                    {
                        keywords.Add(word);
                    }
                }
                return keywords.ToArray();
            }
            catch
            {
                return null;
            }
        }
    }
}