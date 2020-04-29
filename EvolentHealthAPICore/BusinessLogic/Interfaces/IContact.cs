using System.Collections.Generic;
using System.Threading.Tasks;
using EvolentHealthLib.BusinessData;
using EvolentHealthLib.Messages;

namespace EvolentHealthAPICore.BusinessLogic.Interfaces
{
	public interface IContact
	{
		Task<BaseResponse> AddContact(ContactData contactData);
		Task<BaseResponse> DeleteContact(ContactData contactData);
		Task<BaseResponse> EditContact(ContactData contactData);
		Task<BaseResponse> GetContacts();
		int GetContacts(string sortBy, string sortOrder, int pageSize, long pageIndex, out List<ContactData> contactDataList);
	}
}