using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Beefbooster.Operations.PredictabullServices.PredictabullRepositories
{

    public class BaseRepository
    {
        private readonly string _connectionString;
        
        public BaseRepository()
        {
            _connectionString =  ConfigurationManager.ConnectionStrings["BullConnectionString"].ConnectionString;
        }

        private static void SqlInfoHandler(object sqlConnection, SqlInfoMessageEventArgs e)
        {
            var stringBuilder = new StringBuilder("SQL Server Error in Custom Repository");
            for (int i = 0; i < e.Errors.Count; i++)
            {
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("ERROR #" + Convert.ToString(i + 1));
                stringBuilder.Append(e);
            }
            throw new Exception(stringBuilder.ToString());
        }

        protected  void OpenConnection(SqlConnection connection)
        {
            connection.InfoMessage += SqlInfoHandler;
            connection.ConnectionString = _connectionString;
            if (connection.State == ConnectionState.Closed)
            {
                try
                {
                    connection.Open();
                }
                catch (InvalidOperationException ioex)
                {
                    throw new Exception("Unable to open database connection", ioex);
                }
                catch (SqlException sqlex)
                {
                    throw new Exception("Connection-level error on connection open", sqlex);
                }
            }
            else
            {
                throw new Exception("Connection already opened");
            }
        }

        protected static SqlCommand BuildCommand(string procName)
        {
            var connection = new SqlConnection();

            var sqlCmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                Connection = connection,
                CommandText = procName
            };
            sqlCmd.Parameters.Clear();

            return sqlCmd;
        }
 
        
    }

    public class ParameterUtils
    {
        private const string ValueNotAsExpected2Arg = "Value [{0}] is not of the required type [{1}].";

        public static object DbNullStringCheck(string s)
        {
            if (string.IsNullOrEmpty(s))
                return DBNull.Value;
            return s;
        }

        public static object SafeGetValueByName(string columnName, IDataReader source, Type requiredType,
                                                object defaultValue)
        {
            // determine the ordinal of the specific column
            try
            {
                int ordinal = source.GetOrdinal(columnName);
                object val = source.GetValue(ordinal);
                return SafeGetValue(val, requiredType, defaultValue);
            }
            catch (Exception e)
            {
                throw new Exception(
                    string.Format("SafeGetValueByName could not find column named {0}", columnName), e);
            }
        }

        public static object SafeGetValue(object value, Type requiredType, object defaultValue)
        {
            //try
            //{	
            if (value == DBNull.Value)
            {
                return defaultValue;
            }
            // verify type
            if (value.GetType() != requiredType)
            {
                throw new Exception(String.Format(ValueNotAsExpected2Arg, requiredType, defaultValue));
            }
            return value;
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("SafeGetValue trapped execption", ex);
            //}
        }

        /// <summary>
        ///     Converts deemed 'invalid' values into DBNull to facilitate database null-check rules
        /// </summary>
        /// <param name="value">Any value that will passed into a Parameter</param>
        /// <param name="validValue">A True/False expression defining the valid state that should NOT be converted to a DBNull (i.e. if this is true, leave the value alone)</param>
        /// <returns>The value 'untouched', or a DBNull - depending on whether or not it was valid</returns>
        public static object SafeSetValue(object value, bool validValue)
        {
            // check to see if the value passed in is null,
            // if so convert it to a DBNull
            if (!validValue)
            {
                return DBNull.Value;
            }
            return value;
        }
    }


}