using System;
using System.Globalization;

namespace day01_ex00
{
	struct ExchangeSum
	{
		public double sum;
		public string id;

		public ExchangeSum(double sum, string id)
		{
			this.sum = sum;
			this.id = id;
		}

		public override string ToString()
		{
			return $"{sum:f2} {id}";
		}

		public static bool TryParse(string formatString, out ExchangeSum result, CultureInfo cultureInfo = null)
		{
			cultureInfo ??= CultureInfo.InvariantCulture;
			var substrings = formatString.Split(' ');
			if (substrings.Length == 2 
			    && Double.TryParse(substrings[0], NumberStyles.AllowDecimalPoint, cultureInfo, out var sum))
			{
				result = new ExchangeSum(sum, substrings[1]);
				return true;
			}

			result = new ExchangeSum();
			return false;
		}
	}
}
