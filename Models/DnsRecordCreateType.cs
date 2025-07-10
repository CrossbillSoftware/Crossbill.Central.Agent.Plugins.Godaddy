using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossbill.Central.Agent.Plugins.Godaddy.Models
{
	public class DnsRecordCreateType
	{
		[Required]
		public string name { get; set; }

		[Required]
		public string data { get; set; }

		[Range(0, int.MaxValue)]
		public int priority { get; set; }

		[Range(0, int.MaxValue)]
		public int ttl { get; set; }

		public string service { get; set; }

		public string protocol { get; set; }

		[Range(1, 65535)]
		public int port { get; set; }

		[Range(0, int.MaxValue)]
		public int weight { get; set; }
	}
}
