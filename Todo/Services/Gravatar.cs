using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Services
{
    public static class Gravatar
    {
        private static readonly HttpClient httpClient = new HttpClient();

        static Gravatar()
        {
            // This should be kept in a secret store, not in source
            const string apiKey = "460:gk-2K8f1K-zLtcvqUNrwdwlvIOsmhzM9SAuzbhwDT4cwpdMjHkvnaLbt5oWLZD6F";
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            httpClient.Timeout = TimeSpan.FromSeconds(2);
        }

        public static string GetHash(string emailAddress)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.Default.GetBytes(emailAddress.Trim().ToLowerInvariant());
                var hashBytes = md5.ComputeHash(inputBytes);

                var builder = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    builder.Append(b.ToString("X2"));
                }
                return builder.ToString().ToLowerInvariant();
            }
        }

        public static async Task<string> GetName(string emailAddress)
        {
            try
            {
                var url = BuildProfileUrl(emailAddress);

                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var json = System.Text.Json.JsonDocument.Parse(content);
                return json.RootElement.GetProperty("display_name").ToString();
            }
            catch
            {
                // Return an empty string on error or timeout
                return string.Empty;
            }
        }

        private static string BuildProfileUrl(string emailAddress)
        {
            var inputBytes = Encoding.Default.GetBytes(emailAddress.Trim().ToLowerInvariant());
            var hashBytes = SHA256.HashData(inputBytes);
            var hash = new StringBuilder();
            foreach (var b in hashBytes)
            {
                hash.Append(b.ToString("X2"));
            }
            // We might want this 3rd party URL to be in config rather than hardcoded
            var url = $"https://api.gravatar.com/v3/profiles/{hash.ToString().ToLowerInvariant()}";
            return url;
        }
    }
}