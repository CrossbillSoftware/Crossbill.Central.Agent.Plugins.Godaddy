using Crossbill.Central.Agent.Plugins.Godaddy.Models;
using Crossbill.Common;
using Crossbill.Common.Processors;
using Crossbill.Common.Utils;
using Crossbill.Common.Validation;
using Crossbill.Install.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;

namespace Crossbill.Central.Agent.Plugins.Godaddy
{
	[Export(typeof(IBaseAdapter))]
	public class GodaddyDNSAdapter : BaseAdapter
	{
		public override string GetTargetAppType()
		{
			return "GodaddyDNS";
		}

		public override List<string> Install(ContextSettings settings)
		{
			string accessKey, secretKey, domain, aRecords, ip;
			if (!settings.Parameters.TryGetValue("AccessKey", out accessKey) || String.IsNullOrEmpty(accessKey))
			{
				throw new Exception("AccessKey parameter is required for GodaddyDNS adapter.");
			}
			if (!settings.Parameters.TryGetValue("SecretKey", out secretKey) || String.IsNullOrEmpty(secretKey))
			{
				throw new Exception("SecretKey parameter is required for GodaddyDNS adapter.");
			}
			if (!settings.Parameters.TryGetValue("Domain", out domain) || String.IsNullOrEmpty(domain))
			{
				throw new Exception("Domain parameter is required for GodaddyDNS adapter.");
			}
			if (!settings.Parameters.TryGetValue("ARecords", out aRecords) || String.IsNullOrEmpty(aRecords))
			{
				throw new Exception("ARecords parameter is required for GodaddyDNS adapter.");
			}
			if (!settings.Parameters.TryGetValue("IP", out ip) || String.IsNullOrEmpty(ip))
			{
				throw new Exception("IP parameter is required for GodaddyDNS adapter.");
			}

			GodaddyClient client = new GodaddyClient(accessKey, secretKey, null);

			var domains = client.DomainRecords.List();

			var records = new List<DnsRecord>();
			string[] recs = aRecords.Split(',', ';');
			foreach (string rec in recs)
			{
				records.Add(new DnsRecord() { 
					name = rec,
					data = ip,
					priority = 0,
					ttl = 600,
					type = "A", 
					service = "",
					weight = 0,
					port = 80, 
					protocol = "http"
				});
			}
			client.DomainRecords.PatchRecords(domain, records);

			return null;
		}

		public override void Uninstall(ContextSettings settings, List<string> files)
		{
		}
	}
}
