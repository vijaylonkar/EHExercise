using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolentHealthLib.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository.Interfaces;

namespace Repository.Services
{
	public class JsonDB
	{
		protected readonly ILogger logger;
		private readonly string jsonDBFile;

		public JsonDB(ILogger logger)
		{
			this.logger = logger;
			this.jsonDBFile = new Settings(this.logger).GetAppSettingsValue("JsonDBFile", "");
		}

		public async Task<BaseResponse> Add(string tableName, string newRow)
		{
			this.logger.LogInfoMessage(this.GetType(), "Begin Add");
			BaseResponse response = new BaseResponse();

			try
			{
				var json = File.ReadAllText(this.jsonDBFile);
				var jsonObj = JObject.Parse(json);
				var rowsArray = jsonObj.GetValue(tableName) as JArray;
				var newRowJsonObject = JObject.Parse(newRow);

				if (rowsArray.Where(obj => obj["mid"].Value<string>() == newRowJsonObject.Property("mid").Value.ToString()).Any())
				{
					throw new Exception("Email Address already Exists");
				}
				else
				{
					Int64 newRowID = (Int64)rowsArray.Last["id"] + 1;
					newRowJsonObject.Property("id").Value = newRowID;
					rowsArray.Add(newRowJsonObject);

					jsonObj[tableName] = rowsArray;
					string newJsonResult = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);

					using (FileStream fs = File.OpenWrite(this.jsonDBFile))
					{
						fs.SetLength(0);
						await fs.WriteAsync(Encoding.ASCII.GetBytes(newJsonResult), 0, newJsonResult.Length);
					}

					response.Success(newRowID);
				}
			}
			catch (Exception ex)
			{
				response.RunTimeException(ex, "Exception in Add");
				this.logger.LogErrorMessage(this.GetType(), "Exception in Add", ex);
			}

			this.logger.LogInfoMessage(this.GetType(), String.Format("End Add:{0}", response.ReturnValueInt));

			return response;
		}

		public async Task<BaseResponse> Edit(string tableName, string editedRow)
		{
			this.logger.LogInfoMessage(this.GetType(), "Begin Edit");
			BaseResponse response = new BaseResponse();

			try
			{
				bool rowFound = false;
				var json = File.ReadAllText(this.jsonDBFile);
				var jsonObj = JObject.Parse(json);
				JArray rowsArray = (JArray)jsonObj[tableName];

				var newRowJson = JObject.Parse(editedRow);
				Int64 editedRowID = (Int64)newRowJson.Property("id").Value;

				foreach (var row in rowsArray.Where(obj => obj["id"].Value<Int64>() == editedRowID))
				{
					//newRowJson.Property("id").Value = row.Value<int>("id");
					rowFound = true;
					row.Replace(newRowJson);
					break;
				}

				if (rowFound)
				{
					string newJsonResult = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
					using (FileStream fs = File.OpenWrite(this.jsonDBFile))
					{
						fs.SetLength(0);
						await fs.WriteAsync(Encoding.ASCII.GetBytes(newJsonResult), 0, newJsonResult.Length);
					}
					response.Success();
				}
				else
				{
					throw new Exception("Row not found");
				}
			}
			catch (Exception ex)
			{
				response.RunTimeException(ex, "Exception in Edit");
				this.logger.LogErrorMessage(this.GetType(), "Exception in Edit", ex);
			}

			this.logger.LogInfoMessage(this.GetType(), String.Format("End Edit:{0}", response.ReturnValueInt));

			return response;
		}

		public async Task<BaseResponse> Delete(string tableName, string editedRow)
		{
			this.logger.LogInfoMessage(this.GetType(), "Begin Delete");
			BaseResponse response = new BaseResponse();

			try
			{
				bool rowFound = false;
				var json = File.ReadAllText(this.jsonDBFile);
				var jsonObj = JObject.Parse(json);
				JArray rowsArray = (JArray)jsonObj[tableName];

				var newRowJson = JObject.Parse(editedRow);
				Int64 editedRowID = (Int64)newRowJson.Property("id").Value;

				foreach (var row in rowsArray.Where(obj => obj["id"].Value<Int64>() == editedRowID))
				{
					if (row.Value<byte>("st") == 1)
					{
						rowFound = true;
						newRowJson = (JObject)row;
						newRowJson.Property("st").Value = 0;
						row.Replace(newRowJson);
					}
					break;
				}

				if (rowFound)
				{
					string newJsonResult = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
					using (FileStream fs = File.OpenWrite(this.jsonDBFile))
					{
						fs.SetLength(0);
						await fs.WriteAsync(Encoding.ASCII.GetBytes(newJsonResult), 0, newJsonResult.Length);
					}
					response.Success();
				}
				else
				{
					throw new Exception("Row not found");
				}
			}
			catch (Exception ex)
			{
				response.RunTimeException(ex, "Exception in Delete");
				this.logger.LogErrorMessage(this.GetType(), "Exception in Delete", ex);
			}

			this.logger.LogInfoMessage(this.GetType(), String.Format("End Delete:{0}", response.ReturnValueInt));

			return response;
		}

		public async Task<BaseResponse> Get(string tableName)
		{
			this.logger.LogInfoMessage(this.GetType(), "Begin Get");
			BaseResponse response = new BaseResponse();

			try
			{
				var json = File.ReadAllText(this.jsonDBFile);
				var jsonObj = JObject.Parse(json);
				JArray rowsArray = (JArray)jsonObj[tableName];


				//response.ReturnValueCustomObject = JsonConvert.SerializeObject(rowsArray, Formatting.Indented);
				response.ReturnValueCustomObject = JsonConvert.SerializeObject(rowsArray.Where(obj => obj["st"].Value<byte>() == 1), Formatting.Indented);

				response.Success();
			}
			catch (Exception ex)
			{
				response.RunTimeException(ex, "Exception in Get");
				this.logger.LogErrorMessage(this.GetType(), "Exception in Get", ex);
			}

			this.logger.LogInfoMessage(this.GetType(), String.Format("End Get:{0}", response.ReturnValueInt));

			return response;
		}

	}
}
