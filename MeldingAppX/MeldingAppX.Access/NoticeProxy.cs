using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MeldingAppX.Models;
using Newtonsoft.Json;


namespace MeldingAppX.Access
{
    public class NoticeProxy
    {
        public async Task<IEnumerable<Notice>> GetNotice()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1101/");

                var result = await client.GetAsync("/api/Notice");
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Notice>>(json);
            }
        }

        public async Task<Notice> GetNotice(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1101/");

                var result = await client.GetAsync(String.Format("/api/Notice/{0}", id));
                var json = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Notice>(json);
            }
        }

        public async Task<Notice> PostNotice(Notice notice)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1101/");

                var result = await client.PostAsJsonAsync("/api/Notice", notice);
                var json = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Notice>(json);

            }
        }

        public async Task<Notice> DeleteNotice(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1101/");

                var result = await client.DeleteAsync(String.Format("/api/Notice/{0}", id));
                var json = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Notice>(json);
            }
        }

        public async Task PutNotice(int id, Notice notice)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1101/");

                await client.PutAsJsonAsync(String.Format("api/Notice/{0}", id), notice);
            }
        }
    }
}
