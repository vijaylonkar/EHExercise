using System;
using System.IO;
using log4net;
using log4net.Config;
using Repository.Interfaces;

namespace Repository.Services
{
	public class Logger : ILogger
	{
		public void ConfigLogging(string loggingConfigFileLocation)
		{
			if (File.Exists(loggingConfigFileLocation))
				XmlConfigurator.ConfigureAndWatch(new FileInfo(loggingConfigFileLocation));
			else
				throw new IOException("File not found: " + loggingConfigFileLocation);
		}

		public void LogFatalMessage(Type type, string message) { LogManager.GetLogger(type).Fatal(message); }
		public void LogFatalMessage(Type type, string message, Exception exception) { LogManager.GetLogger(type).Fatal(message, exception); }
		public void LogErrorMessage(Type type, string message) { LogManager.GetLogger(type).Error(message); }
		public void LogErrorMessage(Type type, string message, Exception exception) { LogManager.GetLogger(type).Error(message, exception); }
		public void LogWarnMessage(Type type, string message) { LogManager.GetLogger(type).Warn(message); }
		public void LogWarnMessage(Type type, string message, Exception exception) { LogManager.GetLogger(type).Warn(message, exception); }
		public void LogInfoMessage(Type type, string message) { LogManager.GetLogger(type).Info(message); }
		public void LogInfoMessage(Type type, string message, Exception exception) { LogManager.GetLogger(type).Info(message, exception); }
		public void LogDebugMessage(Type type, string message) { LogManager.GetLogger(type).Debug(message); }
		public void LogDebugMessage(Type type, string message, Exception exception) { LogManager.GetLogger(type).Debug(message, exception); }
	}
}
