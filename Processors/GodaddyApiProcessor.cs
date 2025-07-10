using Crossbill.Common.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Crossbill.Central.Agent.Plugins.Godaddy.Processors
{
	public class GodaddyApiProcessor : ApiProcessor
	{
		protected string _accessKey;
		protected string _secretKey;

		public GodaddyApiProcessor(string accessKey, string secretKey) : base()
		{
			_accessKey = accessKey;
			_secretKey = secretKey;
		}

		protected override HttpClient GetClient(string token, HttpClientHandler handler)
		{
			var client = base.GetClient(token, handler);

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("sso-key", String.Format("{0}:{1}", _accessKey, _secretKey));

			return client;
		}

		public Exception FormatError(string message, string apiUrl)
		{
			return FormatError(message, apiUrl, null);
		}
	}
}
