﻿
using System;
using System.Collections.Generic;
using System.Windows.Input;
using TrialApp.Services;
using System.Linq;
using System.Threading.Tasks;
using TrialApp.ViewModels.Inetrfaces;
using TrialApp.Views;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using TrialApp.Common.Extensions;
using TrialApp.Entities.Transaction;

namespace TrialApp.ViewModels
{
    public class Trial : ObservableViewModel
    {

        private int _ezid;

        public int EZID
        {
            get { return _ezid; }
            set { _ezid = value; OnPropertyChanged(); }
        }
        private string _cropCode;

        public string CropCode
        {
            get { return _cropCode; }
            set { _cropCode = value; OnPropertyChanged(); }
        }
        private string _trialName;

        public string TrialName
        {
            get { return _trialName; }
            set { _trialName = value; OnPropertyChanged(); }
        }
        private int _trialTypeID;

        public int TrialTypeID
        {
            get { return _trialTypeID; }
            set { _trialTypeID = value; OnPropertyChanged(); }
        }
        private string _countryCode;

        public string CountryCode
        {
            get { return _countryCode; }
            set { _countryCode = value; OnPropertyChanged(); }
        }
        private string _statusName;

        public string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; OnPropertyChanged(); }
        }
        private bool _selected;

        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }
        private string _styleID;

        public string StyleID
        {
            get { return _styleID; }
            set { _styleID = value; }
        }

        private string _onlineStatus;

        public string OnlineStatus
        {
            get { return _onlineStatus; }
            set
            {
                _onlineStatus = value;
                OnPropertyChanged();
            }
        }
        private bool _isloginButton;

        public bool IsloginButton
        {
            get { return _isloginButton; }
            set
            {
                _isloginButton = value;
                OnPropertyChanged();
            }
        }
        private bool _isTrial;

        public bool IsTrial
        {
            get { return _isTrial; }
            set
            {
                _isTrial = value;
                OnPropertyChanged();
            }
        }
        private double _fontSizeStatus;

        public double FontSizeStatus
        {
            get { return _fontSizeStatus; }
            set
            {
                _fontSizeStatus = value;
                OnPropertyChanged();
            }
        }
        private double _fontsizeTrialName;

        public double FontsizeTrialName
        {
            get { return _fontsizeTrialName; }
            set
            {
                _fontsizeTrialName = value;
                OnPropertyChanged();
            }
        }
        private Color _trialColor;

        public Color TrialColor
        {
            get { return _trialColor; }
            set
            {
                _trialColor = value;
                OnPropertyChanged();
            }
        }
        public string CropSegmentCode { get; set; }
        public int TrialRegionID { get; set; }

    }
    public class MainPageViewModel : BaseViewModel
    {
        #region Private variables
        private bool _enableCotrols;
        private bool _selected = false;
        private bool _submitVisible = false;
        private string _submitText;
        private List<Entities.Transaction.Trial> _selectedTileList = new List<Entities.Transaction.Trial>();
        private bool _displayConfirmation;
        private readonly IDependencyService _dependencyService;
        private string _searchText;
        private bool _searchVisible;
        private readonly string title = "Remove submitted trials?";
        private readonly string _message = "Once removed, submitted trials will no longer be available on this device." +
                                  System.Environment.NewLine + System.Environment.NewLine + "You can always download trials again.";
        
        
        private ObservableCollection<Trial> _listSource;

        #endregion

        #region public variables
        public readonly SettingParametersService _settingParametersService;
        public readonly SaveFilterService SaveFilterService;
        public TrialService trialService;
        public ObservableCollection<Trial> listSource
        {
            get { return _listSource; }
            set
            {
                _listSource = value;
                OnPropertyChanged();
            }
        }
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }
        public bool SearchVisible
        {
            get { return _searchVisible; }
            set
            {
                _searchVisible = value;
                OnPropertyChanged();
            }
        }
        public bool SubmitVisible
        {
            get { return _submitVisible; }
            set
            {
                _submitVisible = value;
                OnPropertyChanged();
            }
        }
        public string SubmitText
        {
            get { return _submitText; }
            set
            {
                _submitText = value;
                OnPropertyChanged();
            }
        }
        public bool EnableCotrols
        {
            get { return _enableCotrols; }
            set
            {
                _enableCotrols = value;
                OnPropertyChanged();
            }
        }
        public ICommand SubmitCommand { get; set; }

        public ICommand GoToFilterScreen { get; set; }

        public List<Entities.Transaction.Trial> SelectedTileList
        {
            get { return _selectedTileList; }
            set
            {
                _selectedTileList = value;
                OnPropertyChanged();
            }
        }
        public List<Trial> AllTrials { get; set; }
        public List<SaveFilter> SaveFilterList;// = new List<SaveFilter>();
        internal SettingParameters Settings;
        //var result = _settingParametersService.GetParamsList().Single();
        #endregion
        public MainPageViewModel()
        {
            trialService = new TrialService();
            SaveFilterService = new SaveFilterService();
            _settingParametersService = new SettingParametersService();
            SubmitCommand = new SubmitOperation(this);
            GoToFilterScreen = new GoToFilterScreenCommand();
            listSource = new ObservableCollection<Trial>();
            AllTrials = new List<Trial>();
            EnableCotrols = true;
            SaveFilterList = new List<SaveFilter>();
            
        }
        internal async Task RemoveTrials()
        {
            await trialService.RemoveTrialFromDevice(SelectedTileList);

        }
        internal async Task UpdateTrial()
        {
            await trialService.UpdateTrialAndObservationData(SelectedTileList);
        }
        /// <summary>
        /// load trials on screen fetching all trial data from db.
        /// </summary>
        public async Task LoadTrials()
        {
            listSource.Clear();
            AllTrials.Clear();
            SearchText = "";
            var data = trialService.GetAllTrials();
            foreach (var _item in data)
            {
                var trial = new Trial
                {
                    CountryCode = _item.CountryCode,
                    CropCode = _item.CropCode,
                    EZID = _item.EZID,
                    StatusName = MainPage.TrialStatus[_item.StatusCode.ToString()],
                    TrialName = _item.TrialName,
                    TrialTypeID = _item.TrialTypeID,
                    IsTrial = true,
                    IsloginButton = false,
                    FontSizeStatus = Device.GetNamedSize(NamedSize.Micro, typeof(Label)) - 2,
                    FontsizeTrialName = Device.GetNamedSize(NamedSize.Small, typeof(Label)) - 2,
                    TrialColor = _item.StatusCode == 30 ? Color.FromHex("#61ce2b") : Color.FromHex("#4990e2"),
                    StyleID = _item.EZID + "|" + _item.TrialName + "|" + _item.CropCode,
                    Selected = false,
                    OnlineStatus = _item.StatusCode.ToString(),
                    TrialRegionID = _item.TrialRegionID,
                    CropSegmentCode = _item.CropSegmentCode,
                };
                AllTrials.Add(trial);
            }
            var filteredlist = await ApplyFilterOnTiles("");
            await InsertLoginButton(); // insert login button if not exists
            foreach (var _item in filteredlist)
            {
                listSource.Add(_item);
            }
            ReloadTrialProp = true;
        }
        /// <summary>
        /// delete trial data after uploading data to server if any changes made otherwise delete directly.
        /// </summary>
        /// <returns></returns>
        internal async Task DeleteTrials()
        {
            await trialService.RemoveTrialFromDevice(SelectedTileList);
        }
        /// <summary>
        /// Reload Trials displayed on mainpage.
        /// </summary>
        /// <param name="search">load trial with name starting with search parameter</param>
        internal async Task ReloadTrial(string search)
        {
            //AllTrials = trialService.GetAllTrials(); // get all trials.. only needed when user downloaded new trial so can be optimized later.
            listSource.Clear();
            var data = await ApplyFilterOnTiles(search); // apply filter and return filter data that will be added later 
            await InsertLoginButton(); // insert login button always on first
            //add data to list source to display
            if (data != null)
            {
                foreach (var trial in data)
                {
                    listSource.Add(trial);
                }
            }
            await RefreshSubmitTrialList(); // refresh submit trial status if user filters tiles.

            var loginTrialtile = listSource[0];
            if (loginTrialtile != null)
            {
                if (!loginTrialtile.IsTrial)
                {
                    loginTrialtile.OnlineStatus = UserName.ToText() == "" ? "OFFLINE" : "+";
                    loginTrialtile.TrialColor = UserName.ToText() == "" ? Color.Gray : Color.FromHex("#61ce2b");
                    loginTrialtile.FontsizeTrialName = UserName.ToText() == ""
                        ? Device.GetNamedSize(NamedSize.Medium, typeof(Button))
                        : 70;
                }

            }
        }

        /// <summary>
        /// fitler function for search and fitler.
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<ObservableCollection<Trial>> ApplyFilterOnTiles(string search)
        { 
            if (search != "")
            {
                listSource.Clear();
                var data = AllTrials.Where(x => x.TrialName.ToLower().Contains(search.ToLower())).ToList();
                if (data.Count + 1 != listSource.Count)
                {
                    return new ObservableCollection<Trial>(data);
                }
            }
            else
            {
                var filteredData = Enumerable.Empty<string>();
                var filteredTrials = AllTrials;
                bool filterFound = false;
                if (Settings.Filter)
                {
                    foreach (var val in SaveFilterList)
                    {
                        filteredData = val.FieldValue.Split(',').Select(x => x.Trim());
                        switch (val.Field.ToLower())
                        {
                            case "trialtypeid":
                                if (!string.IsNullOrWhiteSpace(val.FieldValue))
                                {
                                    filteredTrials = filteredTrials.Where(x => filteredData.Contains(x.TrialTypeID.ToString())).ToList();
                                    filterFound = true;
                                }  
                                break;
                            case "cropcode":
                                if (!string.IsNullOrWhiteSpace(val.FieldValue))
                                {
                                    filteredTrials = filteredTrials.Where(x => filteredData.Contains(x.CropCode)).ToList();
                                    filterFound = true;
                                }
                                break;
                            case "cropsegmentcode":
                                if (!string.IsNullOrWhiteSpace(val.FieldValue))
                                {
                                    filteredTrials = filteredTrials.Where(x => filteredData.Contains(x.CropSegmentCode)).ToList();
                                    filterFound = true;
                                }
                                break;
                            case "trialregionid":
                                if (!string.IsNullOrWhiteSpace(val.FieldValue))
                                {
                                    filteredTrials = filteredTrials.Where(x => filteredData.Contains(x.TrialRegionID.ToString())).ToList();
                                    filterFound = true;
                                }
                                break;
                            case "countrycode":
                                if (!string.IsNullOrWhiteSpace(val.FieldValue))
                                {
                                    filteredTrials = filteredTrials.Where(x => filteredData.Contains(x.CountryCode)).ToList();
                                    filterFound = true;
                                }
                                break;
                        }

                    }
                }
                FilterIcon = Settings.Filter && filterFound ? ImageSource.FromFile("../Assets/Activefilter.png") : ImageSource.FromFile("../Assets/filter.png");
                if (AllTrials.Count != filteredTrials.Count || AllTrials.Count != listSource.Count - 1)
                {
                    return new ObservableCollection<Trial>(filteredTrials.ToList());
                   
                }

            }
            return new ObservableCollection<Trial>();
        }
        public async Task InsertLoginButton()
        {
            var isLoginButtonExists = listSource.FirstOrDefault(x => x.IsTrial == false);
            if (isLoginButtonExists == null)
            {
                var loginTrial = new Trial
                {
                    IsTrial = false,
                    IsloginButton = true,
                    OnlineStatus = UserName.ToText() == "" ? "OFFLINE" : "+",
                    TrialColor = UserName.ToText() == "" ? Color.Gray : Color.FromHex("#61ce2b"),
                    FontsizeTrialName = UserName.ToText() == "" ? Device.GetNamedSize(NamedSize.Medium, typeof(Button)) : 70
                };
                listSource.Insert(0, loginTrial);

            }
        }
        public MainPageViewModel(IDependencyService dependencyService)
        {
            _dependencyService = dependencyService;
            EnableCotrols = true;
        }

        public bool DisplayConfirmation
        {
            get { return _displayConfirmation; }
            set
            {
                _displayConfirmation = value;
                if (value)
                    DisplayAlert();
                OnPropertyChanged();
            }
        }

        public bool ReloadTrialProp { get; set; }

        public void DisplayAlert()
        {
            string[] values = { title, _message };
            Xamarin.Forms.MessagingCenter.Send(this, "DisplayAlert", values);
        }
        internal void UpdateSubmit(Entities.Transaction.Trial selectedTrial, bool isAdded)
        {
            var alreadyAdded = SelectedTileList.FirstOrDefault(x => x.EZID == selectedTrial.EZID);
            if (alreadyAdded != null)
                SelectedTileList.Remove(alreadyAdded);
            else
                SelectedTileList.Add(selectedTrial);
            if (SelectedTileList.Count > 0)
            {
                SubmitVisible = true;
                SubmitText = "SUBMIT (" + SelectedTileList.Count + ")";
            }
            else
                SubmitVisible = false;

        }

        public void PersistSubmitTrialList()
        {
            foreach (var _trial in SelectedTileList)
            {
                var tile = listSource.FirstOrDefault(x => x.EZID == _trial.EZID);
                tile.Selected = true;
            }
            if (SelectedTileList.Count > 0)
            {
                SubmitVisible = true;
                SubmitText = "SUBMIT (" + SelectedTileList.Count + ")";
            }
            else
                SubmitVisible = false;
        }

        private async Task RefreshSubmitTrialList()
        {
            SelectedTileList.Clear();
            var data = listSource.Where(x => x.Selected).ToList();
            foreach (var _data in data)
            {
                SelectedTileList.Add(new Entities.Transaction.Trial
                {
                    EZID = _data.EZID,
                    CountryCode = _data.CountryCode,
                    CropCode = _data.CropCode,
                    TrialName = _data.TrialName,
                    TrialTypeID = _data.TrialTypeID,
                    StatusCode = _data.StatusName == "New" ? 10 : _data.StatusName == "Synced" ? 20 : 30,// _data.StatusCode
                });
            }
            if (SelectedTileList.Count > 0)
            {
                SubmitVisible = true;
                SubmitText = "SUBMIT (" + SelectedTileList.Count + ")";
            }
            else
                SubmitVisible = false;
        }

        public void ClearUserForSingOut()
        {
            WebserviceTasks.passwordWS = "";
            WebserviceTasks.token = "";
            WebserviceTasks.usernameWS = "";
            UserName = "";
        }

        internal async Task RefershFilter()
        {
            var trials = trialService.GetAllTrials();
            var newSavefilterlist = new List<SaveFilter>();
            string trialtypefilterparam = "";
            string countryfilterparam = "";
            string trialregionfilterparam = "";
            string cropsegmentfilterparam = "";
            string cropfilterparam = "";
            bool needtoupdate = false;
            foreach (var _data in SaveFilterList)
            {
                var filteredData = new List<string>();
                var filterDataToExclude = new List<string>(); ;
                var individualFilterItems = Enumerable.Empty<string>();
                switch (_data.Field)
                {
                    case "TrialTypeId":
                        individualFilterItems = trials.Select(x => x.TrialTypeID.ToText());
                        if (!string.IsNullOrWhiteSpace(_data.FieldValue))
                            filteredData = _data.FieldValue.Split(',').Select(x => x.Trim()).ToList();
                        foreach (var _filterparam in filteredData)
                        {
                            if (!individualFilterItems.Contains(_filterparam))
                            {
                                needtoupdate = true;
                                filterDataToExclude.Add(_filterparam);
                            }

                        }
                        if (needtoupdate)
                        {
                            filteredData = filteredData.Except(filterDataToExclude).ToList();
                            newSavefilterlist.Add(new SaveFilter()
                            {
                                Field = "TrialTypeId",
                                FieldValue = string.Join(",", filteredData.Select(x => x.Trim()))

                            });
                        }
                        break;
                    case "CropCode":
                        individualFilterItems = trials.Select(x => x.CropCode);
                        if (!string.IsNullOrWhiteSpace(_data.FieldValue))
                            filteredData = _data.FieldValue.Split(',').Select(x => x.Trim()).ToList();
                        foreach (var _filterparam in filteredData)
                        {
                            if (!individualFilterItems.Contains(_filterparam))
                            {
                                needtoupdate = true;
                                filterDataToExclude.Add(_filterparam);
                            }

                        }
                        if (needtoupdate)
                        {
                            filteredData = filteredData.Except(filterDataToExclude).ToList();
                            newSavefilterlist.Add(new SaveFilter()
                            {
                                Field = "CropCode",
                                FieldValue = string.Join(",", filteredData.Select(x => x.Trim()))

                            });
                        }
                        break;
                    case "CropSegmentCode":
                        individualFilterItems = trials.Select(x => x.CropSegmentCode);
                        if (!string.IsNullOrWhiteSpace(_data.FieldValue))
                            filteredData = _data.FieldValue.Split(',').Select(x => x.Trim()).ToList();
                        foreach (var _filterparam in filteredData)
                        {
                            if (!individualFilterItems.Contains(_filterparam))
                            {
                                needtoupdate = true;
                                filterDataToExclude.Add(_filterparam);
                            }

                        }
                        if (needtoupdate)
                        {
                            filteredData = filteredData.Except(filterDataToExclude).ToList();
                            newSavefilterlist.Add(new SaveFilter()
                            {
                                Field = "CropSegmentCode",
                                FieldValue = string.Join(",", filteredData.Select(x => x.Trim()))

                            });
                        }
                        break;
                    case "TrialRegionId":
                        individualFilterItems = trials.Select(x => x.TrialRegionID.ToText());
                        if (!string.IsNullOrWhiteSpace(_data.FieldValue))
                            filteredData = _data.FieldValue.Split(',').Select(x => x.Trim()).ToList();
                        foreach (var _filterparam in filteredData)
                        {
                            if (!individualFilterItems.Contains(_filterparam))
                            {
                                needtoupdate = true;
                                filterDataToExclude.Add(_filterparam);
                            }

                        }
                        if (needtoupdate)
                        {
                            filteredData = filteredData.Except(filterDataToExclude).ToList();
                            newSavefilterlist.Add(new SaveFilter()
                            {
                                Field = "TrialRegionId",
                                FieldValue = string.Join(",", filteredData.Select(x => x.Trim()))

                            });
                        }
                        break;
                    case "CountryCode":
                        individualFilterItems = trials.Select(x => x.CountryCode.ToText());
                        if (!string.IsNullOrWhiteSpace(_data.FieldValue))
                            filteredData = _data.FieldValue.Split(',').Select(x => x.Trim()).ToList();
                        foreach (var _filterparam in filteredData)
                        {
                            if (!individualFilterItems.Contains(_filterparam))
                            {
                                needtoupdate = true;
                                filterDataToExclude.Add(_filterparam);
                            }

                        }
                        if (needtoupdate)
                        {
                            filteredData = filteredData.Except(filterDataToExclude).ToList();
                            newSavefilterlist.Add(new SaveFilter()
                            {
                                Field = "CountryCode",
                                FieldValue = string.Join(",", filteredData.Select(x => x.Trim()))

                            });
                        }
                        break;
                }
            }
            await SaveFilterService.SaveFilterAsync(newSavefilterlist);
        }
    }
    internal class SubmitOperation : ICommand
    {
        private readonly MainPageViewModel _mainPageViewModel;
        public SubmitOperation(MainPageViewModel mainPageViewModel)
        {
            this._mainPageViewModel = mainPageViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            _mainPageViewModel.EnableCotrols = false;
            var listOFModified = _mainPageViewModel.SelectedTileList.Where(trial => trial.StatusCode == 30).ToList();
            if (listOFModified.Any())
                await _mainPageViewModel.trialService.Uploaddata(listOFModified);
            _mainPageViewModel.DisplayConfirmation = true;
            _mainPageViewModel.EnableCotrols = true;
        }
    }

    internal class GoToFilterScreenCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (parameter is MainPageViewModel)
            {
                var vm = parameter as MainPageViewModel;
                if (vm == null) return;
                var data = vm.AllTrials.Select(x => new Entities.Transaction.Trial
                {
                    EZID = x.EZID,
                    CountryCode = x.CountryCode,
                    CropCode = x.CropCode,
                    TrialName = x.TrialName,
                    CropSegmentCode = x.CropSegmentCode,
                    TrialRegionID = x.TrialRegionID,
                    TrialTypeID = x.TrialTypeID,
                }).ToList();
                await App.MainNavigation.PushAsync(new FilterPage(data));
            }
            else if (parameter is TransferPageViewModel)
            {
                var vm = parameter as TransferPageViewModel;
                if (vm == null) return;

                var dataList =
                    new List<Entities.Transaction.Trial>(
                        vm.TotalDownloadedTrialList.Select(x =>
                         new Entities.Transaction.Trial()
                         {
                             CropCode = x.CropCode,
                             CountryCode = x.CountryCode,
                             TrialTypeID = x.TrialTypeID,
                             TrialRegionID = x.TrialRegionID,
                             CropSegmentCode = x.CropSegmentCode,
                             EZID = x.EZID
                         })).ToList();

                await App.MainNavigation.PushAsync(new FilterPage(dataList));
            }

        }

        public event EventHandler CanExecuteChanged;
    }
}
