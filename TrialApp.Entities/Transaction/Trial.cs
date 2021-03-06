﻿namespace TrialApp.Entities.Transaction
{
    public class Trial
    {
        public int EZID { get; set; }
        public string CropCode { get; set; }
        public string TrialName { get; set; }
        public int TrialTypeID { get; set; }
        public string CountryCode { get; set; }
        public int TrialRegionID { get; set; }
        public string CropSegmentCode { get; set; }
        public int StatusCode { get; set; }
        public int Order { get; set; }
        public string SelectedRecordID { get; set; }
    }

    public class TrialEntryApp
    {
        public string EZID { get; set; }
        public string CropCode { get; set; }
        public string FieldNumber { get; set; }
        public string EZIDVariety { get; set; }
        public int? VarietyNr { get; set; }
        public string CropCodeVariety { get; set; }
        public string VarietyName { get; set; }
        public string Enumber { get; set; }
        public string CropSegmentCode { get; set; }
        public string ProductSegmentCode { get; set; }

        public string ProductStatus { get; set; }
        public string ResistanceHR { get; set; }
        public string ResistanceIR { get; set; }
        public string ResistanceT { get; set; }
        public string MasterNr { get; set; }
        public bool Modified { get; set; }
        public bool NewRecord { get; set; }
    }
}
