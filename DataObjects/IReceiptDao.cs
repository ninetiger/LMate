using LMate.BusinessObjects;
using System;
using System.Collections.Generic;

namespace LMate.DataObjects
{
    /// <summary>
    /// Defines methods to access receipts.
    /// </summary>
    /// <remarks>
    /// This is a database-independent interface. Implementations are database specific.
    /// </remarks>
    public interface IReceiptDao
    {
        /// <summary>
        /// Gets a specific receipt.
        /// </summary>
        /// <param name="receiptId">Unique receipt identifier.</param>
        /// <returns>Receipt.</returns>
        Receipt GetReceipt(int receiptId);

        /// <summary>
        /// Gets a sorted list of all receipts from every singal user!!!!
        /// </summary>
        /// <param name="sortExpression">Sort order.</param>
        /// <returns>Sorted list of receipts.</returns>
        List<Receipt> GetAllReceipts(string sortExpression = "Id ASC");

        List<Receipt> GetReceiptsByUser(string userId, string sortExpression = "Id ASC");

        /// <summary>
        /// Gets a list of receipts placed within a date range.
        /// </summary>
        /// <param name="dateFrom">Date range begin date.</param>
        /// <param name="dateThru">Date range end date.</param>
        /// <returns>List of receipts.</returns>
        List<Receipt> GetReceiptsByDate(DateTime dateFrom, DateTime dateThru);
            
        void SaveReceipt(Receipt receipt);

        /// <summary>
        /// Deletes a receipt
        /// </summary>
        /// <param name="receipt">Receipt.</param>
        void DeleteReceipt(Receipt receipt);
    }
}
