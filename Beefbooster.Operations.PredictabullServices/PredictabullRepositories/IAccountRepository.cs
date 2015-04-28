using System.Collections.Generic;
using Beefbooster.Operations.PredictabullServices.Models;

namespace Beefbooster.Operations.PredictabullServices.PredictabullRepositories
{
    public interface IAccountRepository
    {
        IEnumerable<Account> Accounts(string strain, int saleYear, IEnumerable<string> accountNos);
    }
}