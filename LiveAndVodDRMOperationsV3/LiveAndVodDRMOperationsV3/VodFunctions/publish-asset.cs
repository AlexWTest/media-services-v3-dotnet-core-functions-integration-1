//
// Azure Media Services REST API v3 Functions
//
// publish-asset-with-drm -  This function publish an asset for dynamic encryption (to be used with Irdeto)
//
/*
```c#
Input :
{
    "assetName": "asset-hgdhfg56",
    "defaultIrdetoContentId": "movie1", // optional
    "azureRegion": "euwe" or "we" or "euno" or "no" or "euwe,euno" or "we,no"
            // optional. If this value is set, then the AMS account name and resource group are appended with this value.
            // Resource name is not changed if "ResourceGroupFinalName" in app settings is to a value non empty.
            // This feature is useful if you want to manage several AMS account in different regions.
            // if two regions are sepecified using a comma as a separator, then the function will operate in the two regions at the same time. With this function, two live events will be created.
            // Note: the service principal must work with all this accounts
    "useDRM" : true, // optional. Default is true. Specify false if you don't want to use dynamic encryption
    "metadata" : "<json structure>", // optional metadata to put in entry in Cosmos
    "semaphore" : "<json structure>" // optional. Semaphore file with date like clearStream :true/false, publish date/time and drmContentId
}


Output:
{
  "success": true,
  "operationsVersion": "1.0.0.5",
  "assets": [
  {
    "assetName": "VOD-6918f509-229c",
    "assetStorageAccountName": "voddeveuwe",
    "streamingLocators": [
        {
            "streamingLocatorName": "locator-186d097e-d826",
            "streamingPolicyName": "policy-69e284dc-5b0e",
            "cencKeyId": "c399ec91-3171-474c-ae84-57be0e161686",
            "cbcsKeyId": "4f598865-1660-48bd-a55f-d06a75a13a07",
            "drm": [
                {
                    "type": "FairPlay",
                    "licenseUrl": "skd://rng.stage.ott.irdeto.com/licenseServer/streaming/v1/CUSTOMER/getckc?ContentId=CH1&KeyId=4f598865-1660-48bd-a55f-d06a75a13a07",
                    "certificateUrl": "https://rng.stage.ott.irdeto.com/licenseServer/streaming/v1/CUSTOMER/getcertificate?applicationId=stage",
                    "protocols": [
                        "HlsCmaf",
                        "HlsTs"
                    ]
                },
                {
                    "type": "PlayReady",
                    "licenseUrl": "https://rng.stage.ott.irdeto.com/licenseServer/playready/v1/CUSTOMER/license?contentId=VOD",
                    "protocols": [
                        "DashCmaf",
                        "DashCsf"
                    ]
                },
                {
                    "type": "Widevine",
                    "licenseUrl": "https://rng.stage.ott.irdeto.com/licenseServer/widevine/v1/CUSTOMER/license?contentId=VOD",
                    "protocols": [
                        "DashCmaf",
                        "DashCsf"
                    ]
                }
            ],
            "urls": [
                {
                    "protocol": "SmoothStreaming",
                    "url": "https://voddeveuwe-euwe.streaming.media.azure.net/186d097e-d826-4898-a4e8-692f796ceff4/RTSVOD-6918f509-229c.ism/manifest(encryption=cenc)"
                },
                {
                    "protocol": "DashCsf",
                    "url": "https://voddeveuwe-euwe.streaming.media.azure.net/186d097e-d826-4898-a4e8-692f796ceff4/RTSVOD-6918f509-229c.ism/manifest(format=mpd-time-csf,encryption=cenc)"
                },
                {
                    "protocol": "DashCmaf",
                    "url": "https://voddeveuwe-euwe.streaming.media.azure.net/186d097e-d826-4898-a4e8-692f796ceff4/RTSVOD-6918f509-229c.ism/manifest(format=mpd-time-cmaf,encryption=cenc)"
                },
                {
                    "protocol": "HlsCmaf",
                    "url": "https://voddeveuwe-euwe.streaming.media.azure.net/186d097e-d826-4898-a4e8-692f796ceff4/RTSVOD-6918f509-229c.ism/manifest(format=m3u8-cmaf,encryption=cbcs-aapl)"
                },
                {
                    "protocol": "HlsTs",
                    "url": "https://voddeveuwe-euwe.streaming.media.azure.net/186d097e-d826-4898-a4e8-692f796ceff4/RTSVOD-6918f509-229c.ism/manifest(format=m3u8-aapl,encryption=cbcs-aapl)"
                }
            ]
        }
    ],
    "amsAccountName": "voddeveuwe",
    "region": "West Europe",
    "resourceGroup": "DISTVOD-dev-euwe",
    "urn": "urn:rts:vod:testwilder:HD",
    "createdTime": "20190326T15:31:52Z",
    "semaphore": {
        "encodedAsset": true,
        "startTime": "20190213T10:00:00Z",
        "endTime": "20200218T13:00:00Z",
        "urn": "urn:rts:vod:testwilder:HD",
        "drmContentId": "VOD",
        "clearStream": false,
        "files": [
            {
                "fileName": "625290641000.mp4",
                "containsVideo": true,
                "containsAudio": false,
                "copyToSubAsset": false
            },
            {
                "fileName": "625290643000.mp4",
                "containsVideo": true,
                "containsAudio": false,
                "copyToSubAsset": false
            },
            {
                "fileName": "625290705000.mp4",
                "containsVideo": true,
                "containsAudio": true,
                "audioLanguage": "ita",
                "audioTitle ": "Italian",
                "copyToSubAsset": false
            },
            {
                "fileName": "625290707000.mp4",
                "containsVideo": true,
                "containsAudio": true,
                "audioLanguage": "fra",
                "audioTitle ": "Francais",
                "copyToSubAsset": false
            },
            {
                "fileName": "625290709000.mp4",
                "containsVideo": true,
                "containsAudio": true,
                "audioLanguage": "fra",
                "audioTitle ": "Francais",
                "copyToSubAsset": false
            },
            {
                "fileName": "625290711000.mp4",
                "containsVideo": true,
                "containsAudio": true,
                "audioLanguage": "ita",
                "audioTitle ": "Italian",
                "copyToSubAsset": false
            },
            {
                "fileName": "625290639000.mp4",
                "containsVideo": true,
                "containsAudio": true,
                "audioLanguage": "fra",
                "audioTitle ": "Francais",
                "copyToSubAsset": false
            },
            {
                "fileName": "gsw_CPL.mp4",
                "containsVideo": false,
                "containsAudio": true,
                "audioLanguage": "deu",
                "audioTitle ": "German",
                "copyToSubAsset": false
            },
            {
                "fileName": "W2_ZT036555FR.vtt",
                "containsVideo": false,
                "containsAudio": false,
                "containsText": true,
                "language": "fra",
                "textTitle": "French subs",
                "copyToSubAsset": true
            },
            {
                "fileName": "W2_ZT036555FRDE.vtt",
                "containsVideo": false,
                "containsAudio": false,
                "containsText": true,
                "language": "deu",
                "textTitle": "Deutsch subs",
                "copyToSubAsset": true
            }
        ]
    }
  ]
}
```
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiveDrmOperationsV3.Helpers;
using LiveDrmOperationsV3.Models;
using LiveDRMOperationsV3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LiveDrmOperationsV3
{
    public static class PublishAsset
    {
        // This version registers keys in irdeto backend. For FairPlay and rpv3

        [FunctionName("publish-asset")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            dynamic data;
            try
            {
                data = JsonConvert.DeserializeObject(new StreamReader(req.Body).ReadToEnd());
            }
            catch (Exception ex)
            {
                return IrdetoHelpers.ReturnErrorException(log, ex);
            }


            var assetName = (string)data.assetName;
            if (assetName == null)
                return IrdetoHelpers.ReturnErrorException(log, "Error - please pass assetName in the JSON");


            // Azure region management
            var azureRegions = new List<string>();
            if ((string)data.azureRegion != null)
            {
                azureRegions = ((string)data.azureRegion).Split(',').ToList();
            }
            else
            {
                azureRegions.Add((string)null);
            }

            // semaphore file (json structure)
            VodSemaphore semaphore = null;
            if (data.semaphore != null)
            {
                semaphore = VodSemaphore.FromJson((string)data.semaphore);
            }


            // init default
            var streamingLocatorGuid = Guid.NewGuid(); // same locator for the two ouputs if 2 live event namle created
            var uniquenessLocator = streamingLocatorGuid.ToString().Substring(0, 13);
            var streamingLocatorName = "locator-" + uniquenessLocator;

            string uniquenessPolicyName = Guid.NewGuid().ToString().Substring(0, 13);

            // useDRM init
            var useDRM = true;
            if (data.useDRM != null)
            {
                useDRM = (bool)data.useDRM;
            }
            else if (semaphore != null & semaphore.ClearStream != null)
            {
                useDRM = !(bool)semaphore.ClearStream;
            }


            // Default content id and semaphare value
            string irdetoContentId = null;
            if (semaphore != null && semaphore.DrmContentId != null) // semaphore data has higher priority 
            {
                irdetoContentId = semaphore.DrmContentId;
            }
            else if (data.defaultIrdetoContentId != null)
            {
                irdetoContentId = (string)data.defaultIrdetoContentId;
            }

            DateTime? startTime = null;
            DateTime? endTime = null;
            try
            {
                if (semaphore != null && semaphore.StartTime != null)
                {
                    startTime = DateTime.ParseExact(semaphore.StartTime, AssetEntry.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                }
                if (semaphore != null && semaphore.EndTime != null)
                {
                    endTime = DateTime.ParseExact(semaphore.EndTime, AssetEntry.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                return IrdetoHelpers.ReturnErrorException(log, ex);
            }


            var cencKey = new StreamingLocatorContentKey();
            var cbcsKey = new StreamingLocatorContentKey();

            if (useDRM)
            {
                try
                {
                    ConfigWrapper config = new ConfigWrapper(new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddEnvironmentVariables()
                            .Build(),
                            null);

                    MediaServicesHelpers.LogInformation(log, "Irdeto call...");

                    cencKey = await IrdetoHelpers.GenerateAndRegisterCENCKeyToIrdeto(irdetoContentId, config);
                    cbcsKey = await IrdetoHelpers.GenerateAndRegisterCBCSKeyToIrdeto(irdetoContentId, config);

                    MediaServicesHelpers.LogInformation(log, "Irdeto call done.");
                }
                catch (Exception ex)
                {
                    return IrdetoHelpers.ReturnErrorException(log, ex, "Irdeto response error");
                }
            }

            var clientTasks = new List<Task<AssetEntry>>();

            foreach (var region in azureRegions)
            {
                var task = Task<AssetEntry>.Run(async () =>
                {
                    Asset asset = null;
                    StreamingPolicy streamingPolicy = null;

                    ConfigWrapper config = new ConfigWrapper(new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddEnvironmentVariables()
                            .Build(),
                            region
                    );

                    MediaServicesHelpers.LogInformation(log, "config loaded.", region);
                    MediaServicesHelpers.LogInformation(log, "connecting to AMS account : " + config.AccountName, region);

                    var client = await MediaServicesHelpers.CreateMediaServicesClientAsync(config);
                    // Set the polling interval for long running operations to 2 seconds.
                    // The default value is 30 seconds for the .NET client SDK
                    client.LongRunningOperationRetryTimeout = 2;

                    if (useDRM)
                    {

                        MediaServicesHelpers.LogInformation(log, "Trying to read streaming policy from Cosmos.", region);
                        string streamingPolicyName = null;
                        // Load streaming policy info from Cosmos
                        try
                        {
                            var info = await CosmosHelpers.ReadStreamingPolicyDocument(new StreamingPolicyInfo(true)
                            {
                                AMSAccountName = config.AccountName
                            });

                            if (info == null)
                            {
                                log.LogWarning("Streaming policy not read from Cosmos.");
                            }
                            else
                            {
                                streamingPolicyName = info.StreamingPolicyName;
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error reading Cosmos DB", ex);
                        }


                        // STREAMING POLICY CREATION
                        if (streamingPolicyName == null) // not found in Cosmos let's create a new one
                        {
                            MediaServicesHelpers.LogInformation(log, "Creating streaming policy.", region);
                            try
                            {
                                streamingPolicy = await IrdetoHelpers.CreateStreamingPolicyIrdeto(config, client, uniquenessPolicyName);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Streaming policy creation error", ex);
                            }

                            try
                            {
                                if (!await CosmosHelpers.CreateOrUpdatePolicyDocument(new StreamingPolicyInfo(true)
                                {
                                    AMSAccountName = config.AccountName,
                                    StreamingPolicyName = streamingPolicy.Name
                                }))
                                {
                                    log.LogWarning("Cosmos access not configured or error.");
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Streaming policy write error to Cosmos", ex);
                            }
                        }
                        else
                        {
                            MediaServicesHelpers.LogInformation(log, "Getting streaming policy in AMS.", region);
                            try
                            {
                                streamingPolicy = await client.StreamingPolicies.GetAsync(config.ResourceGroup, config.AccountName, streamingPolicyName);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Error when getting streaming policy " + streamingPolicy, ex);
                            }
                        }
                    }


                    // let's get the asset
                    try
                    {
                        MediaServicesHelpers.LogInformation(log, "Getting asset.", region);
                        asset = await client.Assets.GetAsync(config.ResourceGroup, config.AccountName, assetName);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error when retreving asset by name", ex);
                    }


                    // Locator creation
                    try
                    {
                        StreamingLocator locator = null;
                        if (useDRM)
                        {
                            locator = await IrdetoHelpers.SetupDRMAndCreateLocatorWithNewKeys(config, streamingPolicy.Name,
                                streamingLocatorName, client, asset, cencKey, cbcsKey, streamingLocatorGuid, irdetoContentId, startTime, endTime);
                        }
                        else // no DRM
                        {
                            locator = await IrdetoHelpers.CreateClearLocator(config, streamingLocatorName, client, asset, streamingLocatorGuid, startTime, endTime);
                        }

                        MediaServicesHelpers.LogInformation(log, "locator : " + locator.Name, region);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error when creating the locator", ex);
                    }


                    // let's build info for the live event and output

                    AssetEntry assetEntry = await GenerateInfoHelpers.GenerateAssetInformation(config, client, asset, semaphore, irdetoContentId, region);

                    if (!await CosmosHelpers.CreateOrUpdateAssetDocument(assetEntry))
                        log.LogWarning("Cosmos access not configured.");

                    return assetEntry;

                });
                clientTasks.Add(task);
            }

            try
            {
                Task.WaitAll(clientTasks.ToArray());
            }
            catch (Exception ex)
            {
                return IrdetoHelpers.ReturnErrorException(log, ex);
            }

            return new OkObjectResult(
                                        JsonConvert.SerializeObject(new VodAssetInfo { Success = true, Assets = clientTasks.Select(i => i.Result).ToList() }, Formatting.Indented)
                                    );
        }
    }
}