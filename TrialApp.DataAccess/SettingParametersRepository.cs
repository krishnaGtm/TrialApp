using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enza.DataAccess;
using TrialApp.Common;
using TrialApp.Entities.Transaction;
using SQLite;

namespace TrialApp.DataAccess
{
    public class SettingParametersRepository : Repository<SettingParameters>
    {
        public SettingParametersRepository() : base(DbPath.GetTransactionDbPath())
        {
        }
        public SettingParametersRepository(SQLiteAsyncConnection connection) : base(connection)
        {
        }

        public async Task<List<SettingParameters>> GetAllAsync()
        {
            var settingparams = await DbContextAsync().QueryAsync<SettingParameters>("Select * from SettingParameters");
            return settingparams;
        }

        public List<SettingParameters> GetList()
        {
            var settingparams = DbContext().Query<SettingParameters>("select * from SettingParameters");
            return settingparams;
        }

        public void UpdateSettingParams(string field, string fieldvalue)
        {
            switch (field)
            {
                case "endpoint":
                    DbContext().Execute("update SettingParameters set Endpoint = ?", fieldvalue);
                    break;

                case "filter":
                    DbContext().Execute("update SettingParameters set Filter = ?", fieldvalue);
                    break;
            }

        }
    }
}
