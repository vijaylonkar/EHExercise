using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolentHealthAPICore.BusinessLogic.Interfaces;
using EvolentHealthAPICore.Factories;
using EvolentHealthAPICore.Interfaces;
using EvolentHealthAPILib.Messages;
using EvolentHealthLib.BusinessData;
using EvolentHealthLib.Messages;
using Repository;
using Repository.Interfaces;

namespace EvolentHealthAPICore.Services
{
	public class Core : ICore
	{
		private readonly ILogger logger;
		private readonly IContact contact;
		private readonly IContactRepository contactRepository;

		public Core(ILogger logger)
		{
			this.logger = logger;
			string repositoryType = new Settings(this.logger).GetAppSettingsValue("RepositoryType", "");
			var serviceFactory = new ServiceFactory(repositoryType);
			this.contactRepository = serviceFactory.Resolve<IContactRepository>();
			this.contact = serviceFactory.Resolve<IContact>();
		}

		public async Task<AddContactResponse> AddContact(AddContactRequest addContactRequest)
		{
			this.logger.LogInfoMessage(this.GetType(), "Begin AddContact");

			AddContactResponse addContactResponse = new AddContactResponse();

			try
			{
				ContactData cd = this.ConvertContactMessageToObject(addContactRequest);

				BaseResponse response = await this.contact.AddContact(cd);

				addContactResponse.Build(response.ReturnValueInt, response.ReturnValueString, response.ReturnValueCustomString);
				if (response.ReturnValueCustomObject != null)
					addContactResponse.NewContactID = (Int64)response.ReturnValueCustomObject;
			}
			catch (Exception ex)
			{
				addContactResponse.RunTimeException(ex, "Exception while adding contact.");
				this.logger.LogErrorMessage(this.GetType(), "Exception in AddContact", ex);
			}

			this.logger.LogInfoMessage(this.GetType(),
							String.Format("End AddContact:{0}", addContactResponse.ReturnValueInt));

			return addContactResponse;
		}

		public async Task<EditContactResponse> EditContact(EditContactRequest editContactRequest)
		{
			this.logger.LogInfoMessage(this.GetType(), "Begin EditContact");

			EditContactResponse editContactResponse = new EditContactResponse();

			try
			{
				ContactData cd = this.ConvertContactMessageToObject(editContactRequest);

				BaseResponse response = await this.contact.EditContact(cd);

				editContactResponse.Build(response.ReturnValueInt,
											response.ReturnValueString,
											response.ReturnValueCustomString);
			}
			catch (Exception ex)
			{
				editContactResponse.RunTimeException(ex, "Exception while Editing contact.");
				this.logger.LogErrorMessage(this.GetType(), "Exception in EditContact", ex);
			}

			this.logger.LogInfoMessage(this.GetType(),
							String.Format("End EditContact:{0}", editContactResponse.ReturnValueInt));

			return editContactResponse;
		}

		public async Task<DeleteContactResponse> DeleteContact(DeleteContactRequest deleteContactRequest)
		{
			this.logger.LogInfoMessage(this.GetType(), "Begin DeleteContact");

			DeleteContactResponse deleteContactResponse = new DeleteContactResponse();

			try
			{
				ContactData cd = this.ConvertContactMessageToObject(deleteContactRequest);

				BaseResponse response = await this.contact.DeleteContact(cd);

				deleteContactResponse.Build(response.ReturnValueInt,
											response.ReturnValueString,
											response.ReturnValueCustomString);
			}
			catch (Exception ex)
			{
				deleteContactResponse.RunTimeException(ex, "Exception while Deleting contact.");
				this.logger.LogErrorMessage(this.GetType(), "Exception in DeleteContact", ex);
			}

			this.logger.LogInfoMessage(this.GetType(),
							String.Format("End DeleteContact:{0}", deleteContactResponse.ReturnValueInt));

			return deleteContactResponse;
		}

		public async Task<GetContactsResponse> GetContacts(GetContactsRequest getContactsRequest)
		{
			this.logger.LogInfoMessage(this.GetType(), "Begin GetContacts");

			GetContactsResponse getContactsResponse = new GetContactsResponse();

			try
			{
				BaseResponse response = await this.contact.GetContacts();

				getContactsResponse.ContactDataList = (List<ContactData>)response.ReturnValueCustomObject;
				getContactsResponse.Build(response.ReturnValueInt,
											response.ReturnValueString,
											response.ReturnValueCustomString);
			}
			catch (Exception ex)
			{
				getContactsResponse.RunTimeException(ex, "Exception while Getting contacts.");
				this.logger.LogErrorMessage(this.GetType(), "Exception in GetContacts", ex);
			}

			this.logger.LogInfoMessage(this.GetType(),
							String.Format("End GetContacts:{0}", getContactsResponse.ReturnValueInt));

			return getContactsResponse;
		}

		private ContactData ConvertContactMessageToObject(AddContactRequest addContactRequest)
		{
			ContactData c = new ContactData();

			c.FirstName = addContactRequest.FirstName;
			c.LastName = addContactRequest.LastName;
			c.EmailAddress = addContactRequest.EmailAddress;
			c.PhoneNumber = addContactRequest.PhoneNumber;
			c.Status = 1;

			return c;
		}

		private ContactData ConvertContactMessageToObject(EditContactRequest editContactRequest)
		{
			ContactData c = new ContactData();

			c.ContactID = editContactRequest.ContactID;
			c.FirstName = editContactRequest.FirstName;
			c.LastName = editContactRequest.LastName;
			c.EmailAddress = editContactRequest.EmailAddress;
			c.PhoneNumber = editContactRequest.PhoneNumber;
			c.Status = 1;

			return c;
		}

		private ContactData ConvertContactMessageToObject(DeleteContactRequest deleteContactRequest)
		{
			ContactData c = new ContactData();

			c.ContactID = deleteContactRequest.ContactID;

			return c;
		}
	}
}
