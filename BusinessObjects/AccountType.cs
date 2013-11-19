﻿namespace BusinessObjects
{
    public class AccountType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TaxRate { get; set; }
        public string UserId { get; set; }
    }
}
