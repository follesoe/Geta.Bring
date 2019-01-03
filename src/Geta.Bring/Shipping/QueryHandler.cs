﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
#if CACHE
using System.Web.Caching;
#endif
using Geta.Bring.Shipping.Extensions;
using Geta.Bring.Shipping.Model;
using Geta.Bring.Shipping.Model.Errors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Geta.Bring.Shipping
{
    public interface IQueryHandler
    {
        bool CanHandle(Type type);
        Task<EstimateResult<IEstimate>> FindEstimatesAsync(EstimateQuery query);
    }

    public abstract class QueryHandler<T> : IQueryHandler
        where T : IEstimate
    {
        protected QueryHandler(ShippingSettings settings, string methodName)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            MethodName = methodName ?? throw new ArgumentNullException(nameof(methodName));
        }

        public bool CanHandle(Type type)
        {
            return type == typeof(T);
        }

        public string MethodName { get; }

        public ShippingSettings Settings { get; }

        internal abstract T MapProduct(ProductResponse response);

        public async Task<EstimateResult<IEstimate>> FindEstimatesAsync(EstimateQuery query)
        {
            HttpResponseMessage responseMessage = null;
            string jsonResponse = null;

            var requestUri = CreateRequestUri(query);
#if CACHE
            var cacheKey = CreateCacheKey(requestUri);

            if (HttpRuntime.Cache.Get(cacheKey) is EstimateResult<IEstimate> cached)
                return await Task.FromResult(cached);
#endif

            using (var client = CreateClient())
            {
                try
                {
                    responseMessage = await client.GetAsync(requestUri).ConfigureAwait(false);
                    jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                    responseMessage.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException)
                {
                    if (string.IsNullOrEmpty(jsonResponse))
                    {
                        var responseError = new ResponseError(responseMessage?.StatusCode ?? HttpStatusCode.InternalServerError);
                        return EstimateResult<IEstimate>.CreateFailure(responseError);
                    }
                }
            }

            var response = JsonConvert.DeserializeObject<ShippingResponse>(jsonResponse, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            var errors = response.GetAllErrors().ToArray();
            if (errors.Any())
            {
                return EstimateResult<IEstimate>.CreateFailure(errors);
            }

            var products = response.GetAllProducts();
            var estimates = products.Select(MapProduct).Cast<IEstimate>().ToList();
            var result = EstimateResult<IEstimate>.CreateSuccess(estimates);

#if CACHE
            HttpRuntime.Cache.Insert(cacheKey, result, null, DateTime.UtcNow.AddMinutes(2), Cache.NoSlidingExpiration);
#endif

            return result;
        }

        private string CreateCacheKey(Uri uri)
        {
            return string.Concat("EstimateResult", "-", typeof(T).Name, "-", uri.ToString());
        }

        private HttpClient CreateClient()
        {
            var client = new HttpClient();

            if (Settings.Uid != null)
            {
                client.DefaultRequestHeaders.Add("X-MyBring-API-Uid", Settings.Uid);
            }

            if (Settings.Key != null)
            {
                client.DefaultRequestHeaders.Add("X-MyBring-API-Key", Settings.Key);
            }

            client.DefaultRequestHeaders.Add("X-Bring-Client-URL", Settings.ClientUri.ToString());
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private Uri CreateRequestUri(EstimateQuery query)
        {
            var uri = new Uri(Settings.EndpointUri, MethodName);
            var queryItems = HttpUtility.ParseQueryString(string.Empty); // This creates empty HttpValueCollection which creates query string on ToString
            queryItems.Add(query.Items);

            var ub = new UriBuilder(uri) { Query = queryItems.ToString() };
            return ub.Uri;
        }
    }
}