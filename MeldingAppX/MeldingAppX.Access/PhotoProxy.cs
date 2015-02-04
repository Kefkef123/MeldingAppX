using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MeldingAppX.Models;
using Newtonsoft.Json;

namespace MeldingAppX.Access
{
    public class PhotoProxy
    {
        public PhotoProxy(string uri)
        {
            _uri = new Uri(uri);
        }

        public async Task<IEnumerable<Photo>> GetAll()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _uri;

                var result = await client.GetAsync("/api/Photo/");
                var json = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<Photo>>(json);
            }
        }

        public async Task<Photo> Get(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _uri;

                var result = await client.GetAsync(String.Format("/api/Photo/{0}", id));
                var json = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Photo>(json);
            }
        }

        public async Task Post(Photo photo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _uri;

                await client.PostAsJsonAsync("/api/Photo/", photo);
            }
        }

        private Uri _uri;
    }
}