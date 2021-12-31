using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sixpence.Common.Http
{
    public static class HttpClientExtension
    {
		public static TimeSpan DefaultTimeout = new TimeSpan(0, 0, 10);

		public static async Task<HttpResponseMessage> GetAsync(this HttpClient client, string requestUri, string basicAuthStr = "", string bearerToken = "")
		{
			HttpRequestBuilder builder = new HttpRequestBuilder().AddMethod(HttpMethod.Get).AddRequestUri(requestUri).AddBearerToken(bearerToken)
				.AddBasicAuth(basicAuthStr);
			return await client.SendAsync(builder);
		}

		public static async Task<HttpResponseMessage> PostAsync(this HttpClient client, string requestUri, object value, string basicAuthStr = "", string bearerToken = "")
		{
			HttpRequestBuilder builder = new HttpRequestBuilder().AddMethod(HttpMethod.Post).AddRequestUri(requestUri).AddContent(new JsonContent(value))
				.AddBearerToken(bearerToken)
				.AddBasicAuth(basicAuthStr);
			return await client.SendAsync(builder);
		}

		public static async Task<HttpResponseMessage> PostXml(this HttpClient client, string requestUri, string value, string basicAuthStr = "", string bearerToken = "")
		{
			HttpRequestBuilder builder = new HttpRequestBuilder().AddMethod(HttpMethod.Post).AddRequestUri(requestUri).AddContent(new XmlContent(value))
				.AddBasicAuth(basicAuthStr)
				.AddBearerToken(bearerToken);
			return await client.SendAsync(builder);
		}

		public static async Task<HttpResponseMessage> Put(this HttpClient client, string requestUri, object value, string basicAuthStr = "", string bearerToken = "")
		{
			HttpRequestBuilder builder = new HttpRequestBuilder().AddMethod(HttpMethod.Put).AddRequestUri(requestUri).AddContent(new JsonContent(value))
				.AddBasicAuth(basicAuthStr)
				.AddBearerToken(bearerToken);
			return await client.SendAsync(builder);
		}

		public static async Task<HttpResponseMessage> Patch(this HttpClient client, string requestUri, object value, string basicAuthStr = "", string bearerToken = "")
		{
			HttpRequestBuilder builder = new HttpRequestBuilder().AddMethod(new HttpMethod("PATCH")).AddRequestUri(requestUri).AddContent(new PatchContent(value))
				.AddBasicAuth(basicAuthStr)
				.AddBearerToken(bearerToken);
			return await client.SendAsync(builder);
		}

		public static async Task<HttpResponseMessage> Delete(this HttpClient client, string requestUri, string basicAuthStr = "", string bearerToken = "")
		{
			HttpRequestBuilder builder = new HttpRequestBuilder().AddMethod(HttpMethod.Delete).AddRequestUri(requestUri).AddBasicAuth(basicAuthStr)
				.AddBearerToken(bearerToken);
			return await client.SendAsync(builder);
		}

		public static async Task<HttpResponseMessage> PostFile(this HttpClient client, string requestUri, string filePath, string apiParamName, string basicAuthStr = "", string bearerToken = "")
		{
			HttpRequestBuilder builder = new HttpRequestBuilder().AddMethod(HttpMethod.Post).AddRequestUri(requestUri).AddContent(new FileContent(filePath, apiParamName))
				.AddBasicAuth(basicAuthStr)
				.AddBearerToken(bearerToken);
			return await client.SendAsync(builder);
		}

		public static async Task<HttpResponseMessage> SendAsync(this HttpClient client, HttpRequestBuilder builder)
		{
			if (builder == null)
			{
				return null;
			}
			HttpRequestMessage httpRequest = builder.ToHttpRequestMessage();
			return await client.SendAsync(httpRequest);
		}
	}
}
