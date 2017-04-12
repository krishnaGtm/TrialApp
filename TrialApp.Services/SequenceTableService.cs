using SQLite;
using System.Linq;
using System.Threading.Tasks;
using TrialApp.DataAccess;
using TrialApp.Entities.Master;

namespace TrialApp.Services
{

    public class SequenceTableService
    {
        private SequenceTableRepository repo;

        public SequenceTableService()
        {
            repo = new SequenceTableRepository();
        }
        public async Task<int> GetMaxSequence(string tableName, SQLiteConnection db)
        {
            if (tableName == "ProgramFieldSetField")
                tableName = "ProgramFieldSetFields";
            int maxVal;
            var data =
               db.Query<SequenceTable>("SELECT Sequence from SequenceTable WHERE TableName = ?", tableName)
                   .FirstOrDefault();
            if (data == null)
            {
                var newdata = new SequenceTable();
                newdata.TableName = tableName;
                newdata.Sequence = 0;
                repo.DbContext().InsertOrReplace(newdata);
                maxVal = 0;
            }
            else
                maxVal = data.Sequence;
            return maxVal;
        }

        public async Task setMaxVal(string tableName, int maxVal, SQLiteConnection db)
        {
            if (tableName == "ProgramFieldSetField")
                tableName = "ProgramFieldSetFields";
            var newseq = new SequenceTable();
            newseq.TableName = tableName;
            newseq.Sequence = maxVal;
            db.InsertOrReplace(newseq);
        }


    }

}
