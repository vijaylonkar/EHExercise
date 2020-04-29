using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using EvolentHealthAPICore.Interfaces;
using EvolentHealthAPILib.Messages;
using Repository.Interfaces;

namespace EvolentHealthAPI.Controllers
{
	public class ContactsController : ApiController
	{
		private readonly ILogger logger;
		private readonly ICore core;

		public ContactsController(ICore core, ILogger logger)
		{
			this.logger = logger;
			this.core = core;
		}

		private void CheckInVariants(BaseAPIRequest request, BaseAPIResponse response)
		{
			StringBuilder errorMessageSB = new StringBuilder(1024);

			if (this.logger == null) errorMessageSB.Append("NULL Logger object.");
			if (this.core == null) errorMessageSB.Append("NULL EvolentHealthAPICore object.");
			if (request == null) errorMessageSB.Append("NULL Request object.");

			if (errorMessageSB.Length > 0)
			{
				response.NullObjects(errorMessageSB.ToString());
				if (this.logger != null)
					this.logger.LogErrorMessage(this.GetType(), errorMessageSB.ToString());
			}
			else
			{
				response.Success();
			}
		}

		private void CheckInVariants(BaseAPIResponse response)
		{
			StringBuilder errorMessageSB = new StringBuilder(1024);

			if (this.logger == null) errorMessageSB.Append("NULL Logger object.");
			if (this.core == null) errorMessageSB.Append("NULL EvolentHealthAPICore object.");

			if (errorMessageSB.Length > 0)
			{
				response.NullObjects(errorMessageSB.ToString());
				if (this.logger != null)
					this.logger.LogErrorMessage(this.GetType(), errorMessageSB.ToString());
			}
			else
			{
				response.Success();
			}
		}

		#region API Methods

		[Route("api/Contacts/AddContact")]
		[HttpPost]
		public async Task<AddContactResponse> AddContact(AddContactRequest addContactRequest)
		{
			this.logger.LogInfoMessage(this.GetType(), "Begin AddContact");
			AddContactResponse addContactResponse = new AddContactResponse();

			this.CheckInVariants(addContactRequest, addContactResponse);

			// Validate Request
			if (addContactResponse.ReturnValueInt == 0)
			{
				addContactRequest.Validate(addContactResponse, this.logger);
			}

			if (addContactResponse.ReturnValueInt == 0)
			{
				try
				{
					Task<AddContactResponse> task = this.core.AddContact(addContactRequest);
					await task;

					addContactResponse.ReturnValueInt = task.Result.ReturnValueInt;
					addContactResponse.ReturnValueString = task.Result.ReturnValueString;
					addContactResponse.ReturnValueCustomString = task.Result.ReturnValueCustomString;
					addContactResponse.NewContactID = task.Result.NewContactID;
				}
				catch (Exception e)
				{
					addContactResponse.RunTimeException(e, "Exception while adding contact.");
				}
			}

			if (addContactResponse.ReturnValueInt == 0)
			{
				addContactResponse.Success("Contact added Successfully.");
			}

			this.logger.LogInfoMessage(this.GetType(),
										String.Format("End AddContact:{0}", addContactResponse.ReturnValueInt));

			return addContactResponse;
		}

		[Route("api/Contacts/EditContact")]
		[HttpPost]
		public async Task<EditContactResponse> EditContact(EditContactRequest editContactRequest)
		{
			this.logger.LogInfoMessage(this.GetType(), "Begin EditContact");
			EditContactResponse editContactResponse = new EditContactResponse();

			this.CheckInVariants(editContactRequest, editContactResponse);

			// Validate Request
			if (editContactResponse.ReturnValueInt == 0)
			{
				editContactRequest.Validate(editContactResponse, this.logger);
			}

			if (editContactResponse.ReturnValueInt == 0)
			{
				try
				{
					Task<EditContactResponse> task = this.core.EditContact(editContactRequest);
					await task;

					editContactResponse.ReturnValueInt = task.Result.ReturnValueInt;
					editContactResponse.ReturnValueString = task.Result.ReturnValueString;
					editContactResponse.ReturnValueCustomString = task.Result.ReturnValueCustomString;
				}
				catch (Exception e)
				{
					editContactResponse.RunTimeException(e, "Exception while Editing contact.");
				}
			}

			if (editContactResponse.ReturnValueInt == 0)
			{
				editContactResponse.Success("Contact Edited Successfully.");
			}

			this.logger.LogInfoMessage(this.GetType(),
										String.Format("End EditContact:{0}", editContactResponse.ReturnValueInt));

			return editContactResponse;
		}

		[Route("api/Contacts/DeleteContact")]
		[HttpPost]
		public async Task<DeleteContactResponse> DeleteContact(DeleteContactRequest deleteContactRequest)
		{
			this.logger.LogInfoMessage(this.GetType(), "Begin DeleteContact");
			DeleteContactResponse deleteContactResponse = new DeleteContactResponse();

			this.CheckInVariants(deleteContactRequest, deleteContactResponse);

			// Validate Request
			if (deleteContactResponse.ReturnValueInt == 0)
			{
				deleteContactRequest.Validate(deleteContactResponse, this.logger);
			}

			if (deleteContactResponse.ReturnValueInt == 0)
			{
				try
				{
					Task<DeleteContactResponse> task = this.core.DeleteContact(deleteContactRequest);
					await task;

					deleteContactResponse.ReturnValueInt = task.Result.ReturnValueInt;
					deleteContactResponse.ReturnValueString = task.Result.ReturnValueString;
					deleteContactResponse.ReturnValueCustomString = task.Result.ReturnValueCustomString;
				}
				catch (Exception e)
				{
					deleteContactResponse.RunTimeException(e, "Exception while Deleting contact.");
				}
			}

			if (deleteContactResponse.ReturnValueInt == 0)
			{
				deleteContactResponse.Success("Contact Deleted Successfully.");
			}

			this.logger.LogInfoMessage(this.GetType(),
										String.Format("End DeleteContact:{0}", deleteContactResponse.ReturnValueInt));

			return deleteContactResponse;
		}

		[Route("api/Contacts/GetContacts")]
		[HttpGet]
		public async Task<GetContactsResponse> GetContacts(GetContactsRequest getContactsRequest)
		{
			this.logger.LogInfoMessage(this.GetType(), "Begin GetContacts");
			GetContactsResponse getContactsResponse = new GetContactsResponse();

			this.CheckInVariants(getContactsResponse);
			getContactsRequest = new GetContactsRequest();
			// Validate Request
			if (getContactsResponse.ReturnValueInt == 0)
			{
				getContactsRequest.Validate(getContactsResponse, this.logger);
			}

			if (getContactsResponse.ReturnValueInt == 0)
			{
				try
				{
					Task<GetContactsResponse> task = this.core.GetContacts(getContactsRequest);
					await task;

					getContactsResponse.ContactDataList = task.Result.ContactDataList;
					getContactsResponse.ReturnValueInt = task.Result.ReturnValueInt;
					getContactsResponse.ReturnValueString = task.Result.ReturnValueString;
					getContactsResponse.ReturnValueCustomString = task.Result.ReturnValueCustomString;
				}
				catch (Exception e)
				{
					getContactsResponse.RunTimeException(e, "Exception while Getting contacts.");
				}
			}

			if (getContactsResponse.ReturnValueInt == 0)
			{
				getContactsResponse.Success("Contact Get Successful.");
			}

			this.logger.LogInfoMessage(this.GetType(),
										String.Format("End GetContacts:{0}", getContactsResponse.ReturnValueInt));

			return getContactsResponse;
		}

		#endregion
	}
}