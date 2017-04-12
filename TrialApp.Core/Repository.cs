using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TrialApp.Core
{
   public class SQLRepository<T> : IRepository<T> where T : new()
   {
      //public readonly SQLiteConnection _masterConnection;
      public readonly SQLiteConnection connection;
        public SQLRepository(ISQLProvider provider)
        {
            //this._masterConnection = provider.GetMasterConnection();
            this.connection = provider.GetTransactionConnection();
        }
        public bool Delete(T table)
        {
            throw new NotImplementedException();
        }

        public List<T> Get()
        {
            return connection.Table<T>().ToList();
        }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public bool Update(T table)
        {
            throw new NotImplementedException();
        }
    }
}
