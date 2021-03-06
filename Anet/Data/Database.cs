﻿using System.Data;

namespace Anet.Data
{
    public class Database
    {
        public Database(IDbConnection connection)
        {
            Connection = connection;
        }

        public IDbConnection Connection { get; private set; }

        public IDbTransaction Transaction { get; private set; }

        public IDbTransaction BeginTransaction()
        {
            return BeginTransaction(IsolationLevel.Unspecified);
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            // Auto open connection.
            if (Connection.State == ConnectionState.Closed)
                Connection.Open();
            return Connection.BeginTransaction(il);
        }

        public void Dispose()
        {
            if (Transaction == null)
            {
                Transaction.Dispose();
                Transaction = null;
            }
            Connection?.Close();
            Connection?.Dispose();
            Connection = null;
        }
    }
}
