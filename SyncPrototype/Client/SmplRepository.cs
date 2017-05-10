﻿using Dapper;
using SyncPrototype.Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SyncPrototype.Client
{
    public class SmplRepository : IRepository<Smpl>, IDisposable
    {
        private IConnectionFactory factory;
        private IDbConnection connection;
        private const string update = "UPDATE dbo.ClientSmpl SET Description = @Description WHERE Name = @Name";
        private const string insert = "INSERT INTO dbo.ClientSmpl (Name, Description) VALUES (@Name, @Description)";

        public int Count
        {
            get
            {
                return Connection.Query<int>("SELECT COUNT(1) FROM dbo.ClientSmpl").First();
            }
        }

        public SmplRepository(IConnectionFactory factory)
        {
            this.factory = factory;
        }

        private IDbConnection Connection
        {
            get
            {
                return connection ?? (connection = factory.Create());
            }
        }

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Dispose();
                connection = null;
            }
        }

        public void Save(Smpl sample)
        {
            var command = insert;
            Connection.Execute(command, sample);
        }

        public IEnumerable<Smpl> All()
        {
            return Connection.Query<Smpl>("SELECT * FROM dbo.ClientSmpl");
        }
    }
}