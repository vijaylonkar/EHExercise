using System;
using System.Runtime.Serialization;

namespace EvolentHealthAPILib.Messages
{
	public class AddContactResponse : BaseAPIResponse
	{
		[DataMember(Name = "nid")]
		public Int64 NewContactID { get; set; }
		public AddContactResponse()
		{
		}
	}
}