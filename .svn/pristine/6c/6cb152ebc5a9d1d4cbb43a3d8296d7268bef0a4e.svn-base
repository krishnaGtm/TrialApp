using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TrialApp.Common
{
    public class TraitFieldValidation
    {
        public List<TraitValidation> listOfTraitToValidate = new List<TraitValidation>();

        public string validCharacters = @"[^a-zA-Z0-9.!@,#$%^&*() _+?=-]";
        public string validatecol(string colname, string actualtext)
        {
            try
            {
                var value = listOfTraitToValidate.FirstOrDefault(validation => validation.TraitID == colname);
                return validatescreen(value, actualtext);
            }
            catch (Exception e)
            {
                //Logger.Writelog(e);
                return "";
            }
        }

        //method to validate trait field 
        public string validatescreen(TraitValidation value, string actualtext)
        {
            if (value == null) return "";
            if (string.IsNullOrWhiteSpace(actualtext))
                return "";
            if (actualtext.Trim() != "")
            {
                var checkRegix = Regex.Match(actualtext, validCharacters);
                if (checkRegix.Success)
                    return "Invalid special character/s.";
            }
            if (value.maxvalue != null)
            {
                if (value.decimalplaces <= 0)
                {
                    int i;
                    if (!int.TryParse(actualtext, out i))
                    {
                        return "value|integer";
                    }
                    //if (actualtext.Contains(","))
                    //{
                    //    return "value|integer";
                    //}
                }
                else if (value.decimalplaces > 0)
                {
                    decimal d;
                    if (!decimal.TryParse(actualtext, out d))
                    {
                        return "value|decimal";
                    }
                    var decimals = d.ToString().Split('.');
                    if (decimals.Length == 2)
                    {
                        if (decimals[1].Length > value.decimalplaces)
                        {
                            return "Maximum decimal digit|" + value.decimalplaces;
                        }
                    }

                    //if (actualtext.Contains(","))
                    //{
                    //    var val = actualtext.Split(',');
                    //    if (Convert.ToInt32(val[1]) > value.decimalplaces)
                    //    {
                    //        return "Maximum decimal digit|" + value.decimalplaces;
                    //    }
                    //}
                }
                if (value.minvalue == null)
                {
                    if (actualtext.StartsWith("-"))
                    {
                        return "value|positive";
                    }
                }

                if (value.minvalue != null)
                {
                    try
                    {
                        if (actualtext != "")
                        {
                            if (Convert.ToDecimal(value.maxvalue) < Convert.ToDecimal(actualtext) ||
                                Convert.ToDecimal(value.minvalue) > Convert.ToDecimal(actualtext))
                            {
                                return "value range|" + value.minvalue + " to " + value.maxvalue;
                            }
                        }
                    }
                    catch (Exception excep)
                    {
                        return "format|valid";
                    }
                }
                else
                {
                    try
                    {
                        if (actualtext != "")
                        {
                            if (Convert.ToDecimal(value.maxvalue) < Convert.ToDecimal(actualtext))
                            {
                                return "maxvalue|" + value.maxvalue;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return "format|valid";
                    }
                }
            }
            else if (value.maxlength != null)
            {
                if (actualtext.Length > Convert.ToInt32(value.maxlength))
                {
                    return "maxlength|" + value.maxlength;
                }
            }
            return "";
        }
        public void AddValidation(string[] TraitList, string[] Format, int?[] minvalue, int?[] maxvalue)
        {
            try
            {
                listOfTraitToValidate.Clear();
                for (var i = 0; i < TraitList.Count(); i++)
                {
                    var validation = new TraitValidation();
                    validation.TraitID = TraitList[i];
                    if (Format[i] != null)
                    {
                        var format = Format[i];
                        //validation.needtovalidate = true;
                        if (format.ToLower().StartsWith("x("))
                        {
                            var maxlength = Regex.Match(format, "[0-9]+").Value;
                            validation.maxlength = maxlength;
                            listOfTraitToValidate.Add(validation);
                        }
                        else if (format.StartsWith("-"))
                        {
                            format = format.Replace(",", "");
                            format = format.Replace("-", "");
                            if (format.StartsWith("9"))
                            {
                                if (format.Contains("."))
                                {
                                    var values = format.Split('.');
                                    validation.decimalplaces = values.Length == 2 ? values[1].Length :0;//values[1].Length;
                                    //format = format.Replace(".", ",");
                                    var maxval = Convert.ToInt32(values[0].Replace(">", "9")) + 1;
                                    validation.maxvalue = maxval.ToString();//format;
                                    validation.minvalue = "-" + maxval;//"-" + format;
                                    listOfTraitToValidate.Add(validation);
                                }
                                else
                                {
                                    validation.maxvalue = format;
                                    validation.minvalue = "-" + format;
                                    listOfTraitToValidate.Add(validation);
                                }
                            }
                            else if (format.StartsWith(">"))
                            {
                                format = format.Replace(",", "");
                                if (format.Contains("."))
                                {
                                    var values = format.Split('.');
                                    var rounddecimalval = values.Length == 2 ? values[1].Length : 0;//values[1].Length;
                                    validation.decimalplaces = rounddecimalval;
                                    //format = format.Replace(".", ",");
                                    var maxval = Convert.ToInt32(values[0].Replace(">", "9")) + 1;
                                    validation.maxvalue = maxval.ToString();//format.Replace(">", "9");
                                    validation.minvalue = "-" + maxval;//"-" + format.Replace(">", "9");
                                    listOfTraitToValidate.Add((validation));
                                }
                                else
                                {
                                    validation.maxvalue = format.Replace(">", "9");
                                    listOfTraitToValidate.Add(validation);
                                }
                            }
                        }
                        else if (format.ToLower().StartsWith("9") && format.Contains("/"))
                        {
                            //this is for date field.
                        }
                        else if (format.StartsWith("9"))
                        {
                            format = format.Replace(",", "");
                            if (format.Contains("."))
                            {
                                var values = format.Split('.');
                                validation.decimalplaces = values.Length == 2 ? values[1].Length : 0;//values[1].Length;
                                //format = format.Replace(".", ",");
                                var maxval = Convert.ToInt32(values[0].Replace(">", "9")) + 1;
                                validation.maxvalue = maxval.ToString();//format;
                                listOfTraitToValidate.Add((validation));
                            }
                            else
                            {
                                validation.maxvalue = format;
                                listOfTraitToValidate.Add((validation));
                            }
                        }
                        else if (format.StartsWith(">"))
                        {
                            format = format.Replace(",", "");
                            if (format.Contains("."))
                            {
                                var values = format.Split('.');
                                validation.decimalplaces = values.Length == 2 ? values[1].Length : 0;//values[1].Length;
                                //format = format.Replace(".", ",");
                                var maxval = Convert.ToInt32(values[0].Replace(">", "9")) + 1;
                                validation.maxvalue = maxval.ToString();//format;
                                listOfTraitToValidate.Add((validation));
                            }
                            else
                            {
                                validation.maxvalue = format.Replace(">", "9");
                                listOfTraitToValidate.Add(validation);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // await Logger.Writelog(e);
            }
        }
    }
    public class TraitValidation
    {
        public string TraitID { get; set; }
        public string maxvalue { get; set; }
        public string minvalue { get; set; }
        public string maxlength { get; set; }
        public int decimalplaces { get; set; }
        public bool negativeval { get; set; }
    }
}
