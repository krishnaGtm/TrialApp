using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TrialApp.Common.Extensions;
using TrialApp.Entities.Master;
using TrialApp.Entities.Transaction;
using TrialApp.Services;
using TrialApp.ViewModels.Inetrfaces;

namespace TrialApp.ViewModels
{
    public class FilterPageViewModel : BaseViewModel
    {
        private bool _disableFilter; 
        public ObservableCollection<TrialType> TrialTypeListFromDb { get; set; }
        public ObservableCollection<CropRD> CropListFromDb { get; set; }
        public ObservableCollection<CropSegment> CropSegmentListFromDb { get; set; }
        public ObservableCollection<TrialRegion> TrialRegionListFromDb { get; set; }
        public ObservableCollection<Country> CountryListFromDb { get; set; }
        public List<Entities.Transaction.Trial> TrialList { get; set; }
        public ICommand ApplyFilterCommand { get; set; }

        public ICommand CancelFilterCommand { get; set; }
        private bool _toggleFilter;
        public bool ToggleFilter
        {
            get { return _toggleFilter; }
            set { _toggleFilter = value; OnPropertyChanged(); }
        }
        public bool DisableFilter
        {
            get { return _disableFilter; }
            set { _disableFilter = value; OnPropertyChanged(); }
        }
        public bool IsInitialLoad { get; set; }
        private readonly CropRdService _cropRdService;
        private readonly CountryService _countryService;
        private readonly TrialTypeService _trialTypeService;
        private readonly TrialRegionService _trialRegionService;
        private readonly SettingParametersService _settingParametersService;
        private readonly CropSegmentService _cropSegmentService;
        public readonly SaveFilterService SaveFilterService;
        public FilterPageViewModel()
        {
            _cropRdService = new CropRdService();
            _countryService = new CountryService();
            _trialTypeService = new TrialTypeService();
            _trialRegionService = new TrialRegionService();
            _settingParametersService = new SettingParametersService();
            _cropSegmentService = new CropSegmentService();
            SaveFilterService = new SaveFilterService();

            ApplyFilterCommand = new ApplyfilterOperation();
            CancelFilterCommand = new CancelOperation();
            var settingparams = _settingParametersService.GetParamsList().Single();
            ToggleFilter = settingparams.Filter;
        }

        public FilterPageViewModel(IDependencyService dependencyService)
        {
            DependencyService = dependencyService;
        }

        #region crop
        private ObservableCollection<MyType> _cropList;

        public ObservableCollection<MyType> CropList
        {
            get { return _cropList; }
            set
            {
                _cropList = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<MyType> _cropList1;

        public ObservableCollection<MyType> CropList1
        {
            get { return _cropList1; }
            set
            {
                _cropList1 = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<MyType> _cropSelected;

        public ObservableCollection<MyType> CropSelected
        {
            get { return _cropSelected; }
            set
            {
                OnPropertyChanged();
                if (Equals(value, _cropSelected)) return;
                if (_cropSelected != null)
                    _cropSelected.CollectionChanged -= CropItemsCollectionChanged;
                _cropSelected = value;
                if (value != null)
                    _cropSelected.CollectionChanged += CropItemsCollectionChanged;
                //OnPropertyChanged();
            }
        }
        #endregion
        #region TrialType
        private ObservableCollection<MyType> _trialTypeList;

        public ObservableCollection<MyType> TrialTypeList
        {
            get { return _trialTypeList; }
            set
            {
                _trialTypeList = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<MyType> _trialTypeList1;

        public ObservableCollection<MyType> TrialTypeList1
        {
            get { return _trialTypeList1; }
            set
            {
                _trialTypeList1 = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<MyType> _trialTypeSelected;

        public ObservableCollection<MyType> TrialTypeSelected
        {
            get { return _trialTypeSelected; }
            set
            {
                OnPropertyChanged();
                
                if (Equals(value, _trialTypeSelected)) return;
                if (_trialTypeSelected != null)
                    _trialTypeSelected.CollectionChanged -= TrialTypeItemsCollectionChanged;
                _trialTypeSelected = value;
                if (value != null)
                    _trialTypeSelected.CollectionChanged += TrialTypeItemsCollectionChanged;
            }
        }
        #endregion
        #region cropsegment
        private ObservableCollection<MyType> _cropSegmentList;

        public ObservableCollection<MyType> CropSegmentList
        {
            get { return _cropSegmentList; }
            set
            {
                _cropSegmentList = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<MyType> _cropSegmentList1;

        public ObservableCollection<MyType> CropSegmentList1
        {
            get { return _cropSegmentList1; }
            set
            {
                _cropSegmentList1 = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<MyType> _cropSegmentSelected;

        public ObservableCollection<MyType> CropSegmentSelected
        {
            get { return _cropSegmentSelected; }
            set
            {
                OnPropertyChanged();
                if (Equals(value, _cropSegmentSelected)) return;
                if (_cropSegmentSelected != null)
                    _cropSegmentSelected.CollectionChanged -= CropSegmentItemsCollectionChanged;
                _cropSegmentSelected = value;
                if (value != null)
                    _cropSegmentSelected.CollectionChanged += CropSegmentItemsCollectionChanged;

                
            }
        }
        #endregion
        #region trialregion
        private ObservableCollection<MyType> _trialRegionList;
        public ObservableCollection<MyType> TrialRegionList
        {
            get { return _trialRegionList; }
            set
            {
                _trialRegionList = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<MyType> _trialRegionList1;
        public ObservableCollection<MyType> TrialRegionList1
        {
            get { return _trialRegionList1; }
            set
            {
                _trialRegionList1 = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<MyType> _trialRegionSelected;
        public ObservableCollection<MyType> TrialRegionSelected
        {
            get { return _trialRegionSelected; }
            set
            {
                OnPropertyChanged();
                if (Equals(value, _trialRegionSelected)) return;
                if (_trialRegionSelected != null)
                    _trialRegionSelected.CollectionChanged -= TrialRegionItemsCollectionChanged;
                _trialRegionSelected = value;
                if (value != null)
                    _trialRegionSelected.CollectionChanged += TrialRegionItemsCollectionChanged;
                
            }
        }
        #endregion
        #region Country
        private ObservableCollection<MyType> _countrylist;
        public ObservableCollection<MyType> CountryList
        {
            get { return _countrylist; }
            set
            {
                _countrylist = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<MyType> _countrylist1;
        public ObservableCollection<MyType> CountryList1
        {
            get { return _countrylist1; }
            set
            {
                _countrylist1 = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<MyType> _countrySelected;
        public ObservableCollection<MyType> CountrySelected
        {
            get { return _countrySelected; }
            set
            {
                OnPropertyChanged();
                if (Equals(value, _countrySelected)) return;
                if (_countrySelected != null)
                    _countrySelected.CollectionChanged -= CountryItemsCollectionChanged;
                _countrySelected = value;
                if (value != null)
                    _countrySelected.CollectionChanged += CountryItemsCollectionChanged;
                //OnPropertyChanged(nameof(CountrySelected));
                
            }
        }
        #endregion

        public void ReloadFilter(string styleId, string selectedValue)
        {
            var filteredData = Enumerable.Empty<string>();
            filteredData = selectedValue.Split(',').Select(x => x.Trim());
            switch (styleId)
            {
                case "TrialType":
                    IEnumerable<CropRD> filteredCroplist;
                    if (CropListFromDb == null)
                        break;
                    var croplist = CropListFromDb.ToList();
                    if (!string.IsNullOrWhiteSpace(selectedValue))
                        filteredCroplist = croplist.Where(x => filteredData.Contains(x.CropGroupID.ToText()));
                    else
                        filteredCroplist = CropListFromDb;
                    CropList = new ObservableCollection<MyType>(filteredCroplist.Select(x => new MyType
                    {
                        Id = x.CropCode,
                        Name = x.CropName
                    }).ToList());
                    break;
                case "Crop":
                    IEnumerable<CropSegment> filteredCropsegment;
                    IEnumerable<TrialRegion> filteredtrialRegion;
                    if (CropSegmentListFromDb == null)
                        break;
                    var cropSegment = CropSegmentListFromDb.ToList();
                    if (!string.IsNullOrWhiteSpace(selectedValue))
                        filteredCropsegment = cropSegment.Where(x => filteredData.Contains(x.CropCode));
                    else
                        filteredCropsegment = CropSegmentListFromDb;
                    CropSegmentList = new ObservableCollection<MyType>(filteredCropsegment.Select(x => new MyType
                    {
                        Id = x.CropSegmentCode,
                        Name = x.CropSegmentName
                    }).ToList());

                    var trialRegion = TrialRegionListFromDb.ToList();
                    if (!string.IsNullOrWhiteSpace(selectedValue))
                        filteredtrialRegion = trialRegion.Where(x => filteredData.Contains(x.CropCode));
                    else
                        filteredtrialRegion = TrialRegionListFromDb;
                    TrialRegionList = new ObservableCollection<MyType>(filteredtrialRegion.Select(x => new MyType
                    {
                        Id = x.TrialRegionID.ToText(),
                        Name = x.TrialRegionName

                    }).ToList());
                    break;
                case "CropSegment":
                    break;
                case "TrialRegion":
                    break;
                case "Country":
                    break;

            }
        }

        public void ReloadFilter1(string styleId, string selectedValueTrialType, string selectedValueCrop, string selectedValueCropSegment, string selectedValueTrialRegion, string SelectedValueCountry)
        {
            //List<Entities.Transaction.Trial> filteredList = TrialList;
            //var filteredDataTrialType = Enumerable.Empty<string>();
            //var filteredDataCrop = Enumerable.Empty<string>();
            //var filteredDataCropSegment = Enumerable.Empty<string>();
            //var filteredDataTrialRegion = Enumerable.Empty<string>();
            //var filteredDataCountry = Enumerable.Empty<string>();
            //filteredDataTrialType = selectedValueTrialType.Split(',').Select(x => x.Trim());
            //filteredDataCrop = selectedValueCrop.Split(',').Select(x => x.Trim());
            //filteredDataCropSegment = selectedValueCropSegment.Split(',').Select(x => x.Trim());
            //filteredDataTrialRegion = selectedValueTrialRegion.Split(',').Select(x => x.Trim());
            //filteredDataCountry = SelectedValueCountry.Split(',').Select(x => x.Trim());
            if (selectedValueCrop == null || selectedValueCropSegment == null || selectedValueTrialRegion == null || selectedValueTrialType == null || SelectedValueCountry == null)
                return;

            switch(styleId)
            {
                case "TrialType":
                    IndividualFilterItems("Crop", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    IndividualFilterItems("Country", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    IndividualFilterItems("CropSegment", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    IndividualFilterItems("TrialRegion", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    //IndividualFilterItems("TrialType", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    break;
                case "Crop":
                    //IndividualFilterItems("Crop", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    IndividualFilterItems("Country", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    IndividualFilterItems("CropSegment", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    IndividualFilterItems("TrialRegion", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    //IndividualFilterItems("TrialType", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    break;
                case "CropSegment":
                    //IndividualFilterItems("Crop", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    IndividualFilterItems("Country", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    //IndividualFilterItems("CropSegment", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    IndividualFilterItems("TrialRegion", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    //IndividualFilterItems("TrialType", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    break;
                case "TrialRegion":
                    //IndividualFilterItems("Crop", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    IndividualFilterItems("Country", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    //IndividualFilterItems("CropSegment", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    //IndividualFilterItems("TrialRegion", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    //IndividualFilterItems("TrialType", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    break;
                case "Country":
                    //IndividualFilterItems("Crop", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    //IndividualFilterItems("Country", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    //IndividualFilterItems("CropSegment", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    //IndividualFilterItems("TrialRegion", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    //IndividualFilterItems("TrialType", selectedValueTrialType, selectedValueCrop, selectedValueCropSegment, selectedValueTrialRegion, SelectedValueCountry);
                    break;
               
            }
            //if (!string.IsNullOrWhiteSpace(selectedValueTrialType))
            //{
            //    filteredList = filteredList.Where(x => filteredDataTrialType.Contains(x.TrialTypeID.ToText())).ToList();
            //}
            //if (!string.IsNullOrWhiteSpace(selectedValueCrop))
            //{
            //    filteredList = filteredList.Where(x => filteredDataCrop.Contains(x.CropCode)).ToList();
            //}
            //if (!string.IsNullOrWhiteSpace(selectedValueCropSegment))
            //{
            //    filteredList = filteredList.Where(x => filteredDataCropSegment.Contains(x.CropSegmentCode)).ToList();
            //}
            //if (!string.IsNullOrWhiteSpace(selectedValueTrialRegion))
            //{
            //    filteredList = filteredList.Where(x => filteredDataTrialRegion.Contains(x.TrialRegionID.ToText())).ToList();
            //}
            //if (!string.IsNullOrWhiteSpace(SelectedValueCountry))
            //{
            //    filteredList = filteredList.Where(x => filteredDataCountry.Contains(x.CountryCode)).ToList();
            //}
        }
        /// <summary>
        /// filter data for individual items based on selection provided
        /// </summary>
        /// <param name="filterFor">Item on which we want to get fitlered list </param>
        /// <param name="selectedValueTrialType">filtered trial type id in comma seperated format</param>
        /// <param name="selectedValueCrop">filtered crop id in comma seperated format</param>
        /// <param name="selectedValueCropSegment">filtered crop segment id in comma seperated format</param>
        /// <param name="selectedValueTrialRegion">filtered trial region id in comma seperated format</param>
        /// <param name="SelectedValueCountry">filtered country id in comma seperated format </param>
        /// <returns></returns>
        private void IndividualFilterItems(string filterFor, string selectedValueTrialType, string selectedValueCrop, string selectedValueCropSegment, string selectedValueTrialRegion, string SelectedValueCountry)
        {
            List<Entities.Transaction.Trial> filteredList = TrialList;
            var filteredData = Enumerable.Empty<string>();
            if (!string.IsNullOrWhiteSpace(selectedValueTrialType) && filterFor!= "TrialType")
            {
                filteredData = selectedValueTrialType.Split(',').Select(x => x.Trim());
                filteredList = filteredList.Where(x => filteredData.Contains(x.TrialTypeID.ToText())).ToList();
            }
            if (!string.IsNullOrWhiteSpace(selectedValueCrop) && filterFor != "Crop")
            {
                filteredData = selectedValueCrop.Split(',').Select(x => x.Trim());
                filteredList = filteredList.Where(x => filteredData.Contains(x.CropCode)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(selectedValueCropSegment) && filterFor != "CropSegment")
            {
                filteredData = selectedValueCropSegment.Split(',').Select(x => x.Trim());
                filteredList = filteredList.Where(x => filteredData.Contains(x.CropSegmentCode)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(selectedValueTrialRegion) && filterFor != "TrialRegion")
            {
                filteredData = selectedValueTrialRegion.Split(',').Select(x => x.Trim());
                filteredList = filteredList.Where(x => filteredData.Contains(x.TrialRegionID.ToText())).ToList();
            }
            if (!string.IsNullOrWhiteSpace(SelectedValueCountry) && filterFor != "Country")
            {
                filteredData = SelectedValueCountry.Split(',').Select(x => x.Trim());
                filteredList = filteredList.Where(x => filteredData.Contains(x.CountryCode)).ToList();
            }

            if(filterFor == "TrialType")
            {
                if (TrialTypeListFromDb == null)
                    return;
                var data = filteredList.Select(x => x.TrialTypeID.ToText());
                //var listitems = TrialTypeListFromDb.Where(x => data.Contains(x.TrialTypeID.ToText())).Select(x => new MyType
                //{
                //    Id = x.TrialTypeID.ToText(),
                //    Name = x.TrialTypeName
                //}).ToList();
                //foreach(var _items in listitems)
                //{
                //    if(TrialTypeList1.FirstOrDefault(x=>x.Id == _items.Id) != null)
                //    {
                //        if()
                //    }
                //}
                //TrialTypeSelected = null;
                TrialTypeList = new ObservableCollection<MyType>(TrialTypeListFromDb.Where(x => data.Contains(x.TrialTypeID.ToText())).Select(x => new MyType
                {
                    Id = x.TrialTypeID.ToText(),
                    Name = x.TrialTypeName
                }).ToList());
                if (!string.IsNullOrWhiteSpace(selectedValueTrialType))
                {
                    filteredData = selectedValueTrialType.Split(',').Select(x => x.Trim());
                    //TrialTypeSelected = null;
                    TrialTypeSelected = new ObservableCollection<MyType>(TrialTypeList.Where(x => filteredData.Contains(x.Id)));
                }


            }
            if (filterFor == "Crop")
            {
                if (CropListFromDb == null)
                    return;
                //filteredData = selectedValueCrop.Split(',').Select(x => x.Trim());
                var data = filteredList.Select(x => x.CropCode);
                //CropSelected = null;
                CropList = new ObservableCollection<MyType>(CropListFromDb.Where(x => data.Contains(x.CropCode)).Select(x => new MyType
                {
                    Id = x.CropCode,
                    Name = x.CropName
                }).ToList());
                if (!string.IsNullOrWhiteSpace(selectedValueCrop))
                {
                    filteredData = selectedValueCrop.Split(',').Select(x => x.Trim());
                    //CropSelected = null;
                    CropSelected = new ObservableCollection<MyType>(CropList.Where(x => filteredData.Contains(x.Id)));
                }

            }
            if (filterFor == "CropSegment")
            {
                if (CropSegmentListFromDb == null)
                    return;
                var data = filteredList.Select(x => x.CropSegmentCode);
                CropSegmentList = new ObservableCollection<MyType>(CropSegmentListFromDb.Where(x => data.Contains(x.CropSegmentCode)).Select(x => new MyType
                {
                    Id = x.CropSegmentCode,
                    Name = x.CropSegmentName
                }).ToList());
                //CropSegmentSelected = null;
                if (!string.IsNullOrWhiteSpace(selectedValueCropSegment))
                {
                    filteredData = selectedValueCropSegment.Split(',').Select(x => x.Trim());
                    //CropSegmentSelected = null;
                    CropSegmentSelected = new ObservableCollection<MyType>(CropSegmentList.Where(x => filteredData.Contains(x.Id)));
                }
            }
            if (filterFor == "TrialRegion")
            {
                if (TrialRegionListFromDb == null)
                    return;
                var data = filteredList.Select(x => x.TrialRegionID.ToText());
                TrialRegionList = new ObservableCollection<MyType>(TrialRegionListFromDb.Where(x => data.Contains(x.TrialRegionID.ToText())).Select(x => new MyType
                {
                    Id = x.TrialRegionID.ToText(),
                    Name = x.TrialRegionName
                }).ToList());
                //TrialRegionSelected =  null;
                if (!string.IsNullOrWhiteSpace(selectedValueTrialRegion))
                {
                    filteredData = selectedValueTrialRegion.Split(',').Select(x => x.Trim());
                    //TrialRegionSelected = null;
                    TrialRegionSelected = new ObservableCollection<MyType>(TrialRegionList.Where(x => filteredData.Contains(x.Id)));
                }
            }
            if (filterFor == "Country")
            {
                if (CountryListFromDb == null)
                    return;
                var data = filteredList.Select(x => x.CountryCode);
                CountryList = new ObservableCollection<MyType>(CountryListFromDb.Where(x => data.Contains(x.CountryCode)).Select(x => new MyType
                {
                    Id = x.CountryCode,
                    Name = x.CountryName
                }).ToList());
                //CountrySelected = null;
                if (!string.IsNullOrWhiteSpace(SelectedValueCountry))
                {
                    filteredData = SelectedValueCountry.Split(',').Select(x => x.Trim());
                    //CountrySelected = null;
                    CountrySelected = new ObservableCollection<MyType>(CountryList.Where(x => filteredData.Contains(x.Id)));
                }
            }
            //throw new NotImplementedException();
            //return filteredList;
        }
        
        public async Task LoadAllFilterData()
        {
            var downloadedList = string.Join(",", TrialList.Select(x => x.TrialTypeID).Distinct());
            TrialTypeListFromDb = new ObservableCollection<TrialType>(await _trialTypeService.GetTrialTypeListAsync(downloadedList));

            downloadedList = string.Join(",", TrialList.Select(x => "'" + x.CropCode + "'").Distinct());
            CropListFromDb = new ObservableCollection<CropRD>(await _cropRdService.GetCropListAsync(downloadedList));

            downloadedList = string.Join(",", TrialList.Select(x => "'" + x.CropSegmentCode + "'").Distinct());
            CropSegmentListFromDb = new ObservableCollection<CropSegment>(await _cropSegmentService.GetCropSegmentListAsync(downloadedList));

            downloadedList = string.Join(",", TrialList.Select(x => "'" + x.TrialRegionID + "'").Distinct()); ;
            TrialRegionListFromDb = new ObservableCollection<TrialRegion>( await _trialRegionService.GetTrialRegionListAsync(downloadedList));

            downloadedList = string.Join(",", TrialList.Select(x => "'" + x.CountryCode + "'").Distinct());
            CountryListFromDb = new ObservableCollection<Country>( await _countryService.GetCountryListAsync(downloadedList));

            CropList1 = new ObservableCollection<MyType>(CropListFromDb.Select(x => new MyType
            {
                Id = x.CropCode,
                Name = x.CropName
            }).ToList());
            CropList = CropList1;
            CountryList1 = new ObservableCollection<MyType>(CountryListFromDb.Select(x => new MyType
            {
                Id = x.CountryCode,
                Name = x.CountryName
            }).ToList());
            CountryList = CountryList1;
            TrialTypeList1 = new ObservableCollection<MyType>(TrialTypeListFromDb.Select(x => new MyType
            {
                Id = x.TrialTypeID.ToString(),
                Name = x.TrialTypeName
            }).ToList());
            TrialTypeList = TrialTypeList1;
            CropSegmentList1 = new ObservableCollection<MyType>(CropSegmentListFromDb.Select(x => new MyType
            {
                Id = x.CropSegmentCode,
                Name = x.CropSegmentName
            }).ToList());
            CropSegmentList = CropSegmentList1;
            TrialRegionList1 = new ObservableCollection<MyType>(TrialRegionListFromDb.Select(x => new MyType
            {
                Id = x.TrialRegionID.ToText(),
                Name = x.TrialRegionName
            }).ToList());
            TrialRegionList = TrialRegionList1;

            
            var savedFilter = await SaveFilterService.GetSaveFilterAsync();
            if (savedFilter == null)
            {
                CountrySelected = new ObservableCollection<MyType>();
                CropSegmentSelected = new ObservableCollection<MyType>();
                TrialRegionSelected = new ObservableCollection<MyType>();
                TrialTypeSelected = new ObservableCollection<MyType>();
                CropSelected = new ObservableCollection<MyType>();
                return;
            } 
            var filteredData = Enumerable.Empty<string>();
            foreach(SaveFilter val in savedFilter)
            {
                filteredData = val.FieldValue.Split(',').Select(x => x.Trim());
                switch (val.Field.ToLower())
                {
                    
                    case "trialtypeid":
                            TrialTypeSelected =new ObservableCollection<MyType>(TrialTypeList.Where(x => filteredData.Contains(x.Id)));
                            break;
                    case "cropcode":
                        CropSelected = new ObservableCollection<MyType>(CropList.Where(x => filteredData.Contains(x.Id)));
                            break;
                    case "cropsegmentcode":
                        CropSegmentSelected = new ObservableCollection<MyType>(CropSegmentList.Where(x => filteredData.Contains(x.Id)));
                            break;
                    case "trialregionid":
                        TrialRegionSelected = new ObservableCollection<MyType>(TrialRegionList.Where(x => filteredData.Contains(x.Id)));
                        break;
                    case "countrycode":
                        CountrySelected = new ObservableCollection<MyType>(CountryList.Where(x => filteredData.Contains(x.Id)));
                        break;
                }
            }
            if(CountrySelected == null)
                CountrySelected = new ObservableCollection<MyType>();
            if (CropSegmentSelected == null)
                CropSegmentSelected = new ObservableCollection<MyType>();
            if (TrialRegionSelected == null)
                TrialRegionSelected = new ObservableCollection<MyType>();
            if (TrialTypeSelected == null)
                TrialTypeSelected = new ObservableCollection<MyType>();
            if (CropSelected == null)
                CropSelected = new ObservableCollection<MyType>();
            IsInitialLoad = false;
        }

        private void CountryItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CountrySelected));
            //OnPropertyChanged();
        }
        private void CropItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CropSelected));
            //OnPropertyChanged();
        }
        private void TrialRegionItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(TrialRegionSelected));
            //OnPropertyChanged();
        }
        private void CropSegmentItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CropSegmentSelected));
            //OnPropertyChanged();
        }
        private void TrialTypeItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(TrialTypeSelected));
        }
        public void ToggleFilterSetting(string isToggled)
        {
            _settingParametersService.UpdateParams("filter", isToggled);
        }
    }
    internal class ApplyfilterOperation : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            var vm = parameter as FilterPageViewModel;
            if (vm == null) return;

            var savefilterList = new List<SaveFilter>
            {
                new SaveFilter() {Field = "TrialTypeId", FieldValue = string.Join(",", vm.TrialTypeSelected.Select(m => m.Id))},
                new SaveFilter() {Field = "CropCode", FieldValue = string.Join(",", vm.CropSelected.Select(m => m.Id))},
                new SaveFilter() {Field = "CropSegmentCode", FieldValue = string.Join(",", vm.CropSegmentSelected.Select(m => m.Id))},
                new SaveFilter() {Field = "TrialRegionId", FieldValue = string.Join(",", vm.TrialRegionSelected.Select(m => m.Id))},
                new SaveFilter() {Field = "CountryCode", FieldValue = string.Join(",", vm.CountrySelected.Select(m => m.Id)) }
            };
            await vm.SaveFilterService.SaveFilterAsync(savefilterList);
            await App.MainNavigation.PopAsync();
        }

        public event EventHandler CanExecuteChanged;
    }
}
