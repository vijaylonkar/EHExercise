using System;
using System.Runtime.Serialization;
using System.Text;
using Repository.Interfaces;

namespace EvolentHealthAPILib.Messages
{
	[DataContract(Name = "DCReq")]
	public class DeleteContactRequest : BaseAPIRequest
	{
		[DataMember(Name = "id")]
		public int ContactID { get; set; }

		public void Validate(DeleteContactResponse response, ILogger logger)
		{
			logger.LogInfoMessage(this.GetType(), "Begin Validate");

			try
			{
				StringBuilder validationString = new StringBuilder(1024);
				//
				// Make sure manadatory fields are not empty
				//

				if (this.ContactID <= 0)
					validationString.Append("InValid Contact ID.");
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
