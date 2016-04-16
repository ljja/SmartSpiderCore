using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SmartSpiderCore.Out
{
    public class SqlServerOutput : DbOutput
    {
        public override void Init()
        {
            var sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            DbCommand = new SqlCommand
            {
                CommandText = InsertFormat.ToUpper(),
                CommandType = CommandType.Text,
                Connection = sqlConnection
            };

            DbConnection = sqlConnection;
        }

        public override void Write(ICollection<FieldResult> content)
        {
            DbCommand.Parameters.Clear();

            foreach (var m in content)
            {
                var param = new SqlParameter(string.Format("@{0}", m.DataName.ToUpper()), m.DataValue);
                DbCommand.Parameters.Add(param);
            }

            DbCommand.ExecuteNonQuery();

            DbCommand.Parameters.Clear();
            
        }
    }
}
