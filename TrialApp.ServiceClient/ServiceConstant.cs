﻿using System;
using System.Collections.Generic;

namespace TrialApp.ServiceClient
{
    public class ServiceConstant
    {
        public static Dictionary<string, string> NamespaceDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"GetTrialTokenBack", "http://schemas.cordys.com/TrialPrepWsApp"},
            {"getMetaInfoForMasterDataTables","http://contract.enzazaden.com/common/masterdatamanagement/v1" },
             {"getMasterData_V3","http://contract.enzazaden.com/common/masterdatamanagement/v1" },
             {"getTrialsWrapper","http://schemas.enzazaden.com/trailPrep/trials/1.0" },
             {"GetTrialEntriesData","http://schemas.enzazaden.com/trailPrep/trials/1.0" },
             {"CreateTrialEntry","http://schemas.enzazaden.com/trailPrep/trials/1.0" },
             {"SaveObservationData","http://schemas.enzazaden.com/trailPrep/trials/1.0" }

        };

        public static Dictionary<string, string> ServiceAction = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {
                "GetTrialTokenBack", @"{http://schemas.cordys.com/TrialPrepWsApp}TrialPrepWsAppWebServiceInterface"
            },
            {"getMetaInfoForMasterDataTables",@"{http://contract.enzazaden.com/common/masterdatamanagement/v1}MasterDataInterface" },
            {"getMasterData_V3",@"{http://contract.enzazaden.com/common/masterdatamanagement/v1}MasterDataInterface" },
            {"getTrialsWrapper",@"{http://schemas.enzazaden.com/trailPrep/trials/1.0}trialPrepTrialsServiceWrappersInterface" },
            {"GetTrialEntriesData",@"{http://schemas.enzazaden.com/trailPrep/trials/1.0}trialPrepTrialsServiceWrappersInterface" },
            {"CreateTrialEntry",@"{http://schemas.enzazaden.com/trailPrep/trials/1.0}trialPrepTrialsServiceWrappersInterface" },
            {"SaveObservationData",@"{http://schemas.enzazaden.com/trailPrep/trials/1.0}trialPrepTrialsServiceWrappersInterface" }
        };
    }
}
