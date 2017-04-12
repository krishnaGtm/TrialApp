using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enza.DataAccess;
using SQLite;
using TrialApp.Common;
using TrialApp.DataAccess;
using TrialApp.Entities.Master;

namespace TrialApp.Services
{
    public class TraitService
    {
        private TraitRepository _repo;
        private TraitRepository _repoSync;
        public TraitService()
        {
            _repo = new TraitRepository(new SQLiteAsyncConnection(DbPath.GetMasterDbPath()));
            _repoSync = new TraitRepository();
        }

        public List<Trait> GetTraitList(int fieldsetId)
        {
            var traitInFieldsets = _repoSync.GetTraitsFromFieldset(fieldsetId);
            return traitInFieldsets;
        }
    }
}

