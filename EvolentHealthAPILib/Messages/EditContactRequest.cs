using System;
using System.Runtime.Serialization;
using System.Text;
using Repository.Interfaces;

namespace EvolentHealthAPILib.Messages
{
	[DataContract(Name = "ECReq")]
	public class EditContactRequest : BaseAPIRequest
	{
		[DataMember(Name = "id")]
		public Int64 ContactID { get; set; }
		[DataMember(Name = "fn")]
		public string FirstName { get; set; }
		[DataMember(Name = "ln")]
		public string LastName { get; set; }
		[DataMember(Name = "mid")]
		public string EmailAddress { get; set; }
		[DataMember(Name = "pn")]
		public string PhoneNumber { get; set; }

		public void Validate(EditContactResponse response, ILogger logger)
		{
			logger.LogInfoMessage(this.GetType(), "Begin Validate");

			try
			{
				StringBuilder validationString = new StringBuilder(1024);
				//
				// Make sure manadatory fields are not empty
				//
				if (this.ContactID <= 0)
					validationString.Append("Invalid Contact ID.");
				if (String.IsNullOrWhiteSpace(this.FirstName))
					validationString.Append("FirstName is empty.");
				if (!new CustomValidator().IsValidEmail(this.EmailAddress))
					validationString.Append("InValid Email Address.");
				//
				// Other Validations
				//
				if (validationString.Length > 0)
				{
					response.InvalidRequest(validationString.ToString());
					logger.LogErrorMessage(this.GetType(), validationString.ToString());
				}
			}
			catch (Exception ex)
			{
				response.RunTimeException(ex, "Exception in Validate");
				logger.LogErrorMessage(this.GetType(), "Exception in Validate", ex);
			}

			logger.LogInfoMessage(this.GetType(), "End Validate");
		}
	}
}
