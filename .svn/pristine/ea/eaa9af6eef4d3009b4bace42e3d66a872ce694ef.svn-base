using Enza.DataAccess;
using TrialApp.Common;
using SQLite;
using TrialApp.Entities.Master;

namespace TrialApp.DataAccess
{
    public class SequenceTableRepository : Repository<SequenceTable>
   {
        public SequenceTableRepository() : base(DbPath.GetMasterDbPath())
       {
       }
        public SequenceTableRepository(SQLiteAsyncConnection connection) : base(connection)
        {
        }



    }
}
