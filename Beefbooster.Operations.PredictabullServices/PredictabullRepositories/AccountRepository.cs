using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Beefbooster.Operations.PredictabullServices.Models;

namespace Beefbooster.Operations.PredictabullServices.PredictabullRepositories
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public IEnumerable<Account> Accounts(string strain, int saleYear, IEnumerable<string> accountNos)
        {            
            // valid account number MUST BE EXACTLY 20 charaters long!
            var validAccountNos = accountNos.Where(x => x.Length == 20).ToList();

            if (!validAccountNos.ToList().Any()) return new List<Account>();

            string joinedAccountNos = validAccountNos.Aggregate((current, next) => current + next);
            SqlCommand command = BuildCommand("[pb].[AccountsExtendedInformation]");
            AddParameters(command, strain, saleYear, joinedAccountNos);
            OpenConnection(command.Connection);
            SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return ReadData(dataReader);
        }

        private static IEnumerable<Account> ReadData(SqlDataReader rdr)
        {
            var lst = new List<Account>();

            int ordAccountNo = rdr.GetOrdinal("AccountNo");
            int ordCompany = rdr.GetOrdinal("Company");
            int ordContact = rdr.GetOrdinal("Contact");
            int ordContracted = rdr.GetOrdinal("Contracted");
            int ordPurchased = rdr.GetOrdinal("Purchased");

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    lst.Add(
                        new Account
                            {
                                AccountNo = ((string)
                                             ParameterUtils.SafeGetValue(rdr.GetValue(ordAccountNo), typeof (string),
                                                                         Constants.InitializeString)),
                                Company = ((string)
                                           ParameterUtils.SafeGetValue(rdr.GetValue(ordCompany), typeof (string),
                                                                       Constants.InitializeString)),
                                Contact = ((string)
                                           ParameterUtils.SafeGetValue(rdr.GetValue(ordContact), typeof (string),
                                                                       Constants.InitializeString)),
                                Contracted = ((int)
                                              ParameterUtils.SafeGetValue(rdr.GetValue(ordContracted), typeof (int),
                                                                          Constants.InitializeInt)),
                                Purchased = ((int)
                                             ParameterUtils.SafeGetValue(rdr.GetValue(ordPurchased), typeof (int),
                                                                         Constants.InitializeInt))
                            });
                }
            }

            return lst;
        }

        private void AddParameters(SqlCommand sqlCmd, string strain, int saleYear, string accts)
        {
            sqlCmd.Parameters.Add(new SqlParameter
                {
                    Value = strain,
                    ParameterName = "strain",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Char,
                    Size = 2
                });

            sqlCmd.Parameters.Add(new SqlParameter
                {
                    Value = saleYear,
                    ParameterName = "saleYear",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Int
                });


            sqlCmd.Parameters.Add(new SqlParameter
                {
                    Value = accts,
                    ParameterName = "acctnos",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 2000
                });
        }
    }
}