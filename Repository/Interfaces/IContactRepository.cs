using System.Threading.Tasks;
using EvolentHealthLib.BusinessData;
using EvolentHealthLib.Messages;

namespace Repository.Interfaces
{
	public interface IContactRepository
	{
		Task<BaseResponse> AddContact(ContactData contact);
		Task<BaseResponse> DeleteContact(ContactData contactData);
		Task<BaseResponse> EditContact(ContactData contactData);
		Task<BaseResponse> GetContacts();
	}
}
