﻿using System;
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
    public class ObservationAppService
    {
        private ObservationAppRepository _repoAsync;
        private ObservationAppRepository _repoSync;
        public ObservationAppService()
        {
            _repoAsync = new ObservationAppRepository(new SQLiteAsyncConnection(DbPath.GetTransactionDbPath()));
            _repoSync = new ObservationAppRepository();
        }

        public ObservationApp GetObservationData(string ezId, int traitId)
        {
            var obsData = _repoSync.GetObservations(ezId, traitId);
            return obsData;
        }
        public async Task<List<ObservationApp>> GetObservationDataAll(string ezId, string traitId)
        {
            var obsData = await _repoAsync.GetObservationAll(ezId, traitId);
            return obsData;
        }

        public async Task UpdateOrSaveObservationData(ObservationApp observation)
        {
            var obsVal = await _repoAsync.GetObservationWithDateAndUser(observation);
            if (obsVal.Any())
                await _repoAsync.UpdateObservationValue(observation);
            else
            {
                observation.DateCreated = DateTime.UtcNow.Date.ToString("yyyy-MM-dd");
                observation.Modified = true;
                await _repoAsync.AddAsync(observation);
            }
        }
    }
}
