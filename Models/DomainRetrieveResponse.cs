using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossbill.Central.Agent.Plugins.Godaddy.Models
{
	public class DomainRetrieveResponse
	{
		public DateTime CreatedAt { get; set; }
		public string Domain { get; set; }
		public int DomainId { get; set; }
		public bool ExpirationProtected { get; set; }
		public DateTime Expires { get; set; }
		public bool ExposeWhois { get; set; }
		public bool HoldRegistrar { get; set; }
		public bool Locked { get; set; }
		public string[] NameServers { get; set; }
		public bool Privacy { get; set; }
		public bool RenewAuto { get; set; }
		public DateTime RenewDeadline { get; set; }
		public bool Renewable { get; set; }
		public string Status { get; set; }
		public bool TransferProtected { get; set; }
	}
}
