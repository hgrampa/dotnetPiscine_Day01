using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace day01_ex00
{
	class Exchanger
	{
		private List<ExchangeRate> _rates;
		private CultureInfo _culture;

		public Exchanger(CultureInfo culture)
		{
			_culture = culture;
		}

		public void AddRatesFromDirectory(string dirName)
		{
			if (!Directory.Exists(dirName))
				return;
			var files = Directory.GetFiles(dirName);
			foreach (var file in files)
				AddRatesFormFile(file);
		}
		
		public void AddRatesFormFile(string fileName)
		{
			if (_rates == null)
				_rates = new List<ExchangeRate>();
			
			var currencyIn = Path.GetFileNameWithoutExtension(fileName);
			var lines = File.ReadAllLines(fileName);
			foreach (var line in lines)
			{
				var rate = new ExchangeRate(currencyIn);
				rate.TryParseOutRate(line, _culture);
				_rates.Add(rate);
			}
		}

		public List<ExchangeSum> GetAllConversions(ExchangeSum sum)
		{
			var conversions = new List<ExchangeSum>();
			for (var index = 0; index < _rates.Count; index++)
			{
				var rate = _rates[index];
				if (rate.currencyIn == sum.id)
					conversions.Add(new ExchangeSum(sum.sum * rate.rate, rate.currencyOut));
			}

			return conversions;
		}
	}
}
