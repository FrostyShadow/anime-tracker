using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AnimeTracker.Interfaces;
using Newtonsoft.Json;

namespace AnimeTracker.Models
{
    public class JikanService : IJikanService
    {
        private readonly HttpClient _client;
        public JikanService()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://api.jikan.moe/v3")
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        
    }
}