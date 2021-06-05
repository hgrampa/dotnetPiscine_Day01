using System;
using System.Globalization;

namespace day01_ex00
{
	class Program
	{
		private static void PrintError()
		{
			Console.WriteLine("Input Error. Please check the input and retry the request.");
		}

		private static void Main(string[] args)
		{
			if (args.Length != 2)
			{
				PrintError();
				return;
			}
			
			var culture = (CultureInfo)CultureInfo.InvariantCulture.Clone(); 
			culture.NumberFormat.NumberDecimalSeparator = ",";
			
			var exchanger = new Exchanger(culture);
			exchanger.AddRatesFromDirectory(args[1]);
			if (!ExchangeSum.TryParse(args[0], out var inSum, culture))
			{
				PrintError();
				return;
			}

			var outSums = exchanger.GetAllConversions(inSum);
			Console.WriteLine($"Sum in input currency: {inSum.ToString()}");
			foreach (var outSum in outSums)
			{
				Console.WriteLine($"Sum in {outSum.id}: {outSum.ToString()}");
			}
		}
	}
}
