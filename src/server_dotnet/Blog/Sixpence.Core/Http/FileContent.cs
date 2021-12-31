using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace Sixpence.Common.Http
{
	public class FileContent : MultipartFormDataContent
	{
		public FileContent(string filePath, string apiParamName)
		{
			FileStream content = File.Open(filePath, FileMode.Open);
			string fileName = Path.GetFileName(filePath);
			Add(new StreamContent(content), apiParamName, fileName);
		}
	}
}
