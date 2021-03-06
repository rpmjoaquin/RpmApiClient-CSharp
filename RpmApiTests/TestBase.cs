﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using RPM.Api;

namespace RpmApiTests
{
	[TestClass]
	public class TestBase
	{
		private Client _client;
		protected Client getApiClient()
		{
			if (_client == null)
			{
				_client = new Client(ApiSettings.url, ApiSettings.key, new MockResponseRestClient(ApiSettings.url + "Api2.svc"));
			}
			return _client;
		}
	}
}
