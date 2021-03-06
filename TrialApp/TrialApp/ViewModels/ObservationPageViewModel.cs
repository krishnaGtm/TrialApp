﻿using System;
using System.Collections.Generic;
using System.Windows.Input;
using TrialApp.Common;
using TrialApp.Controls;
using TrialApp.Entities.Transaction;
using TrialApp.Services;
using TrialApp.Views;
using Xamarin.Forms;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using TrialApp.Entities.Master;
using TrialApp.ViewModels.Abstract;
using System.Collections.ObjectModel;
//using Windows.UI.Xaml;
//using Windows.UI.Xaml.Controls;
using TrialApp.ViewModels.Inetrfaces;

namespace TrialApp.ViewModels
{
    public class ObservationPageViewModel : ObservationBaseViewModel
    {
        #region private variables

        private IDependencyService _dependency;
        private string _nextVarietyName;
        private string _prevVarietyName;
        private Color _nextbuttonColor;
        private Color _prevbuttonColor;
        private List<FieldSetPair> _traitSetList;
        private FieldSetService _FieldSetService;
        //private List<VarietyData> _varList;
        private bool _changedVisible;
        private bool _nextButtonEnable;
        private bool _prevButtonEnable;
        //private List<object> _nextVariety;
        private string _varietyName;
        private string _fieldNumber;
        private string _variety;
        private TraitService traitSrv;
        private TraitValueService traitvalSrv;
        private readonly DefaultFieldSetService _defaultFsService;

        #endregion

        #region public properties
        public string _crop { get; set; }
        public int currentVarietyIndex { get; set; }
        public List<VarietyData> VarList { get; set; }

        public bool NextButtonEnable
        {
            get { return _nextButtonEnable; }
            set
            {
                _nextButtonEnable = value;
                OnPropertyChanged();
            }
        }
        public bool PrevButtonEnable
        {
            get { return _prevButtonEnable; }
            set
            {
                _prevButtonEnable = value;
                OnPropertyChanged();
            }
        }

        public bool ChangedVisible
        {
            get { return _changedVisible; }
            set
            {
                _changedVisible = value;
                OnPropertyChanged();
            }
        }

        public ICommand FieldsetChangeCommand { get; set; }

        public ICommand NextCommand { get; set; }
        public ICommand PrevCommand { get; set; }

        public string VarietyName
        {
            get { return _varietyName; }
            set
            {
                _varietyName = value;
                OnPropertyChanged();
            }
        }

        public string FieldNumber
        {
            get { return _fieldNumber; }
            set
            {
                _fieldNumber = value;
                OnPropertyChanged();
            }
        }

        public string Variety
        {
            get { return _variety; }
            set
            {
                _variety = value;
                OnPropertyChanged();
            }
        }

        public Color NextButtonColor
        {
            get { return _nextbuttonColor; }
            set
            {
                _nextbuttonColor = value;
                OnPropertyChanged();
            }
        }
        public Color PrevButtonColor
        {
            get { return _prevbuttonColor; }
            set
            {
                _prevbuttonColor = value;
                OnPropertyChanged();
            }
        }

        public string NextVarietyName
        {
            get { return _nextVarietyName; }
            set
            {
                _nextVarietyName = value;
                OnPropertyChanged();
            }
        }
        public string PrevVarietyName
        {
            get { return _prevVarietyName; }
            set
            {
                _prevVarietyName = value;
                OnPropertyChanged();
            }
        }

        public List<FieldSetPair> TraitSetList
        {
            get { return _traitSetList; }
            set
            {
                _traitSetList = value;
                OnPropertyChanged();
            }
        }


        public TraitService TraitSrv
        {
            get { return traitSrv; }
            set
            {
                traitSrv = value;
                OnPropertyChanged();
            }
        }
        public TraitValueService TraitvalSrv
        {
            get { return traitvalSrv; }
            set
            {
                traitvalSrv = value;
                OnPropertyChanged();
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="crop"></param>
        /// <param name="varList"></param>
        public ObservationPageViewModel()
        {
            TraitSetList = new List<FieldSetPair>();
            TraitSrv = new TraitService();
            TraitvalSrv = new TraitValueService();

            _FieldSetService = new FieldSetService();
            TrialService = new TrialService();
            ObservationService = new ObservationAppService();
            _defaultFsService = new DefaultFieldSetService();

            FieldsetChangeCommand = new FieldsetChangeOperation();
            Validation = new TraitFieldValidation();
            NextCommand = new NextOperation();
            PrevCommand = new PreviousOperation();
            FieldsetPickerEnabled = true;
        }

        public ObservationPageViewModel(IDependencyService dependencyService)
        {
            _dependency = dependencyService;
        }

        public void LoadFieldsset(string crop)
        {
            var firstFieldset = new FieldSetPair() { Id = 0, Name = "<choose traitset>" };
            TraitSetList.Add(firstFieldset);

            var fieldSets = _FieldSetService.GetFieldSetList(crop);

            foreach (var val in fieldSets)
            {
                var fieldset = new FieldSetPair()
                {
                    Id = Convert.ToInt32(val.FieldSetID),
                    Name = val.FieldSetCode + " - " + val.FieldSetName
                };
                TraitSetList.Add(fieldset);
            }

            // Load selected Traitset from DefaultFieldset table
            var selectedIndex = 0;
            var defaultfs = _defaultFsService.GetDefaultFs(_crop);
            if (defaultfs != null)
            {
                var selectedFs = 0;
                int.TryParse(defaultfs.Fieldset, out selectedFs);
                selectedIndex = TraitSetList.IndexOf(TraitSetList.Find(x => x.Id == selectedFs));
            }
            PickerSelectedIndex = selectedIndex == -1 ? 0 : selectedIndex;

        }

        public void LoadObservationViewModel(string id, string crop, List<VarietyData> varList, int trialEzid)
        {
            _crop = crop;
            EzId = id;
            VarList = varList;
            TrialEzId = trialEzid;
            LoadVarietyInfo(EzId);
            UpdateDisplayUi();
        }

        public void LoadVarietyInfo(string VarietyId)
        {
            var currentItem = VarList.Find(x => x.VarietyId == VarietyId);
            currentVarietyIndex = VarList.IndexOf(currentItem);
            VarietyName = currentItem.FieldNumber + " " + currentItem.VarietyName;
            FieldNumber = currentItem.FieldNumber;
            Variety = currentItem.VarietyName;

            // Disable NEXT button if last item in the list is selected
            if (VarList.ElementAtOrDefault(currentVarietyIndex + 1) != null)
            {
                var nextVarName = VarList[currentVarietyIndex + 1].FieldNumber + " " + VarList[currentVarietyIndex + 1].VarietyName;
                NextVarietyName = "NEXT: " + nextVarName;
                NextButtonEnable = true;
            }
            else
            {
                NextVarietyName = "NEXT: ";
                NextButtonEnable = false;
            }
            // Disable PREV button if First item in the list is selected
            if (VarList.ElementAtOrDefault(currentVarietyIndex - 1) != null)
            {
                var PrevVarName = VarList[currentVarietyIndex - 1].FieldNumber + " " + VarList[currentVarietyIndex - 1].VarietyName;
                PrevVarietyName = "PREV: " + PrevVarName;
                PrevButtonEnable = true;
            }
            else
            {
                PrevVarietyName = "PREV: ";
                PrevButtonEnable = false;
            }

            
        }
        /// <summary>
        /// Change the header and button color according to the status
        /// </summary>
        public void UpdateDisplayUi()
        {
            var status = TrialService.GetTrialInfo(TrialEzId);
            if (status.StatusCode == 30)
                UpdatedUi();
            else
                NormalUi();
        }

        public async Task LoadTraits(int fieldsetId)
        {
            var traits = TraitSrv.GetTraitList(fieldsetId);
            var traitIdList = string.Join(",", traits.Select(x => x.TraitID.ToString()));
            await GetObsValueList(EzId, traitIdList);
            TraitList = new List<Trait>(traits.Select(x =>
            {
                var trait = new Trait()
                {
                    ColumnLabel = x.ColumnLabel,
                    CropGroupID = x.CropGroupID,
                    CropCode = x.CropCode,
                    DataType = x.DataType,
                    DisplayFormat = x.DisplayFormat,
                    Editor = x.Editor,
                    ListOfValues = x.ListOfValues,
                    MaxValue = x.MaxValue,
                    MinValue = x.MinValue,
                    Property = x.Property,
                    TraitID = x.TraitID,
                    TraitName = x.TraitName,
                    Updatable = x.Updatable,
                    Updatable1 = x.Updatable,
                    TraitTypeID = x.TraitTypeID,
                    ListVisible = x.ListOfValues,
                    Tag = x.TraitID.ToString() + "|" + x.DataType,
                    CharVisible = (!x.ListOfValues && string.IsNullOrEmpty(x.DataType)) || ((!x.ListOfValues && !string.IsNullOrEmpty(x.DataType) && x.DataType.ToLower() == "c") ? true : false),
                    DateVisible = (x.ListOfValues || !string.IsNullOrEmpty(x.DataType)) && ((!x.ListOfValues && !string.IsNullOrEmpty(x.DataType) && x.DataType.ToLower() == "d") ? true : false),
                    IntOrDecVisible = (x.ListOfValues || !string.IsNullOrEmpty(x.DataType)) && ((!x.ListOfValues && !string.IsNullOrEmpty(x.DataType) && (x.DataType.ToLower() == "i" || x.DataType.ToLower() == "a")) ? true : false),
                    ListSource = x.ListOfValues ? TraitvalSrv.GetTraitValueWithID(x.TraitID) : null,
                    ObsValue = GetObservationValue(GetObsValue(x.TraitID), x.DataType.ToLower()),
                    ValidationErrorVisible = false,
                    ChangedValueVisible = false,
                    DatePickerVisible = false,
                    RevertVisible = false
                };

                trait.DateValue = (trait.DateVisible && trait.ObsValue != "") ? Convert.ToDateTime((trait.ObsValue.Split('T')[0])) : (DateTime?)null;
                trait.DateValueString = (trait.DateVisible && trait.ObsValue != "") ? (trait.ObsValue.Split('T')[0]) : "";
                trait.ObsvalueInitial = trait.DateVisible ? trait.DateValueString : trait.ObsValue;
                trait.ValueBeforeChanged = trait.ObsvalueInitial;


                return trait;
            }).ToList());

            Validation.AddValidation(TraitList.OrderBy(x => x.TraitID).Select(x => x.TraitID.ToString()).ToArray(),
                TraitList.OrderBy(x => x.TraitID).Select(x => x.DisplayFormat).ToArray(),
                TraitList.OrderBy(x => x.TraitID).Select(x => x.MinValue).ToArray(),
                TraitList.OrderBy(x => x.TraitID).Select(x => x.MaxValue).ToArray());

            // save selected Traitset as default traitset
            _defaultFsService.SaveDefaultFs(_crop, fieldsetId);
        }

        public override void NormalUi()
        {
            base.NormalUi();
            NextButtonColor = Color.FromHex("#4a90e2");
            PrevButtonColor = Color.FromHex("#4a90e2");
            ChangedVisible = false;
        }

        public override void UpdatedUi()
        {
            base.UpdatedUi();
            ChangedVisible = true;
            NextButtonColor = Color.Green;
            PrevButtonColor = Color.Green;
        }

    }

    public class NextOperation : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            var observationPageViewModel = parameter as ObservationPageViewModel;
            var nextVar = observationPageViewModel?.VarList[observationPageViewModel.currentVarietyIndex + 1];
            if (nextVar != null)
            {
                observationPageViewModel.EzId = nextVar.VarietyId;
                observationPageViewModel.LoadVarietyInfo(observationPageViewModel.EzId);
                if (observationPageViewModel.SelectedFieldset.HasValue)
                    await observationPageViewModel.LoadTraits(observationPageViewModel.SelectedFieldset.Value);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
    public class PreviousOperation : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            var observationPageViewModel = parameter as ObservationPageViewModel;
            var prevVar = observationPageViewModel?.VarList[observationPageViewModel.currentVarietyIndex - 1];
            if (prevVar != null)
            {
                observationPageViewModel.EzId = prevVar.VarietyId;
                observationPageViewModel.LoadVarietyInfo(observationPageViewModel.EzId);
                if (observationPageViewModel.SelectedFieldset.HasValue)
                    await observationPageViewModel.LoadTraits(observationPageViewModel.SelectedFieldset.Value);
            }
        }

        public event EventHandler CanExecuteChanged;
    }

    public class FieldsetChangeOperation : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

        }

        public event EventHandler CanExecuteChanged;
    }

    public class FieldSetPair
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Trait : ObservableViewModel//INotifyPropertyChanged
    {
        private bool _updatable;
        public int TraitID { get; set; }
        public int? CropGroupID { get; set; }
        public string CropCode { get; set; }
        public int? TraitTypeID { get; set; }
        public string TraitName { get; set; }
        public string ColumnLabel { get; set; }
        private bool validationErrorVisible;
        public string ObsvalueInitial { get; set; }
        private bool revertVisible;

        public bool RevertVisible
        {
            get { return revertVisible; }
            set
            {
                revertVisible = value;
                OnPropertyChanged();//PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RevertVisible"));
            }
        }

        public bool ValidationErrorVisible
        {
            get { return validationErrorVisible; }
            set
            {
                validationErrorVisible = value;
                OnPropertyChanged();//PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ValidationErrorVisible"));
            }
        }
        private bool changedValueVisible;
        public bool ChangedValueVisible
        {
            get { return changedValueVisible; }
            set
            {
                changedValueVisible = value;
                OnPropertyChanged();//PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ChangedValueVisible"));
            }
        }
        private string dataType;
        private bool listVisible;
        public bool ListVisible
        {
            get { return listVisible; }
            set { listVisible = value; }
        }
        private bool charVisible;
        public bool CharVisible
        {
            get { return charVisible; }
            set { charVisible = value; }
        }
        private bool dateVisible;
        private bool datePickerVisible;
        private string dateValueString;
        public string DateValueString
        {
            get { return dateValueString; }
            set
            {
                dateValueString = value;
                OnPropertyChanged();//PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DateValueString"));
            }
        }
        public bool DatePickerVisible
        {
            get { return datePickerVisible; }
            set
            {
                datePickerVisible = value;
                OnPropertyChanged();//PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DatePickerVisible"));
            }
        }
        public bool DateVisible
        {
            get { return dateVisible; }
            set
            {
                dateVisible = value;
                OnPropertyChanged(); //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DateVisible"));
            }
        }
        private DateTime? dateValue;
        public DateTime? DateValue
        {
            get { return dateValue; }
            set
            {
                dateValue = value;
                DateValueString = dateValue?.ToString("yyyy-MM-dd");
                //NotifyPropertyChanged();
                OnPropertyChanged(); //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DateValue"));
            }
        }

        private bool intOrDecVisible;
        public bool IntOrDecVisible
        {
            get { return intOrDecVisible; }
            set { intOrDecVisible = value; }
        }
        public string DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }

        public bool Updatable
        {
            get { return _updatable; }
            set
            {
                _updatable = value;
                OnPropertyChanged();
            }
        }

        public bool Updatable1 { get; set; }
        public string DisplayFormat { get; set; }
        public bool Editor { get; set; }
        private bool listOfValues;

        public bool ListOfValues
        {
            get { return listOfValues; }
            set { listOfValues = value; }
        }
        private string tag;
        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        private string obsValue;

        public string ObsValue
        {
            get { return obsValue; }
            set
            {
                obsValue = value;
                OnPropertyChanged(); //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ObsValue"));
            }
        }

        public string ValueBeforeChanged { get; set; }

        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
        public bool Property { get; set; }

        private ObservableCollection<TraitValue> listSource;

        //public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<TraitValue> ListSource
        {
            get { return listSource; }
            set
            {
                listSource = value;
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("listSource"));
                OnPropertyChanged();
            }
        }
    }
}
