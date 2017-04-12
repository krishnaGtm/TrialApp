using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enza.DataAccess;
using SQLite;
using TrialApp.Common;
using TrialApp.Entities.Transaction;

namespace TrialApp.DataAccess
{
    public class TrialEntryAppRepository : Repository<TrialEntryApp>
    {
        public TrialEntryAppRepository() : base(DbPath.GetTransactionDbPath())
        {
        }

        public TrialEntryAppRepository(SQLiteAsyncConnection connection) : base(connection)
        {
        }
        public async Task<List<TrialEntryApp>> GetVarietiesListAsync(int ezid)
        {
            var trialEntry = await DbContextAsync().QueryAsync<TrialEntryApp>("select * from TrialEntryApp join Relationship on TrialEntryApp.EZID = Relationship.EZID2 where Relationship.EZID1 = ? order by TrialEntryApp.FieldNumber", ezid);
            return trialEntry;
        }

        public async Task<int> AddTrialEntryWithRelationshipAsync(TrialEntryApp TE, Relationship R)
        {
            if (await DbContextAsync().InsertAsync(TE) > 0)
                return await DbContextAsync().InsertAsync(R);
            return 0;

        }
    }
}
