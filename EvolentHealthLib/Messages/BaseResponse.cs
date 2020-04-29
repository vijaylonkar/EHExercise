using System;
using System.Runtime.Serialization;

namespace EvolentHealthLib.Messages
{
	[DataContract]
	public class BaseResponse
	{
		#region Response Values

		public const int RESPONSE_SUCCESS = 0;

		public const int RESPONSE_INITIALIZE = -1;

		public const int RESPONSE_NULLOBJECTS = -11;
		public const int RESPONSE_INVALIDREQUEST = -12;
		public const int RESPONSE_RUNTIMEEXCEPTION = -13;

		#endregion

		public BaseResponse()
		{
			this.ReturnValueInt = RESPONSE_INITIALIZE;
			this.ReturnValueString = String.Empty;
			this.ReturnValueCustomString = String.Empty;
		}
		public void Success()
		{
			this.ReturnValueInt = RESPONSE_SUCCESS;
			this.ReturnValueString = String.Empty;
			this.ReturnValueCustomString = String.Empty;
		}
		public void Success(object customObject)
		{
			this.ReturnValueInt = RESPONSE_SUCCESS;
			this.ReturnValueString = String.Empty;
			this.ReturnValueCustomString = String.Empty;
			this.ReturnValueCustomObject = customObject;
		}
		public void Success(string customString)
		{
			this.ReturnValueInt = RESPONSE_SUCCESS;
			this.ReturnValueString = String.Empty;
			this.ReturnValueCustomString = customString;
		}
		public void RunTimeException(Exception e, string customString)
		{
			this.ReturnValueInt = RESPONSE_RUNTIMEEXCEPTION;
			this.ReturnValueString = e.Message;
			this.ReturnValueCustomString = customString;
		}

		[DataMember(Name = "RVI")]
		public int ReturnValueInt { get; set; } // return value int

		[DataMember(Name = "RVS")]
		public string ReturnValueString { get; set; } // return value string

		[DataMember(Name = "RCS")]
		public string ReturnValueCustomString { get; set; } // return message: custom message

		[DataMember(Name = "RCO")]
		public object ReturnValueCustomObject { get; set; } // return Custom Object
	}
}
