using System.Threading.Tasks;
using EvolentHealthAPI.Controllers;
using EvolentHealthAPI.Tests.Mock;
using EvolentHealthAPICore.Interfaces;
using EvolentHealthAPILib.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Interfaces;
using Repository.Services;
using EvolentHealthLib.Messages;

namespace EvolentHealthAPI.Tests.Controllers
{
	[TestClass]
	public class ContactsControllerTest
	{
		#region GetContacts Tests

		[TestMethod]
		public async Task GetContacts_Success()
		{
			// Arrange
			ICore core = new MockCore();
			ILogger logger = new Logger();
			ContactsController controller = new ContactsController(core, logger);
			GetContactsRequest request = new GetContactsRequest();

			// Act
			GetContactsResponse response = await (controller.GetContacts(request));

			// Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(4, response.ContactDataList.Count);
			Assert.AreEqual(response.ReturnValueInt, BaseResponse.RESPONSE_SUCCESS);
			Assert.AreEqual(response.ReturnValueCustomString, "Contact Get Successful.");
		}

		#endregion

		#region AddContact Tests

		[TestMethod]
		public async Task AddContact_Success()
		{
			// Arrange
			ICore core = new MockCore();
			ILogger logger = new Logger();
			ContactsController controller = new ContactsController(core, logger);
			AddContactRequest request = new AddContactRequest();
			request.FirstName = "Vijay6";
			request.LastName = "Lonkar6";
			request.EmailAddress = "v.l6@a.in";
			request.PhoneNumber = "+91 611.111.111";

			// Act
			AddContactResponse response = await (controller.AddContact(request));

			// Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(response.NewContactID, 6);
			Assert.AreEqual(response.ReturnValueInt, BaseResponse.RESPONSE_SUCCESS);
			Assert.AreEqual(response.ReturnValueCustomString, "Contact added Successfully.");
		}

		[TestMethod]
		public async Task AddContact_NullInput()
		{
			// Arrange
			ICore core = new MockCore();
			ILogger logger = new Logger();
			ContactsController controller = new ContactsController(core, logger);

			// Act
			AddContactResponse response = await (controller.AddContact(null));

			// Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(response.ReturnValueInt, BaseResponse.RESPONSE_NULLOBJECTS);
			Assert.AreEqual(response.ReturnValueCustomString, "NULL Request object.");
		}
		
		[TestMethod]
		public async Task AddContact_InValidInput()
		{
			// Arrange
			ICore core = new MockCore();
			ILogger logger = new Logger();
			ContactsController controller = new ContactsController(core, logger);
			AddContactRequest request = new AddContactRequest();
			request.FirstName = "";
			request.LastName = "Lonkar6";
			request.EmailAddress = "v.l4a.in";
			request.PhoneNumber = "+91 611.111.111";

			// Act
			AddContactResponse response = await (controller.AddContact(request));

			// Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(response.ReturnValueInt, BaseResponse.RESPONSE_INVALIDREQUEST);
			Assert.AreEqual(response.ReturnValueCustomString, "FirstName is empty.InValid Email Address.");
		}

		[TestMethod]
		public async Task AddContact_EmailDuplicate()
		{
			// Arrange
			ICore core = new MockCore();
			ILogger logger = new Logger();
			ContactsController controller = new ContactsController(core, logger);
			AddContactRequest request = new AddContactRequest();
			request.FirstName = "Vijay6";
			request.LastName = "Lonkar6";
			request.EmailAddress = "v.l4@a.in";
			request.PhoneNumber = "+91 611.111.111";

			// Act
			AddContactResponse response = await (controller.AddContact(request));

			// Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(response.ReturnValueInt, BaseResponse.RESPONSE_RUNTIMEEXCEPTION);
			Assert.AreEqual(response.ReturnValueString, "Email Address already Exists");
			Assert.AreEqual(response.ReturnValueCustomString, "Exception in Add");
		}

		#endregion

		#region EditContact Tests

		[TestMethod]
		public async Task EditContact_Success()
		{
			// Arrange
			ICore core = new MockCore();
			ILogger logger = new Logger();
			ContactsController controller = new ContactsController(core, logger);
			EditContactRequest request = new EditContactRequest();
			request.ContactID = 2;
			request.FirstName = "Vijay6";
			request.LastName = "Lonkar6";
			request.EmailAddress = "v.l6@gmail.com";
			request.PhoneNumber = "+91 611.111.111";

			// Act
			EditContactResponse response = await (controller.EditContact(request));

			// Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(response.ReturnValueInt, BaseResponse.RESPONSE_SUCCESS);
			Assert.AreEqual(response.ReturnValueCustomString, "Contact Edited Successfully.");
		}

		[TestMethod]
		public async Task EditContact_NullInput()
		{
			// Arrange
			ICore core = new MockCore();
			ILogger logger = new Logger();
			ContactsController controller = new ContactsController(core, logger);

			// Act
			EditContactResponse response = await (controller.EditContact(null));

			// Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(response.ReturnValueInt, BaseResponse.RESPONSE_NULLOBJECTS);
			Assert.AreEqual(response.ReturnValueCustomString, "NULL Request object.");
		}

		[TestMethod]
		public async Task EditContact_InValidInput()
		{
			// Arrange
			ICore core = new MockCore();
			ILogger logger = new Logger();
			ContactsController controller = new ContactsController(core, logger);
			EditContactRequest request = new EditContactRequest();
			request.ContactID = -1;
			request.FirstName = "";
			request.LastName = "Lonkar6";
			request.EmailAddress = "v.l4a.in";
			request.PhoneNumber = "+91 611.111.111";

			// Act
			EditContactResponse response = await (controller.EditContact(request));

			// Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(response.ReturnValueInt, BaseResponse.RESPONSE_INVALIDREQUEST);
			Assert.AreEqual(response.ReturnValueCustomString, "Invalid Contact ID.FirstName is empty.InValid Email Address.");
		}

		[TestMethod]
		public async Task EditContact_RowNotFound()
		{
			// Arrange
			ICore core = new MockCore();
			ILogger logger = new Logger();
			ContactsController controller = new ContactsController(core, logger);
			EditContactRequest request = new EditContactRequest();
			request.ContactID = 1000;
			request.FirstName = "Vijay6";
			request.LastName = "Lonkar6";
			request.EmailAddress = "v.l4@a.in";
			request.PhoneNumber = "+91 611.111.111";

			// Act
			EditContactResponse response = await (controller.EditContact(request));

			// Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(response.ReturnValueInt, BaseResponse.RESPONSE_RUNTIMEEXCEPTION);
			Assert.AreEqual(response.ReturnValueString, "Row not found");
			Assert.AreEqual(response.ReturnValueCustomString, "Exception in Edit");
		}

		#endregion

		#region DeleteContact Test

		[TestMethod]
		public async Task DeleteContact_Success()
		{
			// Arrange
			ICore core = new MockCore();
			ILogger logger = new Logger();
			ContactsController controller = new ContactsController(core, logger);
			DeleteContactRequest request = new DeleteContactRequest();
			request.ContactID = 3;

			// Act
			DeleteContactResponse response = await (controller.DeleteContact(request));

			// Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(response.ReturnValueInt, BaseResponse.RESPONSE_SUCCESS);
			Assert.AreEqual(response.ReturnValueCustomString, "Contact Deleted Successfully.");
		}

		[TestMethod]
		public async Task DeleteContact_NullInput()
		{
			// Arrange
			ICore core = new MockCore();
			ILogger logger = new Logger();
			ContactsController controller = new ContactsController(core, logger);

			// Act
			DeleteContactResponse response = await (controller.DeleteContact(null));

			// Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(response.ReturnValueInt, BaseResponse.RESPONSE_NULLOBJECTS);
			Assert.AreEqual(response.ReturnValueCustomString, "NULL Request object.");
		}

		[TestMethod]
		public async Task DeleteContact_InValidInput()
		{
			// Arrange
			ICore core = new MockCore();
			ILogger logger = new Logger();
			ContactsController controller = new ContactsController(core, logger);
			DeleteContactRequest request = new DeleteContactRequest();
			request.ContactID = -1;

			// Act
			DeleteContactResponse response = await (controller.DeleteContact(request));

			// Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(response.ReturnValueInt, BaseResponse.RESPONSE_INVALIDREQUEST);
			Assert.AreEqual(response.ReturnValueCustomString, "InValid Contact ID.");
		}

		[TestMethod]
		public async Task DeleteContact_RowNotFound()
		{
			// Arrange
			ICore core = new MockCore();
			ILogger logger = new Logger();
			ContactsController controller = new ContactsController(core, logger);
			DeleteContactRequest request = new DeleteContactRequest();
			request.ContactID = 1000;

			// Act
			DeleteContactResponse response = await (controller.DeleteContact(request));

			// Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(response.ReturnValueInt, BaseResponse.RESPONSE_RUNTIMEEXCEPTION);
			Assert.AreEqual(response.ReturnValueString, "Row not found");
			Assert.AreEqual(response.ReturnValueCustomString, "Exception in Delete");
		}

		[TestMethod]
		public async Task DeleteContact_ActiveRowNotFound()
		{
			// Arrange
			ICore core = new MockCore();
			ILogger logger = new Logger();
			ContactsController controller = new ContactsController(core, logger);
			DeleteContactRequest request = new DeleteContactRequest();
			request.ContactID = 2;

			// Act
			DeleteContactResponse response = await (controller.DeleteContact(request));

			// Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(response.ReturnValueInt, BaseResponse.RESPONSE_RUNTIMEEXCEPTION);
			Assert.AreEqual(response.ReturnValueString, "Row not found");
			Assert.AreEqual(response.ReturnValueCustomString, "Exception in Delete");
		}
		#endregion
	}
}
