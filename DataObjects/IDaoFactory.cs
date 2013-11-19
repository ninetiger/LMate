namespace DataObjects
{
    /// <summary>
    /// Abstract factory interface. Creates data access objects.
    /// </summary>
    /// <remarks>
    /// GoF Design Pattern: Factory.
    /// </remarks>
    public interface IDaoFactory
    {
        /// <summary>
        /// Gets a receipt data access object.
        /// </summary>
        IReceiptDao ReceiptDao { get; }

        IAccountTypeDao AccountTypeDao { get; }
    }
}
