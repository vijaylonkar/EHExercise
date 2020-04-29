using System;
using System.Text.RegularExpressions;

namespace EvolentHealthAPILib
{
	internal class CustomValidator
	{
		internal bool IsValidEmail(string email)
		{
			try
			{
				if (email == null)
				{
					return false;
				}

				Int32 firstATPosition = email.IndexOf('@');
				Int32 lastATPosition = email.LastIndexOf('@');

				if ((firstATPosition > 0) && (lastATPosition == firstATPosition) &&
				(firstATPosition < (email.Length - 1)))
				{
					// address is ok regarding the single @ sign
					return (Regex.IsMatch(email, @"^([a-zA-Z0-9'_.\-’])+@([a-zA-Z0-9'_.\-’])+\.([a-zA-Z])+([a-zA-Z])+"));
				}
				else
				{
					return false;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
