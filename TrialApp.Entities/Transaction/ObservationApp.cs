using System;

namespace TrialApp.Entities.Transaction
{
    public class ObservationApp
    {
        public string EZID { get; set; }
        public int TraitID { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }
        public string UserIDCreated { get; set; }
        public string UserIDUpdated { get; set; }
        public string ObsValueChar { get; set; }
        public int? ObsValueInt { get; set; }
        public decimal? ObsValueDec { get; set; }
        public string ObsValueDate { get; set; }
        public bool Modified { get; set; }
        public int? ObservationId { get; set; }
    }
}
