using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Sixpence.Core.Http
{
	public class JsonContent : StringContent
	{
		public JsonContent(object value)
			: base(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json")
		{
		}

		public JsonContent(object value, string mediaType)
			: base(JsonConvert.SerializeObject(value), Encoding.UTF8, mediaType)
		{
		}
	}
}
