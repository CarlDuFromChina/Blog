using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Sixpence.Common.Http
{
	public class HttpRequestBuilder
	{
		private HttpMethod method = null;

		private string requestUri = string.Empty;

		private HttpContent content = null;

		private string bearerToken = string.Empty;

		private string basicAuth = string.Empty;

		private string acceptHeader = "application/json";

		public HttpRequestBuilder AddMethod(HttpMethod method)
		{
			this.method = method;
			return this;
		}

		public HttpRequestBuilder AddRequestUri(string requestUri)
		{
			this.requestUri = requestUri;
			return this;
		}

		public HttpRequestBuilder AddContent(HttpContent content)
		{
			this.content = content;
			return this;
		}

		public HttpRequestBuilder AddBearerToken(string bearerToken)
		{
			this.bearerToken = bearerToken;
			return this;
		}

		public HttpRequestBuilder AddBasicAuth(string basicAuthStr)
		{
			basicAuth = basicAuthStr;
			return this;
		}

		public HttpRequestBuilder AddAcceptHeader(string acceptHeader)
		{
			this.acceptHeader = acceptHeader;
			return this;
		}

		private void EnsureArguments()
		{
			if (method == null)
			{
				throw new ArgumentNullException("Method");
			}
			if (string.IsNullOrEmpty(requestUri))
			{
				throw new ArgumentNullException("Request Uri");
			}
			if (!string.IsNullOrEmpty(basicAuth) && !string.IsNullOrEmpty(bearerToken))
			{
				throw new ArgumentNullException("Authorization not sure!");
			}
		}

		public HttpRequestMessage ToHttpRequestMessage()
		{
			EnsureArguments();
			HttpRequestMessage httpRequestMessage = new HttpRequestMessage
			{
				Method = method,
				RequestUri = new Uri(requestUri)
			};
			if (content != null)
			{
				httpRequestMessage.Content = content;
			}
			if (!string.IsNullOrEmpty(bearerToken))
			{
				httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
			}
			if (!string.IsNullOrEmpty(basicAuth))
			{
				httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", basicAuth);
			}
			httpRequestMessage.Headers.Accept.Clear();
			if (!string.IsNullOrEmpty(acceptHeader))
			{
				httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptHeader));
			}
			return httpRequestMessage;
		}
	}
}
