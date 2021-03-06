﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Perka.Apply.Client.Adapters
{
    public interface IPerkaApiAdapter
    {
        Task<string> PostApplicationAsync(string body);
    }

    internal class PerkaApiAdapter : HttpClientAdapterBase, IPerkaApiAdapter
    {
        private readonly string _endpoint = ApplicationSettingsAdapter.ApplicationSettings.PerkaApi.Uri;

        public PerkaApiAdapter() : this(null)
        {
        }

        private PerkaApiAdapter(HttpMessageHandler handler) : base(handler)
        {
        }

        public async Task<string> PostApplicationAsync(string body)
        {
            try
            {
                return await HandleResponse(await PostAsync(new Uri(_endpoint), new StringContent(body ?? "", Encoding.UTF8, ContentType)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        protected override void SetHttpHeaders()
        {
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            Client.DefaultRequestHeaders.UserAgent.ParseAdd("Perka.Apply.Client/1.0");
        }
    }
}