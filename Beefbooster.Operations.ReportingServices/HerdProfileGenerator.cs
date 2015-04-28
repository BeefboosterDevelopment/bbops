using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Beefbooster.Operations.ReportingServices
{
    public class HerdProfileGenerator
    {
        private const string Createherdprofile = "rpt.CreateHerdProfile";
        public int Generate(int year, int herdSN)
        {
            int newProfileId;
            try
            {
                using (
                    var connection =
                        new SqlConnection(
                            ConfigurationManager.ConnectionStrings["OperationsConnectionString"].ConnectionString))
                {
                    using (var sqlCmd = new SqlCommand(Createherdprofile, connection))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Clear();
                        sqlCmd.Parameters.Add(new SqlParameter
                        {
                            DbType = DbType.Int32,
                            Direction = ParameterDirection.Input,
                            ParameterName = "@birthYear",
                            Value = year
                        });
                        sqlCmd.Parameters.Add(new SqlParameter
                        {
                            DbType = DbType.Int32,
                            Direction = ParameterDirection.Input,
                            ParameterName = "@herdSN",
                            Value = herdSN
                        });
                        connection.Open();
                        newProfileId = (Int32)sqlCmd.ExecuteScalar();
                    }
                }
            }
            catch (SqlException )
            {
                newProfileId = -1;
            }
            return newProfileId;
        }
    }
}