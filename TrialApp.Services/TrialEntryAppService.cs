using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TrialApp.Common;
using TrialApp.DataAccess;
using TrialApp.Entities.Transaction;

namespace TrialApp.Services
{
    public class TrialEntryAppService
    {
        private readonly TrialEntryAppRepository _repoAsync;
        private readonly TrialEntryAppRepository _repoSync;
        private readonly TrialService _trialService;

        public TrialEntryAppService()
        {
            _repoAsync = new TrialEntryAppRepository(new SQLiteAsyncConnection(DbPath.GetTransactionDbPath()));
            _repoSync = new TrialEntryAppRepository();
            _trialService = new TrialService();
        }

        public async Task<List<TrialEntryApp>> GetVarietiesListAsync(int ezid)
        {
            var varietyList = await _repoAsync.GetVarietiesListAsync(ezid);
            return varietyList;
        }

        public async Task<int> AddVariety(TrialEntryApp TE, Relationship R)
        {
            var operation = await _repoAsync.AddTrialEntryWithRelationshipAsync(TE, R);
            if (operation > 0)
                _trialService.UpdateTrialStatus(R.EZID1);
            return operation;
        }
    }
}
