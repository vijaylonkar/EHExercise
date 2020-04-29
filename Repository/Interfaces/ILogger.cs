using System;

namespace Repository.Interfaces
{
	public interface ILogger
	{
		void ConfigLogging(string loggingConfigFileLocation);
		void LogFatalMessage(Type type, string message);
		void LogFatalMessage(Type type, string message, Exception exception);
		void LogErrorMessage(Type type, string message);
		void LogErrorMessage(Type type, string message, Exception exception);
		void LogWarnMessage(Type type, string message);
		void LogWarnMessage(Type type, string message, Exception exception);
		void LogInfoMessage(Type type, string message);
		void LogInfoMessage(Type type, string message, Exception exception);
		void LogDebugMessage(Type type, string message);
		void LogDebugMessage(Type type, string message, Exception exception);
	}
}
