using System;
using System.Net;
using System.Xml;
using System.Globalization;
using System.Xml.Serialization;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Metadata;
using System.Data.SqlTypes;

public class CurrencyObj
{
    public string CurrencyCode { get; set; }
    public double ExchangeRateFromEUR { get; set; }
    public CurrencyObj(string currencyCode, double exchangeRateFromEUR)
    {
        CurrencyCode = currencyCode;
        ExchangeRateFromEUR = exchangeRateFromEUR;
    }
}
public class CurrencyConverter
{

    private static List<CurrencyObj> currencyList = new List<CurrencyObj>();
    public static void FetchRates()
    {
        currencyList.Clear();
        string url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml"; // Exchange rate XML document
        XmlTextReader reader = new XmlTextReader(url);

        bool dataFetched = false;  // Flag to check if we fetched any data

        while (reader.Read()) // Goes through the XML document and saves the currency exchange rates to the local list
        {
            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Cube" && reader.HasAttributes)
            {
                string currencyCode = null;
                double exchangeRate = 0;
                while (reader.MoveToNextAttribute())
                {
                    if (reader.Name == "currency") // Identifies each currency attribute and saves the currency code and rate as an object
                    {
                        currencyCode = reader.Value;
                    }
                    else if (reader.Name == "rate")
                    {
                        exchangeRate = double.Parse(reader.Value);
                    };
                }
                if (!string.IsNullOrEmpty(currencyCode) && exchangeRate > 0)
                {
                    currencyList.Add(new CurrencyObj(currencyCode, exchangeRate));
                    dataFetched = true;
                }
            }
        }
        currencyList.Add(new CurrencyObj("EUR", 1.0));
        if (!dataFetched)
        {
            throw new InvalidOperationException("Failed to fetch currency rates. Please check the API or network.");
        }
    }
    public static double Convert(double input, string fromCurrency, string toCurrency) // Method that uses the fetched rates to convert between the given rates via Euro

    {

        if (currencyList == null || currencyList.Count == 0)
        {
            throw new InvalidOperationException("Currency rates are not loaded. Please fetch the rates first.");
        }
        if (fromCurrency == toCurrency)
        {
            return input;  // No conversion needed if currencies are the same
        }

        var fromCurrencyObj = currencyList.FirstOrDefault(c => c.CurrencyCode == fromCurrency);
        var toCurrencyObj = currencyList.FirstOrDefault(c => c.CurrencyCode == toCurrency);

        if (fromCurrencyObj == null)
        {
            throw new InvalidOperationException($"Currency {fromCurrency} not found.");
        }

        if (toCurrencyObj == null)
        {
            throw new InvalidOperationException($"Currency {toCurrency} not found.");
        }

        double value = 0;

        if (fromCurrency == "EUR")
        {
            value = input * toCurrencyObj.ExchangeRateFromEUR;
        }
        else if (toCurrency == "EUR")
        {
            value = input / fromCurrencyObj.ExchangeRateFromEUR;
        }
        else
        {
            value = input / fromCurrencyObj.ExchangeRateFromEUR;
            value *= toCurrencyObj.ExchangeRateFromEUR;
        }

        return value;
    }
}