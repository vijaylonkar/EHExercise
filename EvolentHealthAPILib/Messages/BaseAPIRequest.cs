using System.Runtime.Serialization;

namespace EvolentHealthAPILib.Messages
{
	[DataContract]
	public class BaseAPIRequest
	{
		[DataMember(Name = "AVN")]
		public string ApiVersion { get; set; }
		/// <summary>
		/// Platform type values can be as follows
		///  Default         = 0
		///  Windows         = 1
		///  Linux           = 2
		///  Mobile Devices  = 4
		///  Mac             = 8
		/// </summary>
		[DataMember(Name = "PL")]
		public short PlatformType { get; set; }
	}
}
