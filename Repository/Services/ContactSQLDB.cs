using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using EvolentHealthLib.BusinessData;
using EvolentHealthLib.Messages;
using Repository.Interfaces;

namespace Repository.Services
{
	public class ContactSQLDB : SQLDB, IContactRepository
	{
		public ContactSQLDB(ILogger logger) : base(logger)
		{
		}
		public async Task<BaseResponse> GetContacts()
		{
			this.logger.LogInfoMessage(this.GetType(), "Begin GetContacts");

			BaseResponse response = new BaseResponse();
			try
			{
				ParamArray ps = new ParamArray();
				DataSet ds = await this.ExecuteSP("sproc_GetContact", ps);

				response.ReturnValueCustomObject = this.Convert(ds);
				response.Success();
			}
			catch (Exception ex)
			{
				response.RunTimeException(ex, "Exception in GetContacts");
				this.logger.LogErrorMessage(this.GetType(), "Exception in GetContacts", ex);
			}

			this.logger.LogInfoMessage(this.GetType(), String.Format("End GetContacts:{0}", response.ReturnValueInt));

			return response;
		}

		public async Task<BaseResponse> AddContact(ContactData contactData)
		{
			this.logger.LogInfoMessage(this.GetType(), "Begin AddContact");
			BaseResponse response = new BaseResponse();

			try
			{
				//response = await this.Add("contacts", JsonConvert.SerializeObject(contact));
				ParamArray ps = new ParamArray();

				ps.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 96) { Value = contactData.FirstName });
				ps.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 96) { Value = contactData.LastName });
				ps.Add(new SqlParameter("@EmailAddress", SqlDbType.NVarChar, 256) { Value = contactData.EmailAddress });
				ps.Add(new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 32) { Value = contactData.PhoneNumber });
				SqlParameter outNewContactIDParam = ps.AddParam("@NewContactID", SqlDbType.BigInt, null, ParameterDirection.Output);

				await this.ExecuteSP_NonQuery("sproc_AddContact", ps);

				response.ReturnValueCustomObject = outNewContactIDParam.Value;
				response.Success();
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
				ParamArray ps = new ParamArray();

				ps.Add(new SqlParameter("@ContactID", SqlDbType.BigInt) { Value = contactData.ContactID });
				ps.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 96) { Value = contactData.FirstName });
				ps.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 96) { Value = contactData.LastName });
				ps.Add(new SqlParameter("@EmailAddress", SqlDbType.NVarChar, 256) { Value = contactData.EmailAddress });
				ps.Add(new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 32) { Value = contactData.PhoneNumber });

				await this.ExecuteSP_NonQuery("sproc_EditContact", ps);

				response.Success();
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
				ParamArray ps = new ParamArray();

				ps.Add(new SqlParameter("@ContactID", SqlDbType.BigInt) { Value = contactData.ContactID });
				await this.ExecuteSP_NonQuery("sproc_DeleteContact", ps);

				response.Success();
			}
			catch (Exception ex)
			{
				response.RunTimeException(ex, "Exception in DeleteContact");
				this.logger.LogErrorMessage(this.GetType(), "Exception in DeleteContact", ex);
			}

			this.logger.LogInfoMessage(this.GetType(), String.Format("End DeleteContact:{0}", response.ReturnValueInt));

			return response;
		}

		private List<ContactData> Convert(DataSet dataSet)
		{
			List<ContactData> contactDataList = new List<ContactData>();

			foreach (DataRow row in dataSet.Tables[0].Rows)
			{
				contactDataList.Add(new ContactData((Int64)row[0],
													row[1].ToString(),
													row[2].ToString(),
													row[3].ToString(),
													row[4].ToString(),
													1));
			}

			return contactDataList;
		}
	}
}
