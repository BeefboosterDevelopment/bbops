using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Beefbooster.Operations.PredictabullServices.Models;

namespace Beefbooster.Operations.PredictabullServices.PredictabullRepositories
{
    public class PercentileRepository : BaseRepository, IPercentileRepository
    {
        public List<string> PercentileColumnNames
        {
            get
            {
                return new List<string>
                    {
                        "BW_EBV",
                        "WWD_EBV",
                        "YWT_EBV",
                        "SC_EBV",
                        "BF_EBV",
                        "WWM_EBV",
                        "MW_EBV",
                        /*"RFI_EBV",*/
                        "H18M_EBV",
                        "SEL_IDX"
                    };
            }
        }

        public List<StrainPercentiles> Calculate(string strain, int saleYear)
        {
            List<StrainPercentiles> allSPs = CalcAllPercentiles(strain, Convert.ToInt16(saleYear));
            foreach (StrainPercentiles sP in allSPs)
            {
                foreach (var pct in sP.Percentiles)
                {
                    SqlCommand command = BuildCommand("[pb].[AddPercentile]");
                    AddSetParameters(command, strain, Convert.ToInt16(saleYear), sP.ColName, pct.Key, pct.Value);
                    OpenConnection(command.Connection);
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                    command.Dispose();
                }
            }
            return allSPs;
        }

        public StrainPercentiles Get(string strain, int saleYear, string colName)
        {
            DateTime calculatedOn = CalculatedOn(strain, saleYear);

            SqlCommand cmd = BuildCommand("pb.GetPercentiles");
            cmd.Parameters.Add(new SqlParameter
                {
                    Value = strain,
                    ParameterName = "strain",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.VarChar,
                    Size = 2
                });

            cmd.Parameters.Add(new SqlParameter
                {
                    Value = saleYear,
                    ParameterName = "saleYear",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.SmallInt
                });

            cmd.Parameters.Add(new SqlParameter
                {
                    Value = colName,
                    ParameterName = "colName",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.VarChar,
                    Size = 20
                });

            var sp = new StrainPercentiles
                {
                    ColName = colName,
                    SaleYear = saleYear,
                    Strain = strain,
                    CalculatedOn = calculatedOn,
                    Percentiles = new Dictionary<int, decimal>()
                };

            OpenConnection(cmd.Connection);
            using (cmd.Connection)
            {
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                using (reader)
                {
                    if (reader.HasRows)
                    {
                        int ordPercentile = reader.GetOrdinal("Percentile");
                        int ordValue = reader.GetOrdinal("Value");
                        while (reader.Read())
                        {
                            var p =
                                ((int)
                                 ParameterUtils.SafeGetValue(reader.GetValue(ordPercentile), typeof (int),
                                                             Constants.InitializeInt));
                            var v =
                                ((decimal)
                                 ParameterUtils.SafeGetValue(reader.GetValue(ordValue), typeof (decimal),
                                                             Constants.InitializeDecimal));

                            sp.Percentiles.Add(p, v);
                        }
                    }
                }
            }
            return sp;
        }

        private DateTime CalculatedOn(string strain, int saleYear)
        {
            DateTime created = Constants.InitializeDateTime, updated = Constants.InitializeDateTime;

            SqlCommand cmdDates = BuildCommand("pb.PercentileCalculatedOn");
            cmdDates.Parameters.Add(new SqlParameter
                {
                    Value = strain,
                    ParameterName = "strain",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.VarChar,
                    Size = 2
                });

            cmdDates.Parameters.Add(new SqlParameter
                {
                    Value = saleYear,
                    ParameterName = "saleYear",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.SmallInt
                });

            OpenConnection(cmdDates.Connection);
            using (cmdDates.Connection)
            {
                SqlDataReader dateReader = cmdDates.ExecuteReader(CommandBehavior.CloseConnection);
                using (dateReader)
                {
                    if (dateReader.HasRows)
                    {
                        int ordCreateDate = dateReader.GetOrdinal("CreateDate");
                        int ordUpdateDate = dateReader.GetOrdinal("UpdateDate");
                        dateReader.Read();
                        created = ((DateTime)
                                   ParameterUtils.SafeGetValue(dateReader.GetValue(ordCreateDate), typeof (DateTime),
                                                               Constants.InitializeDateTime));
                        updated = ((DateTime)
                                   ParameterUtils.SafeGetValue(dateReader.GetValue(ordUpdateDate), typeof (DateTime),
                                                               Constants.InitializeDateTime));
                    }
                }
            }

            return updated == Constants.InitializeDateTime ? created : updated;
        }

        private StrainPercentiles CalculatePercentiles(string strain, short saleYear, string colName)
        {
            SqlCommand command = BuildCommand("[pb].[CalcPercentilesForSaleBulls]");
            command.Parameters.Add(new SqlParameter
                {
                    Value = strain,
                    ParameterName = "strain",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.VarChar,
                    Size = 2
                });

            command.Parameters.Add(new SqlParameter
                {
                    Value = saleYear,
                    ParameterName = "saleYear",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.SmallInt
                });

            command.Parameters.Add(new SqlParameter
                {
                    Value = colName,
                    ParameterName = "colName",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.VarChar,
                    Size = 20
                });

            var sp = new StrainPercentiles
                {
                    ColName = colName,
                    CalculatedOn = DateTime.Today,
                    SaleYear = saleYear,
                    Strain = strain,
                    Percentiles = new Dictionary<int, decimal>()
                };

            OpenConnection(command.Connection);
            using (command.Connection)
            {
                SqlDataReader dataReader = command.ExecuteReader();
                using (dataReader)
                {
                    if (dataReader.HasRows)
                    {
                        int ordPercentile = dataReader.GetOrdinal("Percentile");
                        int ordValue = dataReader.GetOrdinal("Value");
                        while (dataReader.Read())
                        {
                            var per = ((decimal)
                                       ParameterUtils.SafeGetValue(dataReader.GetValue(ordPercentile), typeof (decimal),
                                                                   Constants.InitializeDecimal));
                            int peri = Convert.ToInt32(per*100);

                            var pval = ((decimal)
                                        ParameterUtils.SafeGetValue(dataReader.GetValue(ordValue), typeof (decimal),
                                                                    Constants.InitializeDecimal));

                            sp.Percentiles.Add(peri, pval);
                        }
                    }
                }
            }
            return sp;
        }

        private List<StrainPercentiles> CalcAllPercentiles(string strain, short saleYear)
        {
            return PercentileColumnNames.Select(col => CalculatePercentiles(strain, saleYear, col)).ToList();
        }

        private void AddSetParameters(SqlCommand sqlCmd, string strain, short saleYear, string colName, int pct,
                                      decimal pctval)
        {
            sqlCmd.Parameters.Add(new SqlParameter
                {
                    Value = strain,
                    ParameterName = "strain",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.VarChar,
                    Size = 2
                });

            sqlCmd.Parameters.Add(new SqlParameter
                {
                    Value = saleYear,
                    ParameterName = "saleYr",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.SmallInt
                });

            sqlCmd.Parameters.Add(new SqlParameter
                {
                    Value = colName,
                    ParameterName = "col",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.VarChar,
                    Size = 20
                });

            sqlCmd.Parameters.Add(new SqlParameter
                {
                    Value = pct,
                    ParameterName = "pct",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Int
                });

            sqlCmd.Parameters.Add(new SqlParameter
                {
                    Value = pctval,
                    ParameterName = "pctval",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Decimal
                });
        }
    }
}