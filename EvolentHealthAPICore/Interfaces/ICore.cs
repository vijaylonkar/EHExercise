using System.Threading.Tasks;
using EvolentHealthAPILib.Messages;

namespace EvolentHealthAPICore.Interfaces
{
	public interface ICore
	{
		Task<GetContactsResponse> GetContacts(GetContactsRequest getContactsRequest);
		Task<AddContactResponse> AddContact(AddContactRequest addContactRequest);
		Task<EditContactResponse> EditContact(EditContactRequest editContactRequest);
		Task<DeleteContactResponse> DeleteContact(DeleteContactRequest deleteContactRequest);
	}
}