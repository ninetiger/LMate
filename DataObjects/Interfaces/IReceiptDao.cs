using System;
using System.Collections.Generic;
using DataObjects.EntityFramework;

namespace DataObjects.Interfaces
{
    /// <summary>
    /// Defines methods to access receipts.
    /// </summary>
    /// <remarks>
    /// This is a database-independent interface. Implementations are database specific.
    /// </remarks>
    public interface IReceiptDao : IDao<Receipt>
    {
        //IQueryable<ReceiptBrief> GetReceiptBriefsByUser(string userId, string sortExpression = "Id ASC");
        //Task<IQueryable<ReceiptBrief>> GetReceiptBriefsByUserAsync(string userId, string sortExpression = "Id ASC");

        /// <summary>
        /// Gets a list of receipts placed within a date range.
        /// </summary>
        /// <param name="dateFrom">Date range begin date.</param>
        /// <param name="dateThru">Date range end date.</param>
        /// <returns>List of receipts.</returns>
        List<Receipt> GetReceiptsByDate(DateTime dateFrom, DateTime dateThru);
            
        //IEnumerable<string> AutoCompletSearch(string tagId, string searchString);
    }
}
