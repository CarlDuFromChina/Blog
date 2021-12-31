using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Sixpence.Common.Http
{
	public class PatchContent : StringContent
	{
		public PatchContent(object value)
			: base(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json-patch+json")
		{
		}
	}
}
