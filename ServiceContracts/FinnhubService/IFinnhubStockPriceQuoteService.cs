namespace ServiceContracts
{
    /// <summary>
    /// Represents a service that makes HTTP requests to finnhub.io
    /// </summary>
    public interface IFinnhubStockPriceQuoteSerivce
    {
        /// <summary>
        /// Returns stock price details such as current price, chagne in price, perecentage chagne, high price of the day, low price of the day, open price of the day, previous close price
        /// </summary>
        /// <param name="stockSymbole">Stock symbol to search</param>
        /// <returns>Returns a dictionary that contatins details such as current price, chagne in price, perecentage chagne, high price of the day, low price of the day, open price of the day, previous close price</returns>
        Task<Dictionary<string, object>?> GetSotckPriceQuote(string stockSymbol);

    }
}
