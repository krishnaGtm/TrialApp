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
    public class DefaultFieldSetRepository : Repository<DefaultFieldSet>
    {
        public DefaultFieldSetRepository() : base(DbPath.GetTransactionDbPath())
        {
        }

        public DefaultFieldSetRepository(SQLiteAsyncConnection connection) : base(connection)
        {
        }

        public void SaveDefaultFieldset(string crop, int? cropgroupId, int fieldsetId)
        {
            var defaultFs = new DefaultFieldSet()
            {
                CropCode = crop,
                CropGroupId = cropgroupId,
                Fieldset = fieldsetId.ToString()
            };
            DbContext().InsertOrReplace(defaultFs);
        }

        public DefaultFieldSet GetDefaultFieldset(int? cropGroupId)
        {
            return DbContext().Query<DefaultFieldSet>("Select * from DefaultFieldset where CropGroupID =? ", cropGroupId).FirstOrDefault();
        }
    }
}
