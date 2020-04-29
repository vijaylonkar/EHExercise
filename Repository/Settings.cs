using System;
using System.Configuration;
using Repository.Interfaces;

namespace Repository
{
	public class Settings
	{
		private readonly ILogger logger;
		public Settings(ILogger logger)
		{
			this.logger = logger;
		}

		public string GetAppSettingsValue(string key, string defaultValue)
		{
			string value = ConfigurationManager.AppSettings[key];
			if (String.IsNullOrWhiteSpace(value))
			{
				this.logger.LogWarnMessage(this.GetType(),
					String.Format("The settings value for '{0}' does not exist.  Defaulting to '{1}'.", key, defaultValue));
				value = defaultValue;
			}

			return value.ToString();
		}
	}
}
