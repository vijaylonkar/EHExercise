using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolentHealthAPICore.BusinessLogic.Interfaces;
using EvolentHealthLib.BusinessData;
using EvolentHealthLib.Messages;
using Repository.Interfaces;

namespace EvolentHealthAPICore.BusinessLogic.Services
{
	public class Contact : IContact
	{
		private readonly ILogger logger;
		private readonly IContactRepository contactRepository;

		public Contact(IContactRepository contactRepository, ILogger logger)
		{
			this.logger = logger;
			this.contactRepository = contactRepository;
		}

		public async Task<BaseResponse> GetContacts()
		{
			return await this.contactRepository.GetContacts();
		}
		public int GetContacts(string sortBy,
								string sortOrder,
								Int32 pageSize,
								Int64 pageIndex,
								out List<ContactData> contactDataList)
		{
			int status = -1;

			contactDataList = null;

			return status;
		}

		public async Task<BaseResponse> AddContact(ContactData contactData)
		{
			return await this.contactRepository.AddContact(contactData);
		}

		public async Task<BaseResponse> EditContact(ContactData contactData)
		{
			return await this.contactRepository.EditContact(contactData);
		}

		public async Task<BaseResponse> DeleteContact(ContactData contactData)
		{
			return await this.contactRepository.DeleteContact(contactData);
		}
	}
}
