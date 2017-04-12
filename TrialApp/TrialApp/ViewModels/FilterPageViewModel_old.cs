using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrialApp.Entities.Master;
using TrialApp.Entities.Transaction;
using TrialApp.Services;
using TrialApp.ViewModels.Inetrfaces;
using TrialApp.Views;
using Xamarin.Forms;

namespace TrialApp.ViewModels
{
    public class FilterPageViewModel_old : BaseViewModel
    {

        #region private variables

        private readonly CropRdService _cropRdService;
        private readonly CountryService _countryService;
        private readonly TrialTypeService _trialTypeService;
        private readonly TrialRegionService _trialRegionService;
        private readonly SettingParametersService _settingParametersService;
        private readonly CropSegmentService _cropSegmentService;

        private bool _disableFilter;

        private List<CropRD> _cropList;
        private List<Country> _countryList;
        private List<TrialRegion> _trialRegionList;
        private List<TrialType> _trialTypeList;
        private List<CropSegment> _cropSegmentList;

        private int _selectedTrialType;
        private string _selectedCrop;
        private string _selectedCountry;
        private int _selectedTrialRegion;
        private bool _toggleFilter;
        private string _selectedCropSegment;

        #endregion

        #region public properties
        public List<TrialType> TrialTypeListFromDb { get; set; }
        public List<CropRD> CropListFromDb { get; set; }
        public List<CropSegment> CropSegmentListFromDb { get; set; }
        public List<TrialRegion> TrialRegionListFromDb { get; set; }
        public List<Country> CountryListFromDb { get; set; }
        public string SelectedCropSegment
        {
            get { return _selectedCropSegment; }
            set { _selectedCropSegment = value; OnPropertyChanged(); }
        }

        public List<CropSegment> CropSegmentList
        {
            get { return _cropSegmentList; }
            set { _cropSegmentList = value; OnPropertyChanged(); }
        }

        public List<Entities.Transaction.Trial> TrialList { get; set; }

        public int SelectedTrialType
        {
            get { return _selectedTrialType; }
            set { _selectedTrialType = value; OnPropertyChanged(); }
        }

        public List<TrialType> TrialTypeList
        {
            get { return _trialTypeList; }
            set { _trialTypeList = value; OnPropertyChanged(); }
        }

        public bool ToggleFilter
        {
            get { return _toggleFilter; }
            set { _toggleFilter = value; OnPropertyChanged(); }
        }

        public List<CropRD> CropList
        {
            get { return _cropList; }
            set { _cropList = value; OnPropertyChanged(); }
        }

        public List<Country> CountryList
        {
            get { return _countryList; }
            set { _countryList = value; OnPropertyChanged(); }
        }

        public List<TrialRegion> TrialRegionList
        {
            get { return _trialRegionList; }
            set { _trialRegionList = value; OnPropertyChanged(); }
        }

        public int SelectedTrialRegion
        {
            get { return _selectedTrialRegion; }
            set { _selectedTrialRegion = value; OnPropertyChanged(); }
        }

        public string SelectedCountry
        {
            get { return _selectedCountry; }
            set { _selectedCountry = value; OnPropertyChanged(); }
        }

        public string SelectedCrop
        {
            get { return _selectedCrop; }
            set { _selectedCrop = value; OnPropertyChanged(); }
        }

        public readonly SaveFilterService SaveFilterService;
        public bool DisableFilter
        {
            get { return _disableFilter; }
            set { _disableFilter = value; OnPropertyChanged(); }
        }

        public ICommand ApplyFilterCommand { get; set; }

        public ICommand CancelFilterCommand { get; set; }

        #endregion

        public FilterPageViewModel_old()
        {
            _cropRdService = new CropRdService();
            _countryService = new CountryService();
            _trialTypeService = new TrialTypeService();
            _trialRegionService = new TrialRegionService();
            _settingParametersService = new SettingParametersService();
            _cropSegmentService = new CropSegmentService();

            SaveFilterService = new SaveFilterService();

            TrialTypeList = new List<TrialType>();
            CropList = new List<CropRD>();
            CropSegmentList = new List<CropSegment>();
            TrialRegionList = new List<TrialRegion>();
            CountryList = new List<Country>();

            ApplyFilterCommand = new ApplyfilterOperation();
            CancelFilterCommand = new CancelOperation();

            //Load default values
            SelectedTrialType = 0;
            SelectedCrop = "0";
            SelectedCropSegment = "0";
            SelectedTrialRegion = 0;
            SelectedCountry = "0";

            var settingparams = _settingParametersService.GetParamsList().Single();
            ToggleFilter = settingparams.Filter;
        }

        public FilterPageViewModel_old(IDependencyService dependencyService)
        {
            DependencyService = dependencyService;
        }

        public async void LoadControls()
        {
            // commas separated list to limit data to only downloaded data
            var downloadedList = string.Join(",", TrialList.Select(x => x.TrialTypeID).Distinct());
            TrialTypeListFromDb = await _trialTypeService.GetTrialTypeListAsync(downloadedList);

            downloadedList = string.Join(",", TrialList.Select(x => "'" + x.CropCode + "'").Distinct());
            CropListFromDb = await _cropRdService.GetCropListAsync(downloadedList);

            downloadedList = string.Join(",", TrialList.Select(x => "'" + x.CropSegmentCode + "'").Distinct());
            CropSegmentListFromDb = await _cropSegmentService.GetCropSegmentListAsync(downloadedList);

            downloadedList = string.Join(",", TrialList.Select(x => "'" + x.TrialRegionID + "'").Distinct()); ;
            TrialRegionListFromDb = await _trialRegionService.GetTrialRegionListAsync(downloadedList);

            downloadedList = string.Join(",", TrialList.Select(x => "'" + x.CountryCode + "'").Distinct());
            CountryListFromDb = await _countryService.GetCountryListAsync(downloadedList);

            TrialTypeListFromDb.Insert(0, new TrialType() { TrialTypeID = 0, TrialTypeName = "All" });
            CropListFromDb.Insert(0, new CropRD() { CropCode = "0", CropName = "All" });
            CropSegmentListFromDb.Insert(0, new CropSegment() { CropSegmentCode = "0", CropSegmentName = "All" });
            TrialRegionListFromDb.Insert(0, new TrialRegion() { TrialRegionID = 0, TrialRegionName = "All" });
            CountryListFromDb.Insert(0, new Country() { CountryCode = "0", CountryName = "All" });

            // Assign original list from db to display list
            TrialTypeList = TrialTypeListFromDb;
            CropList = CropListFromDb;
            CropSegmentList = CropSegmentListFromDb;
            TrialRegionList = TrialRegionListFromDb;
            CountryList = CountryListFromDb;

            var savedFilter = await SaveFilterService.GetSaveFilterAsync();
            if (savedFilter == null) return;

            foreach (var val in savedFilter)
            {
                switch (val.Field.ToLower())
                {
                    case "trialtypeid":
                        {
                            var data = 0;
                            int.TryParse(val.FieldValue, out data);
                            SelectedTrialType = data;
                        }
                        break;
                    case "cropcode":
                        SelectedCrop = val.FieldValue;
                        break;
                    case "cropsegmentcode":
                        SelectedCropSegment = val.FieldValue;
                        break;
                    case "trialregionid":
                        {
                            var data = 0;
                            int.TryParse(val.FieldValue, out data);
                            SelectedTrialRegion = data;
                        }
                        break;
                    case "countrycode":
                        SelectedCountry = val.FieldValue;
                        break;

                }
            }
        }

        public void ToggleFilterSetting(string isToggled)
        {
            _settingParametersService.UpdateParams("filter", isToggled);
        }

        public void ReloadFilter(string styleId, string selectedValue)
        {
            switch (styleId)
            {
                case "TrialType":
                    {
                        var cropgroupid = TrialTypeList.Find(x => x.TrialTypeID == Convert.ToInt32(selectedValue)).CropGroupID;
                        CropList = selectedValue == "0" ? CropListFromDb : CropListFromDb.Where(x => (x.CropGroupID == cropgroupid || x.CropCode == "0")).ToList();
                        break;
                    }
                case "Crop":
                    {
                        CropSegmentList = selectedValue == "0" ? CropSegmentListFromDb : CropSegmentListFromDb.Where(x => (x.CropCode == selectedValue || x.CropSegmentCode == "0")).ToList();
                        TrialRegionList = selectedValue == "0" ? TrialRegionListFromDb : TrialRegionListFromDb.Where(x => (x.CropCode == selectedValue || x.TrialRegionID == 0)).ToList();

                        break;
                    }
                case "CropSegment":
                    {
                        //var crop = CropSegmentList.Find(x => x.CropSegmentCode == selectedValue).CropCode;

                        //CropList = selectedValue == "0" ? CropListFromDb : CropListFromDb.Where(x => (x.CropCode == crop || x.CropCode == "0")).ToList();
                        //TrialRegionList = selectedValue == "0" ? TrialRegionListFromDb : TrialRegionListFromDb.Where(x => (x.CropCode == crop || x.TrialRegionID == 0)).ToList();

                        break;
                    }
                case "TrialRegion":
                    {
                        //var crop = TrialRegionList.Find(x => x.TrialRegionID == Convert.ToInt32(selectedValue)).CropCode;

                        //CropList = selectedValue == "0" ? CropListFromDb : CropListFromDb.Where(x => (x.CropCode == crop || x.CropCode == "0")).ToList();
                        //CropSegmentList = selectedValue == "0" ? CropSegmentListFromDb : CropSegmentListFromDb.Where(x => (x.CropCode == crop || x.CropSegmentCode == "0")).ToList();

                        break;
                    }
                case "Country":

                    break;

            }
        }
    }

    //internal class ApplyfilterOperation : ICommand
    //{
    //    public bool CanExecute(object parameter)
    //    {
    //        return true;
    //    }

    //    public async void Execute(object parameter)
    //    {
    //        var vm = parameter as FilterPageViewModel;
    //        if (vm == null) return;

    //        var savefilterList = new List<SaveFilter>
    //        {
    //            new SaveFilter() {Field = "TrialTypeId", FieldValue = vm.SelectedTrialType.ToString()},
    //            new SaveFilter() {Field = "CropCode", FieldValue = vm.SelectedCrop},
    //            new SaveFilter() {Field = "CropSegmentCode", FieldValue = vm.SelectedCropSegment},
    //            new SaveFilter() {Field = "TrialRegionId", FieldValue = vm.SelectedTrialRegion.ToString()},
    //            new SaveFilter() {Field = "CountryCode", FieldValue = vm.SelectedCountry}
    //        };

    //        await vm.SaveFilterService.SaveFilterAsync(savefilterList);
    //        await App.MainNavigation.PopAsync();
    //    }

    //    public event EventHandler CanExecuteChanged;
    //}

    public class FilterSet : ObservableViewModel
    {
        public string FilterLabel { get; set; }

        private List<CodeValuePair> _filterValue;
        private string _selectedValue;

        public List<CodeValuePair> FilterValue
        {
            get { return _filterValue; }
            set { _filterValue = value; OnPropertyChanged(); }
        }

        public string SelectedPickerValue
        {
            get { return _selectedValue; }
            set { _selectedValue = value; OnPropertyChanged(); }
        }
    }

    public class CodeValuePair
    {
        public string Code { get; set; }
        public string Value { get; set; }
    }
}
