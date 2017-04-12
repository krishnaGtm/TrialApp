using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enza.DataAccess;
using TrialApp.Common;
using TrialApp.Entities.Transaction;
using SQLite;
using System;
using System.Linq.Expressions;

namespace TrialApp.DataAccess
{
    public class TrialRepository : Repository<Trial>
    {
        public TrialRepository() : base(DbPath.GetTransactionDbPath())
        {
        }
        public TrialRepository(SQLiteAsyncConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<Trial>> GetAsync()
        {
            var trials = await DbContextAsync().QueryAsync<Trial>("Select * from Trial");
            return trials;
        }
        public IEnumerable<Trial> Get()
        {
         
            var trials = DbContext().Query<Trial>("Select * from Trial");
            return trials;
        }
        
        public override Task<int> AddAsync(Trial entity)
        {
            return base.AddAsync(entity);
        }
        
        public void UpdateTrialStatus(int _trialEzid)
        {
            DbContext().Execute("UPDATE Trial set StatusCode = ? WHERE EZID = ?", "30", _trialEzid);
        }

        public Trial Get(int trialId)
        {
            var trial = DbContext().Query<Trial>("Select * from Trial where EZID = ?", trialId).FirstOrDefault();
            return trial;
        }
        
    }
}
