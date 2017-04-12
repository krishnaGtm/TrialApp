using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrialApp.Common;
using TrialApp.Entities.Master;
using TrialApp.Entities.Transaction;
using TrialApp.Services;
using TrialApp.ViewModels.Abstract;
using Xamarin.Forms;

namespace TrialApp.ViewModels
{
    public class EditTrialPropertiesPageViewModel : ObservationBaseViewModel
    {
        #region private variables

        private readonly FieldSetService _fieldSetService;
        private readonly TraitService _traitService;
        private readonly TraitValueService _traitValueService;
        private ObservableCollection<FieldSetPair> _propertySetList;
        private INavigation _navigation;
        private string _currentTrial;
        
        #endregion

        #region public properties

        public ObservableCollection<FieldSetPair> PropertySetList
        {
            get { return _propertySetList; }
            set { _propertySetList = value; OnPropertyChanged(); }
        }

        public string CurrentTrial
        {
            get { return _currentTrial; }
            set { _currentTrial = value; OnPropertyChanged(); }
        }

        #endregion

        public EditTrialPropertiesPageViewModel()
        {
            PropertySetList = new ObservableCollection<FieldSetPair>();
            _fieldSetService = new FieldSetService();
            _traitService = new TraitService();
            _traitValueService = new TraitValueService();
            ObservationService = new ObservationAppService();
            Validation = new TraitFieldValidation();
            TraitList = new List<Trait>();
            ObsValueList = new List<ObservationApp>();
            TrialService = new TrialService();
        }

        public void LoadTrialName()
        {
            var trial = TrialService.GetTrialInfo(TrialEzId);

            if (trial == null) return;

            CurrentTrial = trial.TrialName;
            if (trial.StatusCode == 30)
                UpdatedUi();
            else
                NormalUi();
        }

        /// <summary>
        /// Load list of Property set in Propertyset picker
        /// </summary>
        /// <param name="crop"></param>
        public void LoadFieldsset(string crop)
        {
            var firstFieldset = new FieldSetPair() { Id = 0, Name = "<choose propertyset>" };
            //var list = new ObservableCollection<FieldSetPair>();
            PropertySetList.Add(firstFieldset);

            var fieldSets = _fieldSetService.GetPropertySetList(crop);

            foreach (var val in fieldSets)
            {
                var fieldset = new FieldSetPair()
                {
                    Id = Convert.ToInt32(val.FieldSetID),
                    Name = val.FieldSetCode + " - " + val.FieldSetName
                };
                PropertySetList.Add(fieldset);
            }
            //PropertySetList = list;
            PickerSelectedIndex = 0;
        }

        public async Task LoadProperties(int fieldsetId)
        {
            var traits = _traitService.GetTraitList(fieldsetId);
            var traitIdList = string.Join(",", traits.Select(x => x.TraitID.ToString()));
            await GetObsValueList(TrialEzId.ToString(), traitIdList);

            TraitList = new List<Trait>(traits.Select(x =>
            {
                var trait = new Trait
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
                    CharVisible = (!x.ListOfValues && string.IsNullOrEmpty(x.DataType)) || ((!x.ListOfValues && !string.IsNullOrEmpty(x.DataType) && x.DataType.ToLower() == "c") ? true : false),//datatype c=char, i=int,d=date,a=dec
                    DateVisible = (x.ListOfValues || !string.IsNullOrEmpty(x.DataType)) && ((!x.ListOfValues && !string.IsNullOrEmpty(x.DataType) && x.DataType.ToLower() == "d") ? true : false),
                    IntOrDecVisible = (x.ListOfValues || !string.IsNullOrEmpty(x.DataType)) && ((!x.ListOfValues && !string.IsNullOrEmpty(x.DataType) && (x.DataType.ToLower() == "i" || x.DataType.ToLower() == "a")) ? true : false),
                    ListSource = x.ListOfValues ? _traitValueService.GetTraitValueWithID(x.TraitID) : null,
                    ObsValue = GetObservationValue(GetObsValue(x.TraitID), x.DataType.ToLower()),
                    ValidationErrorVisible = false,
                    ChangedValueVisible = false,
                    DatePickerVisible = false
                };
                //trait.ObsValue = (trait.ObsValue == "" && trait.ListVisible) ? null : trait.ObsValue;
                //trait.DateValue = null;
                trait.ObsvalueInitial = trait.ObsValue;
                trait.DateValue = (trait.DateVisible && trait.ObsValue != "") ? Convert.ToDateTime((trait.ObsValue.Split('T')[0])) : (DateTime?)null;


                return trait;
            }).ToList());

            Validation.AddValidation(TraitList.OrderBy(x => x.TraitID).Select(x => x.TraitID.ToString()).ToArray(),
                TraitList.OrderBy(x => x.TraitID).Select(x => x.DisplayFormat).ToArray(),
                TraitList.OrderBy(x => x.TraitID).Select(x => x.MinValue).ToArray(),
                TraitList.OrderBy(x => x.TraitID).Select(x => x.MaxValue).ToArray());
        }

    }

}
