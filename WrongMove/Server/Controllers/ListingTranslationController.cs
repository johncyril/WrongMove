using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace WrongMove.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListingTranslationController : ControllerBase
    {
        private readonly Dictionary<string, string> _dictionary;

        public ListingTranslationController()
        {
            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("WrongMove.Server.Controllers.dictionary.json")))
            {
                var content = reader.ReadToEnd();
                _dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
            }
        }

        [HttpPost]
        public async Task<string> PostAsync([FromBody] Uri listingUri)
        {
            var rawListing = await GetListingAsync(listingUri);

            var content = UpdateListingBasedOnDictionary(rawListing);

            return content;
        }

        private async Task<string> GetListingAsync(Uri listingUri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, listingUri);
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; rv:36.0) Gecko/20100101 Firefox/36.0");
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }

        private string UpdateListingBasedOnDictionary(string rawListing)
        {
            foreach (var key in _dictionary.Keys)
            {
                rawListing = rawListing.Replace(key, $"<strike>{key}</strike><span style=\"color: red;font-weight:bold\">{_dictionary[key]}</span>", StringComparison.InvariantCultureIgnoreCase);
            }

            return rawListing;
        }

    }
}
