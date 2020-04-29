using System;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using EvolentHealthLib.Messages;

namespace EvolentHealthAPILib.Messages
{
	[DataContract]
	public class BaseAPIResponse : BaseResponse
	{
		[DataMember(Name = "HRC")]
		public Int16 HTTPReturnCode { get; set; } // API return code: HTTP Types

		[DataMember(Name = "AVN")]
		public string APIVersion { get; set; } // API Build version

		public BaseAPIResponse()
		{
			this.APIVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString(); ;
			this.HTTPReturnCode = (Int16)HttpStatusCode.OK;
		}

		public void NullObjects(string customString)
		{
			this.ReturnValueInt = RESPONSE_NULLOBJECTS;
			this.ReturnValueCustomString = customString;
		}
		public void InvalidRequest(string customString)
		{
			this.ReturnValueInt = RESPONSE_INVALIDREQUEST;
			this.ReturnValueCustomString = customString;
		}
		public void Build(int returnValueInt,
							string returnValueString,
							string returnValueCustomString)
		{
			this.ReturnValueInt = returnValueInt;
			this.ReturnValueString = returnValueString;
			this.ReturnValueCustomString = returnValueCustomString;
		}
	}


}
