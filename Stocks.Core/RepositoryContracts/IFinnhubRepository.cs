namespace RepositoryContracts
{
    /// <summary>
    /// Represents a repository that makes HTTP requestes to finnhub.io
    /// </summary>
    public interface IFinnhubRepository
    {
        // <summary>
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

        /// <summary>
        /// Returns list of all stocks supported by an exchange (defualt: US)
        /// </summary>
        /// <returns></returns>
        Task<List<Dictionary<string, string>>>GetStocks();

        /// <summary>
        /// Returns list of matching stocks based on the given stock symbol
        /// </summary>
        /// <param name="stockSymbolToSearch"></param>
        /// <returns>List of matching stocks</returns>
        Task<Dictionary<string, object>?>SearchStocks(string stockSymbolToSearch);

    }
}
