using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects.Shared;

namespace DataObjects.ADO.NET.SqlServer
{
    //todo the select should be specific to each field for security rather than select *
    /// <summary>
    /// Sql Server specific data access object that handles data access of receipts.
    /// </summary>
    public class SqlServerReceiptDao : IReceiptDao
    {
        /// <summary>
        /// Gets a receipt.
        /// </summary>
        /// <param name="receiptId">Unique receipt identifier.</param>
        /// <returns>Receipt.</returns>
        public Receipt GetReceipt(int receiptId)
        {
            string sql =
                @" SELECT *
                FROM [Receipts]
                WHERE Id = @Id";

            object[] parms = { "@Id", receiptId };
            return Db.Read(sql, Make, parms);
        }
        public async Task<Receipt> GetReceiptAsync(int receiptId)
        {
            string sql =
                @" SELECT *
                FROM [Receipts]
                WHERE Id = @Id";

            object[] parms = { "@Id", receiptId };
            return await Db.ReadAsync(sql, Make, parms);
        }

        /// <summary>
        /// Gets list of receipts in given sortorder from every singal user!!!
        /// </summary>
        /// <param name="sortExpression">The required sort order.</param>
        /// <returns>List of receipts.</returns>
        public List<Receipt> GetAllReceipts(string sortExpression)
        {
            string sql =
            @"SELECT *
                FROM [Receipts] ".OrderBy(sortExpression);

            return Db.ReadList(sql, Make);
        }

        /// <summary>
        /// Gets list of receipts in given sortorder with a given userId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sortExpression">The required sort order.</param>
        /// <returns>List of receipts.</returns>
        public IQueryable<Receipt> GetReceiptsByUser(string userId, string sortExpression)
        {
            string sql =
                @"SELECT * FROM [Receipts] WHERE User_Id = @userId".OrderBy(sortExpression);

            object[] parms = { "@UserId", userId };
            return Db.ReadList(sql, Make, parms).AsQueryable();
        }

        /// <summary>
        /// Gets the receipts between a given data range.
        /// </summary>
        /// <param name="dateFrom">Start date.</param>
        /// <param name="dateThru">End date.</param>
        /// <returns></returns>
        public List<Receipt> GetReceiptsByDate(DateTime dateFrom, DateTime dateThru)
        {
            string sql =
                 @" SELECT *
                 FROM [Receipts]
                WHERE PurchaseDate  >= @DateFrom
                  AND PurchaseDate  <= @DateThru
                ORDER BY OrderDate ASC ";

            object[] parms = { "@DateFrom", dateFrom, "@DateThru", dateThru };
            return Db.ReadList(sql, Make, parms);
        }


        /// <summary>
        /// Insert new or update a receipt based on receipt id
        /// </summary>
        /// <param name="receipt">If the Id is 0, the recored is inserted as new.</param>
        public void SaveReceipt(Receipt receipt)
        {
            if (receipt.Id == 0)
            {
                string sql =
                    @"INSERT INTO [Receipts] (Description, PurchaseDate, Price) 			
                    VALUES (@CompanyName, @PurchaseDate, @Price)";

                receipt.Id = Db.Insert(sql, Take(receipt));
                receipt.Version = GetReceipt(receipt.Id).Version;
            }
            else
            {
                string sql =
                    @"UPDATE [Receipts]
                     SET Description = @Description, PurchaseDate = @PurchaseDate, Price = @Price
                     WHERE Id = @Id AND Version = @Version";

                Db.Update(sql, Take(receipt));
            }
        }


        /// <summary>
        /// Deletes a receipt.
        /// </summary>
        /// <param name="receipt">Receipt.</param>
        /// <returns>Number of receipt records deleted.</returns>
        public void DeleteReceipt(ReceiptBrief receipt)
        {
//            string sql =
//            @"DELETE FROM [Receipts]
//               WHERE Id = @Id 
//                AND Version = @Version";

//            Db.Update(sql, Take(receipt));
            throw new NotImplementedException();
        }
        public Task DeleteReceiptAsync(ReceiptBrief receipt)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a Receipt object based on DataReader.
        /// </summary>
        private static readonly Func<IDataReader, Receipt> Make = reader =>
           new Receipt
           {
               Id = reader["Id"].AsId(),
               Description = reader["Description"].AsString(),
               PurchaseDate = reader["PurchaseDate"].AsDateTime(),
               UserId = reader["User_Id"].AsString(),
               Version = reader["Version"].AsBase64String()
           };


        /// <summary>
        /// Creates query parameters list from Receipt object
        /// </summary>
        /// <param name="receipt">Receipt.</param>
        /// <returns>Name value parameter list.</returns>
        private static object[] Take(Receipt receipt)
        {
            return new object[]  
            {
                "@Id", receipt.Id,
                "@Description", receipt.Description,
                "@PurchaseDate", receipt.PurchaseDate,
                "@Tax", receipt.Tax,
			    "@User_Id", receipt.UserId,
			    "@Version", receipt.Version.AsByteArray()
            };
        }


        public IQueryable<ReceiptBrief> GetReceiptBriefsByUser(string userId, string sortExpression = "Id ASC")
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<ReceiptBrief>> GetReceiptBriefsByUserAsync(string userId, string sortExpression = "Id ASC")
        {
            throw new NotImplementedException();
        }
    }
}
