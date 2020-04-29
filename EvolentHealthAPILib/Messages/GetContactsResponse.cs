using System.Collections.Generic;
using System.Runtime.Serialization;
using EvolentHealthLib.BusinessData;

namespace EvolentHealthAPILib.Messages
{
	public class GetContactsResponse : BaseAPIResponse
	{
		public GetContactsResponse()
		{
		}

		[DataMember(Name = "lstcd")]
		public List<ContactData> ContactDataList { get; set; }

	}
}
