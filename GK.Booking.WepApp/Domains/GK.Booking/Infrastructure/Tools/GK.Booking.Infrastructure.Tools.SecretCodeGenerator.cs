using System;
using System.Text;

namespace GK.Booking.Infrastructure.Tools
{
	public static class SecretCodeGenerator
	{
		private readonly static Random _randomGenerator = new Random();

		public static string Generate()
		{
			int prefix = _randomGenerator.Next(0, 9);
			int body = _randomGenerator.Next(10, 99);
			int salt = System.Threading.Thread.CurrentThread.ManagedThreadId;

			return string.Format("{0}{1}{2}",
				ConvertNumberToASCIISymbol(prefix),
				body,
				ConvertNumberToASCIISymbol(salt));
		}

		private static string ConvertNumberToASCIISymbol(int number)
		{
			int aSymbolPosition = 65;
			int alphabetRange = 25;

			if (number < 0)
			{
				throw new ArgumentException("The number can not be negative");
			}

			if (number > 25)
			{
				number = number % alphabetRange;
			}

			return Encoding.ASCII.GetString(new byte[] { (byte)(aSymbolPosition + number) });
		}
	}
}