using System.Data;

namespace SmartSpiderCore.Out
{
    public abstract class DbOutput : Output
    {
        protected IDbConnection DbConnection { get; set; }
        protected IDbCommand DbCommand { get; set; }

        public string ConnectionString { get; set; }

        public string InsertFormat { get; set; }

        public override void Dispose()
        {
            if (DbCommand != null)
            {
                DbCommand.Dispose();
                DbCommand = null;
            }

            if (DbConnection.State != ConnectionState.Closed)
            {
                DbConnection.Close();
            }

            if (DbConnection != null)
            {
                DbConnection.Dispose();
                DbConnection = null;
            }
        }
    }
}
