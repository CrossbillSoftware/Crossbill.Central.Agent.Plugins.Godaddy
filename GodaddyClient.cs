using Crossbill.Central.Agent.Plugins.Godaddy.Models;
using Crossbill.Central.Agent.Plugins.Godaddy.Processors;
using Crossbill.Common.Processors;
using System;
using System.Collections.Generic;
using System.Net;

namespace Crossbill.Central.Agent.Plugins.Godaddy
{
	public class GodaddyClient
	{
		protected string _apiUrl;
		public string ApiUrl
		{
			get { return _apiUrl; }
			set { _apiUrl = value; }
		}

		protected string _accessKey;
		protected string _secretKey;

		protected bool _isIgnoreErrors;
		public bool IsIgnoreErrors
		{
			get { return _isIgnoreErrors; }
			set { _isIgnoreErrors = value; }
		}

		public GodaddyClient()
		{
		}

		public GodaddyClient(string accessKey, string secretKey, IWebProxy proxy, string apiUrl = null, bool isIgnoreErrors = false, bool isInitialize = true)
		{
			_apiUrl = apiUrl ?? "https://api.godaddy.com"; // "https://api.ote-godaddy.com";
			_accessKey = accessKey;
			_secretKey = secretKey;
			_isIgnoreErrors = isIgnoreErrors;

			if (isInitialize)
			{
				Initialize(proxy);
			}
		}

		public virtual ApiProcessor Initialize(IWebProxy proxy)
		{
			GodaddyApiProcessor svc = GetApiProcessor(proxy);
			this.DomainRecords = new DomainRecordsFacade(_apiUrl, _isIgnoreErrors, svc);
			return svc;
		}

		public virtual GodaddyApiProcessor GetApiProcessor(IWebProxy proxy)
		{
			GodaddyApiProcessor processor = new GodaddyApiProcessor(_accessKey, _secretKey);
			processor.Proxy = proxy;
			return processor;
		}

		public virtual DomainRecordsFacade DomainRecords { get; protected set; }
	}

	public partial class DomainRecordsFacade
	{
		protected string _apiUrl;
		protected bool _isIgnoreErrors;
		protected GodaddyApiProcessor _svc;
		public DomainRecordsFacade(string apiUrl, bool isIgnoreErrors, GodaddyApiProcessor svc)
		{
			_apiUrl = apiUrl;
			_isIgnoreErrors = isIgnoreErrors;
			_svc = svc;
		}

		public virtual List<DomainRetrieveResponse> List()
		{
			return _svc.Call<string, List<DomainRetrieveResponse>>(null, String.Format("{0}/v1/domains", _apiUrl), HttpVerb.Get, _isIgnoreErrors, null);
		}

		public virtual bool PatchRecords(string domain, List<DnsRecord> records)
		{
			RecordsPatchResponse response = _svc.Call<List<DnsRecord>, RecordsPatchResponse>(null, String.Format("{0}/v1/domains/{1}/records", _apiUrl, domain), HttpVerb.Patch, _isIgnoreErrors, records);
			if (response != null && response.code != "200")
			{
				if (!_isIgnoreErrors)
				{
					throw _svc.FormatError(response.message, _apiUrl);
				}
				return false;
			}
			return true;
		}
	}
}
