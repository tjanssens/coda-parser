namespace CodaParser.Values
{
    /// <summary>
    /// Raw information of an account.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class.
        /// </summary>
        /// <param name="accountNumberTypeString">The type of the account.</param>
        /// <param name="accountInfo">The raw information of the account.</param>
        /// <param name="accountNameInfo">The raw information about the name.</param>
        public Account(string accountNumberTypeString, string accountInfo, string accountNameInfo)
        {
            Helpers.ValidateStringLength(accountInfo, 37, "Account");
            Helpers.ValidateStringLength(accountNameInfo, 61, "AccountNameInfo");

            AccountInfo aInfo =
                AddAccountInfo(accountInfo, accountNumberTypeString);
            NumberType = new AccountNumberType(accountNumberTypeString);
            Name = new AccountName(accountNameInfo.Substring(0, 26));
            Description = new AccountDescription(accountNameInfo.Substring(26, 35));
            Number = new AccountNumber(aInfo.AccountNumber, aInfo.AccountIsIban);
            Currency = new Currency(aInfo.AccountCurrency);
            Country = new Country(aInfo.AccountCountry);
        }

        /// <summary>
        /// Gets the country.
        /// </summary>
        public Country Country { get; }

        /// <summary>
        /// Gets the currency.
        /// </summary>
        public Currency Currency { get; }

        /// <summary>
        /// Gets the account description.
        /// </summary>
        public AccountDescription Description { get; }

        /// <summary>
        /// Gets the name of the account holder.
        /// </summary>
        public AccountName Name { get; }

        /// <summary>
        /// Gets the number of the account.
        /// </summary>
        public AccountNumber Number { get; }

        /// <summary>
        /// Gets the type of the account number.
        /// </summary>
        public AccountNumberType NumberType { get; }

        private AccountInfo AddAccountInfo(string accountInfo, string accountType)
        {
            var accountIsIban = false;
            var accountNumber = "";
            var accountCurrency = "";
            var accountCountry = "";

            if (accountType == "0")
            {
                accountNumber = accountInfo.Substring(0, 12);
                accountCurrency = accountInfo.Substring(13, 3);
                accountCountry = accountInfo.Substring(17, 2);
            }
            else if (accountType == "1")
            {
                accountNumber = accountInfo.Substring(0, 34);
                accountCurrency = accountInfo.Substring(34, 3);
            }
            else if (accountType == "2")
            {
                accountIsIban = true;
                accountNumber = accountInfo.Substring(0, 31);
                accountCurrency = accountInfo.Substring(34, 3);
                accountCountry = "BE";
            }
            else if (accountType == "3")
            {
                accountIsIban = true;
                accountNumber = accountInfo.Substring(0, 34);
                accountCurrency = accountInfo.Substring(34, 3);
            }

            return new AccountInfo(accountIsIban, accountNumber, accountCurrency, accountCountry);
        }
    }
    public struct AccountInfo
    {
        public AccountInfo(bool accountIsIban, string accountNumber, string accountCurrency, string accountCountry)
        {
            AccountIsIban = accountIsIban;
            AccountNumber = accountNumber;
            AccountCurrency = accountCurrency;
            AccountCountry = accountCountry;
        }

        public bool AccountIsIban { get; set; }
        public string AccountNumber { get; set; }
        public string AccountCurrency { get; set; }
        public string AccountCountry { get; set; }
    }
}