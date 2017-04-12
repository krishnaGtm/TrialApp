
using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TrialApp.Common;
using TrialApp.DataAccess;
using TrialApp.Entities.Master;

namespace TrialApp.Services
{

    public class TraitValueService
    {
        private TraitValueRepository repo;
        private TraitValueRepository repo1;

        public TraitValueService()
        {
            repo = new TraitValueRepository(new SQLiteAsyncConnection(DbPath.GetMasterDbPath()));
            repo1 = new TraitValueRepository();
        }
        public ObservableCollection<TraitValue> GetTraitValueWithID(int traitID)
        {
            var cmbnull = new TraitValue { TraitValueCode = "" ,TraitValueName= " "};
            var traitValueList =  repo1.GetTraitValueWithID(traitID);
            traitValueList.Insert(0, cmbnull);
            return new ObservableCollection<TraitValue>(traitValueList);
        }
    }

}
