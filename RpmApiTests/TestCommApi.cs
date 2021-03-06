﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using RPM.Api;
using RPM.Api.Response;

namespace RpmApiTests
{
	/// <summary>
	/// Test API endpoints that begin with Comm.
	/// </summary>
	[TestClass]
	public class TestCommApi : TestBase
	{
		[TestMethod]
		public void TestCommAccounts()
		{
			Client client = getApiClient();
			List<CommAccountResponse> accounts = client.CommAccounts(Client.Var.Referral, Client.Run._this);
		}
	}
}
