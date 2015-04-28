using System.Collections.Generic;

namespace Beefbooster.Operations.PredictabullServices
{
    public class UserView
    {
        public int UserId { get; set; }
        public string AccountNumber { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }

    public class AccountView
    {
        public int UserId { get; set; }
        public string AccountNumber { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Contact { get; set; }
        public int Contracted { get; set; }
        public int Purchased { get; set; }
    }

    public class UsersAndPreferencesView
    {
        public string Strain { get; set; }
        public int SaleYear { get; set; }
        public IEnumerable<UserView> Users { get; set; }         
    }


    public class AccountsWithPreferencesView
    {
        public string Strain { get; set; }
        public int SaleYear { get; set; }
        public IEnumerable<AccountView> Accounts { get; set; }
    }


}