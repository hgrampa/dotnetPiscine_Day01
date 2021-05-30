using System;

namespace d01_ex00
{
	struct ExchangeRate
	{
		public string currencyIn;
		public string currencyOut;
		public double rate;

		public ExchangeRate(string currencyIn) : this()
		{
			this.currencyIn = currencyIn;
		}

		public bool TryParseOutRate(string formatString)
		{
			var substrings = formatString.Split(':');
			if (substrings.Length != 2)
				return false;
			if (!Double.TryParse(substrings[1], out double rate))
				return false;
			this.currencyOut = substrings[0];
			this.rate = rate;
			return true;
		}

	}
}