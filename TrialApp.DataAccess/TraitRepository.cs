using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enza.DataAccess;
using SQLite;
using TrialApp.Common;
using TrialApp.Entities.Master;

namespace TrialApp.DataAccess
{
    public class TraitRepository : Repository<Trait>
    {
        public TraitRepository(SQLiteAsyncConnection connection) : base(connection)
        {
        }

        public TraitRepository() : base(DbPath.GetMasterDbPath())
        {
        }

        public Trait GetTraits(int traitId)
        {
            var trait = DbContext().Query<Trait>("select * from Trait where TraitID = ?", traitId).FirstOrDefault();
            return trait;
        }

        public List<Trait> GetTraitsFromFieldset(int fieldsetId)
        {
            var traitList =
                DbContext()
                    .Query<Trait>(
                        "select * from TraitInFieldSet join Trait on TraitInFieldSet.TraitID = Trait.TraitID where TraitInFieldSet.FieldSetID = ? order by TraitInFieldSet.SortingOrder ",
                        fieldsetId);
            return traitList;
        }
    }
}
