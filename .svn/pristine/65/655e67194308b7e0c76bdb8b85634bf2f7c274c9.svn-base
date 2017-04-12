using Enza.DataAccess;
using TrialApp.Common;
using SQLite;
using TrialApp.Entities.Master;
using System.Collections.Generic;
using System;

namespace TrialApp.DataAccess
{
    public class TraitValueRepository : Repository<TraitValue>
    {
        public TraitValueRepository() : base(DbPath.GetMasterDbPath())
        {
        }
        public TraitValueRepository(SQLiteAsyncConnection connection) : base(connection)
        {
        }

        public List<TraitValue> GetTraitValueWithID(int traitID)
        {
            return DbContext().Query<TraitValue>("select SortingOrder, TraitValueCode ||  '  : '  || TraitValueName as 'TraitValueName' ,  TraitValueCode from  TraitValue where TraitID = ? ORDER BY SortingOrder", traitID);
        }
    }
}
