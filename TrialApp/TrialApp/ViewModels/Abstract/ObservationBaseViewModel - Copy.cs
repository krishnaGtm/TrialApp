﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreFoundation;
using Microsoft.Practices.ObjectBuilder2;
using TrialApp.Common;
using TrialApp.Common.Extensions;
using TrialApp.Controls;
using TrialApp.Entities.Transaction;
using TrialApp.Services;
using Xamarin.Forms;

namespace TrialApp.ViewModels.Abstract
{
    public class ObservationBaseViewModel : BaseViewModel
    {
        #region private variables

        private int _pickerSelectedIndex;
        private List<Trait> traitList;
        private Color _headerTextColor;
        private Color _headerColor;
        private Entry _unValidatedEntry;

        #endregion

        #region public properties

        public List<Trait> TraitList
        {
            get { return traitList; }
            set
            {
                traitList = value;
                OnPropertyChanged();
            }
        }

        public ObservationAppService ObservationService;

        public int PickerSelectedIndex
        {
            get { return _pickerSelectedIndex; }
            set
            {
                _pickerSelectedIndex = value;
                OnPropertyChanged();
            }
        }
        public List<ObservationApp> ObsValueList { get; set; }

        public int? SelectedFieldset { get; set; }

        public TraitFieldValidation Validation { get; set; }

        public string EzId { get; set; }
        public int TrialEzId { get; set; }

        public TrialService TrialService { get; set; }

        public Color HeaderTextColor
        {
            get { return _headerTextColor; }
            set
            {
                _headerTextColor = value;
                OnPropertyChanged();
            }
        }

        public Color HeaderColor
        {
            get { return _headerColor; }
            set
            {
                _headerColor = value;
                OnPropertyChanged();
            }
        }

        #endregion

        /// <summary>
        /// returns corresponant value according to data type
        /// </summary>
        /// <param name="observationData"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public string GetObservationValue(ObservationApp observationData, string dataType)
        {
            if (observationData != null)
            {
                switch (dataType)
                {
                    case "c":
                        return observationData.ObsValueChar.ToText();
                    case "i":
                        return observationData.ObsValueInt.ToText();
                    case "d":
                        return observationData.ObsValueDate.ToText();
                    case "a":
                        return observationData.ObsValueDec.ToText();
                    default:
                        return "";

                }
            }
            return "";

        }

        /// <summary>
        /// returns single object from list
        /// </summary>
        /// <param name="traitId"></param>
        /// <returns></returns>
        public ObservationApp GetObsValue(int traitId)
        {
            var obsData = ObsValueList.FirstOrDefault(x => x.TraitID == traitId);
            return obsData;
        }

        /// <summary>
        /// returns correct observation value according to the datatype
        /// </summary>
        /// <param name="observationData"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public ObservationApp ObservationWithCorrVal(ObservationApp observation, string dataType, string value)
        {
            switch (dataType)
            {
                case "c":
                    observation.ObsValueChar = value;
                    return observation;
                case "i":
                    int i;
                    observation.ObsValueInt = int.TryParse(value, out i) ? i : (int?)null;// integerval) //Convert.ToInt32(value);
                    return observation;
                case "d":
                    DateTime dt;
                    observation.ObsValueDate = DateTime.TryParse(value, out dt) ? dt.ToString("yyyy-MM-ddTHH:mm:ss") : "";// Convert.ToDateTime(value);
                    return observation;
                case "a":
                    decimal dec;
                    observation.ObsValueDec = decimal.TryParse(value, out dec) ? dec : (decimal?)null;// Convert.ToDecimal(value);
                    return observation;
                default:
                    return observation;

            }
        }

        public async Task GetObsValueList(string ezid, string traitIdList)
        {
            ObsValueList = await ObservationService.GetObservationDataAll(ezid, traitIdList);
        }

        public async void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            var entry = sender as Entry;
            if (entry == null) return;
            var traitId = Convert.ToInt32(entry.ClassId.Split('|')[0]);
            var trait = TraitList.FirstOrDefault(x => x.TraitID == traitId);
            if (trait == null)
            {
                
                //TraitList.ForEach(trait1 => trait1.Updatable = trait1.Updatable1); // this is else condition which will make other control non updatable and only one control updatable
                return;
            }
            if (trait.ValueBeforeChanged == trait.ObsValue) //this method is called to prevent on data saving because event is fired multiple times by default.
            {
                //#region  make updatable
                ////TraitList.Except(trait).ForEach(trait1 => trait1.Updatable = trait1.Updatable1); // this is else condition which will make other control non updatable and only one control updatable
                //foreach (var  trait1 in TraitList.Where(x => x.TraitID != trait.TraitID).ToList())
                //    trait1.Updatable = trait1.Updatable1;
                ////trait.Updatable = true;
                //#endregion
                return;
            }


            #region make other control non updatable
            //TraitList.ForEach(trait1 => trait1.Updatable = false); // this is else condition which will make other control non updatable and only one control updatable
            //trait.Updatable = true;
            #endregion
            //trait.ValueBeforeChanged = trait.ObsValue;
            var dataType = entry.ClassId.Split('|')[1];
            var validateResult = Validation.validatecol(traitId.ToString(), entry.Text);
            if (string.IsNullOrEmpty(validateResult))
            {
                trait.ValueBeforeChanged = trait.ObsValue;
                #region  make updatable
                TraitList.ForEach(trait1 => trait1.Updatable = trait1.Updatable1); // this is else condition which will make other control non updatable and only one control updatable
                //trait.Updatable = true;
                #endregion
                var observation = new ObservationApp
                {
                    EZID = EzId,
                    TraitID = traitId,
                    DateCreated = DateTime.UtcNow.Date.ToString("yyyy-MM-dd"),
                    DateUpdated = DateTime.UtcNow.Date.ToString("yyyy-MM-dd"),
                    UserIDCreated = "user",
                    UserIDUpdated = "user",
                    Modified = true
                };
                observation = ObservationWithCorrVal(observation, dataType.ToLower(), entry.Text);
                await ObservationService.UpdateOrSaveObservationData(observation);
                TrialService.UpdateTrialStatus(TrialEzId);
                trait.ValidationErrorVisible = false;
                if (trait.ObsvalueInitial != trait.ObsValue)
                {
                    trait.RevertVisible = true;
                    trait.ChangedValueVisible = true;
                }
                else
                {
                    trait.RevertVisible = false;
                    trait.ChangedValueVisible = false;
                }
                UpdatedUi();

            }
            else
            {
                _unValidatedEntry = entry;
                //foreach (var trait1 in TraitList.Where(x => x.TraitID != trait.TraitID).ToList())
                //    trait1.Updatable = false;
                trait.ValidationErrorVisible = true;
                trait.ChangedValueVisible = false;
                entry.Focus();
                
            }
        }
        public void DateEntry_Focused(object sender, FocusEventArgs e)
        {
            if (sender is Entry)
            {

                _unValidatedEntry = null;
                var entry = sender as Entry;
                if (entry == null)
                    return;
                //var parent = entry.Parent as Xamarin.Forms.Grid;
                //if (parent == null)
                //    return;
                TraitList.ForEach(trait1 => trait1.Updatable = trait1.Updatable1);


                var traitId = Convert.ToInt32(entry.ClassId.Split('|')[0]);
                var trait = TraitList.FirstOrDefault(x => x.TraitID == traitId);
                if (trait == null)
                    return;

                #region make other control non updatable

                //TraitList.ForEach(trait1 => trait1.Updatable = false); // this is else condition which will make other control non updatable and only one control updatable
                //trait.Updatable = true;
                //foreach (var trait1 in TraitList.Where(x => x.TraitID != trait.TraitID).ToList())
                //    trait1.Updatable = false;

                #endregion

                if (trait.DateVisible)
                {
                    trait.DateVisible = false;
                    trait.DatePickerVisible = true;
                }
                //foreach (var _children in parent.Children)
                //{
                //    if (_children is NullableDatePicker)
                //    {
                //        Device.BeginInvokeOnMainThread(() =>
                //        {
                //            //_children.Unfocus();
                //            //if(_children.IsFocused)
                //            //    _children.Unfocus();
                //            _children.Focus();
                //            //_children.Focus();
                //        });
                //        break;
                //    }
                //}
                //this f*****g code will work in android and IOS but not on windows :(
            }
            else if (sender is BindablePicker && _unValidatedEntry!= null)
            {
                var picker = sender as BindablePicker;
                //picker.Unfocus();
                picker.IsVisible = false;
                _unValidatedEntry.Focus();
                picker.IsVisible = true;

            }
        }
        public async void Today_Clicked(object sender, EventArgs e)
        {
            var img = sender as Image;
            var traitId = Convert.ToInt32(img.ClassId.Split('|')[0]);
            var trait = TraitList.FirstOrDefault(x => x.TraitID == traitId);
            if (trait == null) return;
            if (trait.DateValue != DateTime.Today.Date)
            {
                var observation = new ObservationApp
                {
                    EZID = EzId,
                    TraitID = traitId,
                    DateCreated = DateTime.UtcNow.Date.ToString("yyyy-MM-dd"),
                    DateUpdated = DateTime.UtcNow.Date.ToString("yyyy-MM-dd"),
                    UserIDCreated = "user",
                    UserIDUpdated = "user",
                    ObsValueDate = DateTime.Today.Date.ToString("yyyy-MM-ddTHH:mm:ss"),// datePicker.Date,
                    Modified = true
                };
                await ObservationService.UpdateOrSaveObservationData(observation);
                TrialService.UpdateTrialStatus(TrialEzId);
                trait.DatePickerVisible = false;
                trait.DateVisible = true;
                trait.DateValue = DateTime.Today.Date;
                trait.ValidationErrorVisible = false;
                trait.ChangedValueVisible = true;
                trait.RevertVisible = true;
                UpdatedUi();
            }
            else
            {
                trait.ValidationErrorVisible = false;
                trait.ChangedValueVisible = false;
                trait.RevertVisible = false;

            }
            
            
            
        }
        public void DatePicker_UnFocusedEX(object sender, FocusEventArgs e)
        {
            var datePicker = sender as DatePicker;

            var traitId = Convert.ToInt32(datePicker.ClassId.Split('|')[0]);
            var trait = TraitList.FirstOrDefault(x => x.TraitID == traitId);
            if (trait == null) return;

            trait.DatePickerVisible = false;
            trait.DateVisible = true;
        }
        public async void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as BindablePicker;
            if (picker?.SelectedValue == null) return;
            if (picker.ClassId != null)
            {
                var traitId = Convert.ToInt32(picker.ClassId.Split('|')[0]);
                var trait = TraitList.FirstOrDefault(x => x.TraitID == traitId);
                if (trait == null) return;
                if (trait.ValueBeforeChanged == trait.ObsValue) //this method is called to prevent on data saving because event is fired multiple times by default.
                    return;
                trait.ValueBeforeChanged = trait.ObsValue;
                var validateResult = Validation.validatecol(traitId.ToString(), picker.SelectedValue.ToString());
                if (string.IsNullOrEmpty(validateResult))
                {
                    var observation = new ObservationApp
                    {
                        EZID = EzId,
                        TraitID = traitId,
                        DateCreated = DateTime.UtcNow.Date.ToString("yyyy-MM-dd"),
                        DateUpdated = DateTime.UtcNow.Date.ToString("yyyy-MM-dd"),
                        UserIDCreated = "user",
                        UserIDUpdated = "user",
                        ObsValueChar = picker.SelectedValue.ToString(),
                        Modified = true
                    };
                    await ObservationService.UpdateOrSaveObservationData(observation);
                    TrialService.UpdateTrialStatus(TrialEzId);
                    trait.ValidationErrorVisible = false;                    
                    if (trait.ObsValue != trait.ObsvalueInitial)
                    {
                        trait.RevertVisible = true;
                        trait.ChangedValueVisible = true;
                    }   
                    else
                    {
                        trait.RevertVisible = false;
                        trait.ChangedValueVisible = false;

                    }   
                    UpdatedUi();
                }
                else
                {
                    trait.ValidationErrorVisible = true;
                    trait.ChangedValueVisible = false;
                }
            }


        }
        public async void DateData_DateSelected(object sender, DateChangedEventArgs e)
        {
            var datePicker = sender as DatePicker;
            var traitId = Convert.ToInt32(datePicker.ClassId.Split('|')[0]);
            var trait = TraitList.FirstOrDefault(x => x.TraitID == traitId);
            if (trait == null) return;
            if (trait.ValueBeforeChanged == datePicker.Date.ToString("yyyy-MM-dd")) //this method is called to prevent on data saving because event is fired multiple times by default.
                return;
            trait.ValueBeforeChanged = datePicker.Date.ToString("yyyy-MM-dd");
            var observation = new ObservationApp
                {
                    EZID = EzId,
                    TraitID = traitId,
                    DateCreated = DateTime.UtcNow.Date.ToString("yyyy-MM-dd"),
                    DateUpdated = DateTime.UtcNow.Date.ToString("yyyy-MM-dd"),
                    UserIDCreated = "user",
                    UserIDUpdated = "user",
                    ObsValueDate = datePicker.Date.ToString("yyyy-MM-ddTHH:mm:ss"),
                    Modified = true
                };
                await ObservationService.UpdateOrSaveObservationData(observation);
                TrialService.UpdateTrialStatus(TrialEzId);
                trait.DatePickerVisible = false;
                trait.DateVisible = true;
                trait.ValidationErrorVisible = false;
                trait.ChangedValueVisible = true;
                trait.DateValueString = datePicker.Date.ToString("yyyy-MM-dd");
                if (trait.ObsvalueInitial != datePicker.Date.ToString("yyyy-MM-dd"))
                {
                    trait.RevertVisible = true;
                    trait.ChangedValueVisible = true;
                }
                else
                {
                    trait.RevertVisible = false;
                    trait.ChangedValueVisible = false;

                }
            UpdatedUi();
        }

        public async void Revert_Clicked(object sender, EventArgs e)
        {
            var img = sender as Image;
            var traitId = Convert.ToInt32(img.ClassId.Split('|')[0]);
            var trait = TraitList.FirstOrDefault(x => x.TraitID == traitId);
            if (trait == null)
                return;
            if (trait.DataType.ToLower() == "d")
            {
                trait.DateValueString = trait.ObsvalueInitial;
                trait.DateValue = (trait.DateVisible && trait.ObsvalueInitial != "") ? Convert.ToDateTime((trait.ObsValue.Split('T')[0])) : (DateTime?)null;
                var observation = new ObservationApp
                {
                    EZID = EzId,
                    TraitID = traitId,
                    DateCreated = DateTime.UtcNow.Date.ToString("yyyy-MM-dd"),
                    DateUpdated = DateTime.UtcNow.Date.ToString("yyyy-MM-dd"),
                    UserIDCreated = "user",
                    UserIDUpdated = "user",
                    ObsValueDate = trait.DateValue?.ToString("yyyy-MM-ddTHH:mm:ss"),
                    Modified = true
                };
                await ObservationService.UpdateOrSaveObservationData(observation);
            }
            else
            {

                var observation = new ObservationApp
                {
                    EZID = EzId,
                    TraitID = traitId,
                    DateCreated = DateTime.UtcNow.Date.ToString("yyyy-MM-dd"),
                    DateUpdated = DateTime.UtcNow.Date.ToString("yyyy-MM-dd"),
                    UserIDCreated = "user",
                    UserIDUpdated = "user",
                    Modified = true
                };
                observation = ObservationWithCorrVal(observation, trait.DataType.ToLower(), trait.ObsvalueInitial);
                await ObservationService.UpdateOrSaveObservationData(observation);
            }
            trait.ObsValue = trait.ObsvalueInitial;
            trait.ValidationErrorVisible = false;
            trait.ChangedValueVisible = false;
            trait.RevertVisible = false;
        }

        /// <summary>
        /// Change the UI after value is updated in db
        /// </summary>
        public virtual void UpdatedUi()
        {
            HeaderColor = Color.Green;
            HeaderTextColor = Color.White;
        }

        /// <summary>
        /// Normal non modified UI page
        /// </summary>
        public virtual void NormalUi()
        {
            HeaderColor = Color.FromHex("#ebebeb");
            HeaderTextColor = Color.Black;
        }
    }
}