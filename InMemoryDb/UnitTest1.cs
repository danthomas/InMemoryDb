using System;
using System.Data;
using System.Dynamic;
using System.Management.Instrumentation;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InMemoryDb
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Database database = new Database();

            database.Account.Insert(new Account { Name = "thomasd" });
            database.Account.Insert(new Account { Name = "fawcetts" });
            database.Account.Insert(new Account { Name = "bulloughj" });

            database.Session.Insert(new Session{ AccountId = 1, StartDateTime = new DateTime(2015, 1, 1)});
            database.Session.Insert(new Session{ AccountId = 2, StartDateTime = new DateTime(2015, 1, 2)});
            database.Session.Insert(new Session{ AccountId = 3, StartDateTime = new DateTime(2015, 1, 3)});
            
            IDataReader reader = database.Account.Read();

            while (reader.Read())
            {
                short id = reader.GetInt16(0);
                string name = reader.GetString(1);
            }

            reader = database.Session.Read();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                short accountId = reader.GetInt16(1);
                DateTime startDateTime = reader.GetDateTime(2);
            }
        }
    }

    public class Database
    {
        public Database()
        {
            Account = new Table<Account>(
                ColumnBuilder<Account>.Builder.ForProperty("Id").WithAuto().Build(),
                ColumnBuilder<Account>.Builder.ForProperty("Name").Build());

            Session = new Table<Session>(
                ColumnBuilder<Session>.Builder.ForProperty("Id").WithAuto().Build(),
                ColumnBuilder<Session>.Builder.ForProperty("AccountId").Build(),
                ColumnBuilder<Session>.Builder.ForProperty("StartDateTime").Build());
        }

        public Table<Session> Session { get; set; }

        public Table<Account> Account { get; set; }

        public void Clear()
        {
            Account.Clear();
            Session.Clear();
        }
    }
}
