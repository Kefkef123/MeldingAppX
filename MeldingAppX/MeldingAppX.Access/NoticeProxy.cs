using System;
using System.Collections.Generic;
using System.Net.Http;
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
    }
}
