
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using TrialApp.DataAccess;
using TrialApp.Entities.Master;

namespace TrialApp.Services
{
    public class FieldSetService
    {
        private FieldSetRepository repo;
        private readonly CropRdService _cropRdService;

        public FieldSetService()
        {
            repo = new FieldSetRepository();
            _cropRdService = new CropRdService();
        }

        internal void Update(List<FieldSet> fieldsetList, SQLiteConnection db)
        {
            repo.InsertAll(fieldsetList, db);
        }

        public List<FieldSet> GetFieldSetList(string cropCode)
        {
            var cropInfo = _cropRdService.GetCropRd(cropCode);
            var fieldSetList = repo.Get(cropCode, cropInfo.CropGroupID);
            return fieldSetList;
        }

        public List<FieldSet> GetPropertySetList(string cropCode)
        {
            var cropInfo = _cropRdService.GetCropRd(cropCode);
            var propertySetList = repo.GetProperty(cropCode,cropInfo.CropGroupID);
            return propertySetList;
        }
    }
}
