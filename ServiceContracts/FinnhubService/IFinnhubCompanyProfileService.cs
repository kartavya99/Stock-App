namespace ServiceContracts.FinnhubService
{
    /// <summary>
    /// Represents a service that makes HTTP requests to finnhub.io
    /// </summary>
    public interface IFinnhubCompanyProfileService
    {
        /// <summary>
        /// Returns company details such as company country, currency, exchange, IPO date, logo image, etc.
        /// </summary>
        /// <param name="stockSymbol">Stock symbol to search</param>
        /// <returns>Retunts a dictionary that contains details such as company country, exchange, IPO date, logo image etc.</returns>
        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);       
    }
}
