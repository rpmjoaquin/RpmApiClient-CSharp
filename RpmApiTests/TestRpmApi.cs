﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using RPM.Api;
using RPM.Api.Response;

namespace RpmApiTests
{
	[TestClass]
	public class TestRpmApi : TestBase
	{
		[TestMethod]
		public void TestIncorrectApiURL()
		{
			try
			{
				Client client = new Client("badurl", ApiSettings.key);
				Assert.Fail("Expected UriFormatException");
			}
			catch (Exception e)
			{
				Assert.AreEqual(e.GetType().Name, "UriFormatException");
			}
		}

		[TestMethod]
		public void TestIncorrectApiKey()
		{
			Client client = new Client(ApiSettings.url, "badkey");
			try
			{
				InfoResponse info = client.Info();
				Assert.Fail("Expected RPMApiError Exception");
			}
			catch (RPMApiError e)
			{
				Assert.AreEqual(e.Message, "Valid key required");
			}
		}

		[TestMethod]
		public void TestInfo()
		{
			Client client = getApiClient();
			InfoResponse info = client.Info();
			Assert.IsNotNull(info.Role);
			Assert.IsNotNull(info.Subscriber);
			Assert.IsNotNull(info.User);
			Assert.IsNotNull(info.RPM);
		}

		[TestMethod]
		public void TestAccount()
		{
			AccountResponse firstAccount = getFirstAccount();
			Client client = getApiClient();
			AccountResponse account = client.Account(firstAccount.Supplier, firstAccount.Account);

			// getFirstAccount uses the Accounts API call which does not return Reps
			firstAccount.Reps = account.Reps;
			Assert.IsTrue(firstAccount.Equals(account));
		}

		private AccountResponse getFirstAccount(SupplierResponse supplier = null)
		{
			if (supplier == null)
			{
				supplier = this.getFirstSupplier();	
			}

			Client client = getApiClient();
			List<AccountResponse> accounts = client.AccountsForSupplier(supplier.Supplier);

			if (accounts.Count == 0)
			{
				return null;
			}
			return accounts[0];
		}

		[TestMethod]
		public void TestAccounts()
		{
			SupplierResponse supplier = this.getFirstSupplier();
			AccountResponse account = getFirstAccount(supplier);
			if (account == null)
			{
				Assert.Inconclusive("No Accounts found for Supplier " + supplier.Supplier);
				return;
			}
			Assert.AreEqual(account.Supplier, supplier.Supplier);
			Assert.AreEqual(account.SupplierID, supplier.SupplierID);
		}

		[TestMethod]
		public void TestCustomer()
		{
			SupplierResponse supplier = this.getFirstSupplier();
			AccountResponse account = getFirstAccount(supplier);

			CustomerResponse customerByID = getApiClient().Customer(account.CustomerID);
			CustomerResponse customerByName = getApiClient().Customer(account.Customer);

			Assert.IsTrue(customerByID.Equals(customerByName));
		}

		[TestMethod]
		public void TestAgencies()
		{
			Client client = getApiClient();
			List<AgencyResponse> agencies = client.Agencies();
			AgencyResponse agency = agencies[0];
			Assert.IsNotNull(agency.Agency);
			Assert.IsTrue(agency.AgencyID > 0);
		}

		[TestMethod]
		public void TestAgency()
		{
			Client client = getApiClient();
			List<AgencyResponse> agencies = client.Agencies();
			AgencyResponse agency0 = agencies[0];

			AgencyResponse agency = client.Agency(agency0.AgencyID);
			
			// The Agencies call only returns these 2 fields
			Assert.AreEqual(agency0.Agency, agency.Agency);
			Assert.AreEqual(agency0.AgencyID, agency.AgencyID);
		}

		

		[TestMethod]
		public void TestSuppliers()
		{
			SupplierResponse supplier = getFirstSupplier();
			Assert.IsNotNull(supplier.Supplier);
			Assert.IsTrue(supplier.SupplierID > 0);
		}

		private SupplierResponse getFirstSupplier()
		{
			Client client = getApiClient();
			List<SupplierResponse> suppliers = client.Suppliers();

			return suppliers[0];
		}
	}
}
