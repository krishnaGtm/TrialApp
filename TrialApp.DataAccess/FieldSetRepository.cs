using Enza.DataAccess;
using SQLite;
using System.Collections.Generic;
using TrialApp.Common;
using TrialApp.Entities.Master;

namespace TrialApp.DataAccess
{
    public class FieldSetRepository : Repository<FieldSet>
    {
        public FieldSetRepository() : base(DbPath.GetMasterDbPath())
        {
        }
        public FieldSetRepository(SQLiteAsyncConnection connection) : base(connection)
        {
        }

        public void InsertAll(List<FieldSet> fieldsetList, SQLiteConnection db)
        {
            db.InsertAll(fieldsetList);
        }

        public List<FieldSet> Get(string cropCode, int? cropGroupId)
        {
            var fieldSet = DbContext().Query<FieldSet>(@"SELECT * FROM FieldSet WHERE [NormalTrait] = 1 and ((CropGroupID = ? and CropGroupLevel = 1)  or ( CropCode = ? and CropLevel = 1)) ORDER BY FieldSetCode",
                                        cropGroupId, cropCode);
            return fieldSet;
        }

        public List<FieldSet> GetProperty(string cropCode, int? cropGroupId)
        {
            var propertySet = DbContext().Query<FieldSet>(@"SELECT * FROM FieldSet WHERE [NormalTrait] = 0 and [Property] = 1 and ((CropGroupID = ? and CropGroupLevel = 1)  or ( CropCode = ? and CropLevel = 1)) ORDER BY FieldSetCode",
                                            cropGroupId, cropCode);
            return propertySet;
        }

    }
}
