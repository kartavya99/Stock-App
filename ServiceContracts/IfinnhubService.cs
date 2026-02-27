namespace ServiceContracts
{
    /// <summary>
    /// Represents a service that makes HTTP requests to finnhub.io
    /// </summary>
    public interface IFinnhubService
    {
        /// <summary>
        /// Returns company details such as company country, currency, exchange, IPO date, logo image, etc.
        /// </summary>
        /// <param name="stockSymbol">Stock symbol to search</param>
        /// <returns>Retunts a dictionary that contains details such as company country, exchange, IPO date, logo image etc.</returns>
        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);

        /// <summary>
        /// Returns stock price details such as current price, chagne in price, perecentage chagne, high price of the day, low price of the day, open price of the day, previous close price
        /// </summary>
        /// <param name="stockSymbole">Stock symbol to search</param>
        /// <returns>Returns a dictionary that contatins details such as current price, chagne in price, perecentage chagne, high price of the day, low price of the day, open price of the day, previous close price</returns>
        Task<Dictionary<string, object>?> GetSotckPriceQuote(string stockSymbol);
    }
}
