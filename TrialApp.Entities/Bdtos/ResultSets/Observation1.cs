namespace TrialApp.Entities.Bdtos.ResultSets
{
    public class Observation1
    {
        private string _dateString;
        public int trialEntryEZID { get; set; }
        public int traitID { get; set; }
        public string userIDCreated { get; set; }
        public string dateUpdated { get; set; }
        public string dateCreated { get; set; }
        public string observationValue { get; set; }
        public string userIDUpdated { get; set; }

    }
}
