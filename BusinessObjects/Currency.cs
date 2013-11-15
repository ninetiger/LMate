using System.Collections.Generic;

namespace BusinessObjects
{
    public class Currency
    {
        public string Id { get; set; }
        public string Country { get; set; }
    }

    public struct Currencies
    {
        public List<Currency> CurrencyList { get; set; }
    }
}
