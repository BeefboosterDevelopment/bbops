using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Beefbooster.Operations.PredictabullServices.Models;
using Beefbooster.Operations.PredictabullServices.PredictabullRepositories;
using Newtonsoft.Json;

namespace Beefbooster.Operations.PredictabullServices
{
    // Arvixe accounts data is at this URL
    // http://service.predict-a-bull.com/api/accounts/2012/M1

    // Arvixe pref data is at this URL
    // http://service.predict-a-bull.com/api/preferences/stevoGM/2012/M1


    public class PredictABullAccountServices : IPredictABullAccountServices
    {
        private readonly IAccountRepository _accountRepository;
        private readonly PredictabullWebRequest _predictabullWebRequest;
        private readonly string _siteHome;

        public PredictABullAccountServices(IAccountRepository accountRepository,
            PredictabullWebRequest predictabullWebRequest)
        {
            //_siteHome = "service.predict-a-bull.com/api";
            _siteHome = ConfigurationManager.AppSettings["PredictABullServiceURL"];
            _predictabullWebRequest = new PredictabullWebRequest();
            _accountRepository = accountRepository;
        }

        public PreferencesView PreferencesForUser(int userId, int year, string strain)
        {
            string url = string.Format("http://{0}/preferences/{1}/{2}/{3}", _siteHome, userId, year, strain);
            string response = _predictabullWebRequest.IssueWebRequest(url, null);
            return JsonConvert.DeserializeObject<PreferencesView>(response);
        }

        public AccountsWithPreferencesView AccountsWithPreferences(int year, string strain)
        {
            string url = string.Format("http://{0}/accounts/{1}/{2}", _siteHome, year, strain);
            string response = _predictabullWebRequest.IssueWebRequest(url, null);
            var userView = JsonConvert.DeserializeObject<UsersAndPreferencesView>(response);
            IEnumerable<string> listOfAccountNos = userView.Users.Select(a => a.AccountNumber);

            List<Account> accountsList = _accountRepository.Accounts(strain, year, listOfAccountNos).ToList();

            IEnumerable<AccountView> listAccountView = from u in userView.Users
                join a in accountsList on u.AccountNumber equals a.AccountNo
                    into ua
                from joined in ua.DefaultIfEmpty()
                select new AccountView
                {
                    AccountNumber = u.AccountNumber,
                    UserId = u.UserId,
                    Username = u.Username,
                    Email = u.Email,
                    Company = joined == null ? "N/A" : joined.Company,
                    Contact = joined == null ? "N/A" : joined.Contact,
                    Contracted = joined == null ? -1 : joined.Contracted,
                    Purchased = joined == null ? -1 : joined.Purchased
                };


            var vw = new AccountsWithPreferencesView
            {
                SaleYear = year,
                Strain = strain,
                Accounts = listAccountView
            };

            return vw;
        }
    }
}