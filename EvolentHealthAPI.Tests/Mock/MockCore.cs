using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EvolentHealthAPICore.BusinessLogic.Interfaces;
using EvolentHealthAPICore.Factories;
using EvolentHealthAPICore.Interfaces;
using EvolentHealthAPILib.Messages;
using EvolentHealthLib.BusinessData;
using Repository.Interfaces;

namespace EvolentHealthAPI.Tests.Mock
{
	public class MockCore : ICore
	{

		private List<ContactData> contactDataList;
		public MockCore()
		{
			this.contactDataList = new List<ContactData>();

			contactDataList.Add(new ContactData(1, "vijay1", "lonkar1", "v.l1@a.in", "+91 111.111.1111", 1));
			contactDataList.Add(new ContactData(2, "vijay2", "lonkar2", "v.l2@a.in", "+91 211.111.1111", 0));
			contactDataList.Add(new ContactData(3, "vijay3", "", "v.l3@a.in", "+91 311.111.1111", 1));
			contactDataList.Add(new ContactData(4, "vijay4", "lonkar4", "v.l4@a.in", "+91 411.111.1111", 1));
			contactDataList.Add(new ContactData(5, "vijay5", "lonkar5", "v.l5@a.in", "+91 511.111.1111", 1));
		}

		public Task<GetContactsResponse> GetContacts(GetContactsRequest getContactsRequest)
		{
			return Task.Run(() =>
			{
				GetContactsResponse response = new GetContactsResponse();

				response.ContactDataList = contactDataList.FindAll(c => c.Status == 1);
				response.HTTPReturnCode = (Int16)HttpStatusCode.OK;
				response.APIVersion = "1.0.0.0";
				response.ReturnValueInt = 0;
				response.ReturnValueString = "";
				response.ReturnValueCustomString = "Contact Get Successfully.";
				response.ReturnValueCustomObject = null;

				return response;
			});
		}

		public Task<AddContactResponse> AddContact(AddContactRequest addContactRequest)
		{
			return Task.Run(() =>
			{
				AddContactResponse response = new AddContactResponse();

				if (this.contactDataList.Exists(c => c.EmailAddress == addContactRequest.EmailAddress))
				{
					response.NewContactID = 0;
					response.ReturnValueInt = -13;
					response.ReturnValueString = "Email Address already Exists";
					response.ReturnValueCustomString = "Exception in Add";
				}
				else
				{
					this.contactDataList.Add(new ContactData(6,
															addContactRequest.FirstName,
															addContactRequest.LastName,
															addContactRequest.EmailAddress,
															addContactRequest.PhoneNumber,
															1));
					response.NewContactID = 6;
					response.ReturnValueInt = 0;
					response.ReturnValueString = "";
					response.ReturnValueCustomString = "Contact added Successfully.";
				}

				response.HTTPReturnCode = (Int16)HttpStatusCode.OK;
				response.APIVersion = "1.0.0.0";
				response.ReturnValueCustomObject = null;

				return response;
			});
		}

		public Task<EditContactResponse> EditContact(EditContactRequest editContactRequest)
		{
			return Task.Run(() =>
			{
				EditContactResponse response = new EditContactResponse();

				ContactData editContactData = this.contactDataList.Find(c => c.ContactID == editContactRequest.ContactID);

				if (editContactData != null)
				{
					editContactData.FirstName = editContactRequest.FirstName;
					editContactData.LastName = editContactRequest.LastName;
					editContactData.EmailAddress = editContactRequest.EmailAddress;
					editContactData.PhoneNumber = editContactRequest.PhoneNumber;

					response.ReturnValueInt = 0;
					response.ReturnValueString = "";
					response.ReturnValueCustomString = "Contact Edited Successfully.";
				}
				else
				{
					response.ReturnValueInt = -13;
					response.ReturnValueString = "Row not found";
					response.ReturnValueCustomString = "Exception in Edit";
				}

				response.HTTPReturnCode = (Int16)HttpStatusCode.OK;
				response.APIVersion = "1.0.0.0";
				response.ReturnValueCustomObject = null;

				return response;
			});
		}

		public Task<DeleteContactResponse> DeleteContact(DeleteContactRequest deleteContactRequest)
		{
			return Task.Run(() =>
			{
				DeleteContactResponse response = new DeleteContactResponse();

				ContactData editContactData =
					this.contactDataList.Find(c => c.ContactID == deleteContactRequest.ContactID && c.Status == 1);

				if (editContactData != null)
				{
					editContactData.Status = 0;

					response.ReturnValueInt = 0;
					response.ReturnValueString = "";
					response.ReturnValueCustomString = "Contact Edited Successfully.";
				}
				else
				{
					response.ReturnValueInt = -13;
					response.ReturnValueString = "Row not found";
					response.ReturnValueCustomString = "Exception in Delete";
				}

				response.HTTPReturnCode = (Int16)HttpStatusCode.OK;
				response.APIVersion = "1.0.0.0";
				response.ReturnValueCustomObject = null;

				return response;
			});
		}
	}
}
