using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossbill.Central.Agent.Plugins.Godaddy.Models
{
	public class DnsRecord : DnsRecordCreateType
	{
		[Required]
		public string type { get; set; }
	}
}
