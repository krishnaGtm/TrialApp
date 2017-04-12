using System;
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
    public class DefaultFieldSetService
    {
        private DefaultFieldSetRepository _repoAsync;
        private readonly DefaultFieldSetRepository _repoSync;
        private readonly CropRdService _cropRdService;

        public DefaultFieldSetService()
        {
            _repoAsync = new DefaultFieldSetRepository(new SQLiteAsyncConnection(DbPath.GetTransactionDbPath()));
            _repoSync = new DefaultFieldSetRepository();
            _cropRdService = new CropRdService();
        }

        public void SaveDefaultFs(string crop, int fieldsetId)
        {
            var cropData = _cropRdService.GetCropRd(crop);
            var cropGroupId = cropData.CropGroupID;
            _repoSync.SaveDefaultFieldset(crop, cropGroupId, fieldsetId);
        }

        public DefaultFieldSet GetDefaultFs(string crop)
        {
            var cropData = _cropRdService.GetCropRd(crop);
            var cropGroupId = cropData.CropGroupID;
            return _repoSync.GetDefaultFieldset(cropGroupId);
        }
    }
}
