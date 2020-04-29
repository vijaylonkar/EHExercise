using System;
using System.Runtime.Serialization;

namespace EvolentHealthLib.BusinessData
{
	[DataContract]
	public class ContactData
	{
		[DataMember(Name ="id")]
		public Int64 ContactID { get; set; }
		[DataMember(Name = "fn")]
		public string FirstName { get; set; }
		[DataMember(Name = "ln")]
		public string LastName { get; set; }
		[DataMember(Name = "mid")]
		public string EmailAddress { get; set; }
		[DataMember(Name = "pn")]
		public string PhoneNumber { get; set; }
		[DataMember(Name = "st")]
		public byte Status { get; set; }

		public ContactData()
		{

		}
		public ContactData(Int64 id, string firstName, string lastName, string emailAddress, string phoneNumber, byte status)
		{
			this.ContactID = id;
			this.FirstName = firstName;
			this.LastName = lastName;
			this.EmailAddress = emailAddress;
			this.PhoneNumber = phoneNumber;
			this.Status = status;
		}
	}
}
