﻿
using System.Linq;
using System.Xml.Linq;
//using Foundation;
using TrialApp.Common;
using TrialApp.ServiceClient;
using System;

namespace TrialApp.Services
{
    public class WebserviceTasks
    {
        public static string usernameWS { get; set; }
        public static string passwordWS { get; set; }
        public static string dbVersion = "1.1";
        
        public static string SyncCode { get; set; }
        public static string appVersion = "0.6.0.0";
        public static string domain = "INTRA";

        public static string Endpoint { get; set; }
        public static int FldNrPerRqst { get; set; }
        public static string token { get; set; }
        public static DateTime TokenExpiryDate { get; set; }

        /// <summary>
        /// Set default values 
        /// </summary>
        public static void SetDefaults(XNamespace ns)
        {
            var service = new SettingParametersService();

            var settingparams = service.GetParamsList();

            if (settingparams == null) return;
            if (string.IsNullOrEmpty(settingparams.Single().Endpoint))
                Endpoint = "https://bpmdev.enzazaden.com/cordys/com.eibus.web.soap.Gateway.wcp?";
            else
            {
                var endpoints = settingparams.Single().Endpoint.Split('|');
                if (endpoints.Length > 1)
                {
                    var name = XDocument.Load("AppxManifest.xml").Root?.Element(ns + "Properties")?.Element(ns + "DisplayName")?.Value;
                    
                    if (name == null) return;

                    if (name.Contains("Test"))
                        settingparams.Single().Endpoint = endpoints[0];
                    else if (name.Contains("Acc"))
                        settingparams.Single().Endpoint = endpoints[1];
                    else
                        settingparams.Single().Endpoint = endpoints[2];

                    Endpoint = settingparams.Single().Endpoint;

                    service.UpdateParams("endpoint", settingparams.Single().Endpoint);
                }
                else
                    Endpoint = settingparams.Single().Endpoint;

            }

        }

        public static SoapClient GetSoapClient()
        {
            return new SoapClient
            {
                EndPointAddress = Endpoint,
                Credentail = domain + "/" + usernameWS + ":" + passwordWS
            };
        }
        public static bool CheckTokenValidDate()
        {
            var timeDifference = TokenExpiryDate - DateTime.Now;
            if (timeDifference >= new TimeSpan(0, 10, 0))
                return true;
           else {  usernameWS = ""; return false; }
        }

    }


}