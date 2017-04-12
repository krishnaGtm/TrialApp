
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrialApp.Common;
using TrialApp.DataAccess;
using TrialApp.Entities.Transaction;

namespace TrialApp.Services
{

    public class SettingParametersService
    {
        private SettingParametersRepository repo;

        public SettingParametersService()
        {
            repo = new SettingParametersRepository();
        }
        public async Task<List<SettingParameters>> GetAllAsync()
        {
            var orgiList = await repo.GetAllAsync();
            return orgiList;
        }

        public List<SettingParameters> GetParamsList()
        {
            var settingparams = repo.GetList();
            return settingparams;
        }

        public void UpdateParams(string field, string endpoint)
        {
            repo.UpdateSettingParams(field, endpoint);
        }
    }

}
