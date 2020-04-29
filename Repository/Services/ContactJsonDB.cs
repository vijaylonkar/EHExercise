using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolentHealthLib.BusinessData;
using EvolentHealthLib.Messages;
using Newtonsoft.Json;
using Repository.Interfaces;

namespace Repository.Services
{
	public class ContactJsonDB : JsonDB, IContactRepository
	{
		public ContactJsonDB(ILogger logger) : base(logger)
		{
		}

		public async Task<BaseResponse> AddContact(ContactData contact)
		{
			this.logger.LogInfoMessage(this.GetType(), "Begin AddContact");
			BaseResponse response = new BaseResponse();

			try
			{
				response = await this.Add("contacts", JsonConvert.SerializeObject(contact));
			}
			catch (Exception ex)
			{
				response.RunTimeException(ex, "Exception in AddContact");
				this.logger.LogErrorMessage(this.GetType(), "Exception in AddContact", ex);
			}

			this.logger.LogInfoMessage(this.GetType(),
							String.Format("End AddContact:{0}", response.ReturnValueInt));

			return response;
		}

		public async Task<BaseResponse> EditContact(ContactData contactData)
		{
			this.logger.LogInfoMessage(this.GetType(), "Begin EditContact");

			BaseResponse response = new BaseResponse();
			try
			{
				response = await this.Edit("contacts", JsonConvert.SerializeObject(contactData));
			}
			catch (Exception ex)
			{
				response.RunTimeException(ex, "Exception in EditContact");
				this.logger.LogErrorMessage(this.GetType(), "Exception in EditContact", ex);
			}

			this.logger.LogInfoMessage(this.GetType(), String.Format("End EditContact:{0}", response.ReturnValueInt));

			return response;
		}

		public async Task<BaseResponse> DeleteContact(ContactData contactData)
		{
			this.logger.LogInfoMessage(this.GetType(), "Begin DeleteContact");

			BaseResponse response = new BaseResponse();
			try
			{
				response = await this.Delete("contacts", JsonConvert.SerializeObject(contactData));
			}
			catch (Exception ex)
			{
				response.RunTimeException(ex, "Exception in DeleteContact");
				this.logger.LogErrorMessage(this.GetType(), "Exception in DeleteContact", ex);
			}

			this.logger.LogInfoMessage(this.GetType(), String.Format("End DeleteContact:{0}", response.ReturnValueInt));

			return response;
		}

		public async Task<BaseResponse> GetContacts()
		{
			this.logger.LogInfoMessage(this.GetType(), "Begin GetContacts");

			BaseResponse response = new BaseResponse();
			try
			{
				response = await this.Get("contacts");
				response.ReturnValueCustomObject = JsonConvert.DeserializeObject<List<ContactData>>(response.ReturnValueCustomObject.ToString());
			}
			catch (Exception ex)
			{
				response.RunTimeException(ex, "Exception in GetContacts");
				this.logger.LogErrorMessage(this.GetType(), "Exception in GetContacts", ex);
			}

			this.logger.LogInfoMessage(this.GetType(), String.Format("End GetContacts:{0}", response.ReturnValueInt));

			return response;
		}
	}
}
