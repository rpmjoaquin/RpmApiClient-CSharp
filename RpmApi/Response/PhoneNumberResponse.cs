﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class PhoneNumberResponse : Abstract.Response
	{
		/// <summary>
		/// Type of phone numbers allowed by RPM
		/// </summary>
		public enum NumberType
		{
			Business=1, Home, Fax, Other=6
		};
		private string _Number { get; set; }
		/// <summary>
		/// Gets or sets the number.
		/// <remarks>Setting Number to "" will result in having an actual value of "none"</remarks>
		/// </summary>
		/// <value>
		/// The number.
		/// </value>
		public string Number {
			get {
				if (_Number == null || _Number == "")
				{
					_Number = "none";
				}
				return _Number;
			}
			set { _Number = value;  }
		}
		/// <summary>
		/// Gets or sets the phone number identifier (generated by RPM).
		/// </summary>
		/// <value>
		/// The phone number identifier.
		/// </value>
		public int PhoneNumberID { get; set; }
		/// <summary>
		/// Gets or sets the type of phone number.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		public NumberType Type { get; set; }

		/// <summary>
		/// Determines whether the specified <see cref="System.Object" }, is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj)
		{
			PhoneNumberResponse other = (PhoneNumberResponse)obj;
			return
				this.Number == other.Number &&
				this.PhoneNumberID == other.PhoneNumberID &&
				this.Type == other.Type;
		}
	}
}
