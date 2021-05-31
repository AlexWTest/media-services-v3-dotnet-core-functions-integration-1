//
// Azure Media Services REST API v3 Functions
//
// create-empty-asset
/*
This function creates an empty asset.


```c#
Input:
{
    "assetName" : "asset-dgdccfcffs", // optional. Will be automatically generated if not provided
    "prefixAssetName" : "movie-", // optional. "asset-" will be used if not provided and if assetName is not provided
    "baseStorageName" :"amsstore01" // optional. Name of attached storage where to create the asset. If azureRegion is specified, then the region is appended to the name
    "alternateId" : "data" //optional. Set data in alternate id
    "description": "my asset description" // optional
    "azureRegion": "euwe" or "we" or "euno" or "no" or "euwe,euno" or "we,no"
            // optional. If this value is set, then the AMS account name and resource group are appended with this value.
            // Resource name is not changed if "ResourceGroupFinalName" in app settings is to a value non empty.
            // This feature is useful if you want to manage several AMS account in different regions.
            // if two regions are sepecified using a comma as a separator, then the function will operate in the two regions at the same time. With this function, the live event will be deleted from the two regions.
            // Note: the service principal must work with all this accounts
}


Output:
{
    "success": true,
    "errorMessage" : "",
    "operationsVersion": "1.0.0.5",
    "assetName":"asset-fedtsdslkjdsd",
    "assetId" : "0ba50322-0a9e-4a91-8c28-0f87d60e3526"
    "containerPath" : [
    {"the url to the storage container of the asset"} 
    ],
    "container" : [
    {"the name of the storage container of the asset"} 
    ]

}

```
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LiveDrmOperationsV3.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace LiveDrmOperationsV3
{
    public static class CreateEmptyAsset
    {
        [FunctionName("create-empty-asset")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
            HttpRequest req, ILogger log, Microsoft.Azure.WebJobs.ExecutionContext execContext)
        {
            MediaServicesHelpers.LogInformation(log, "C# HTTP trigger function processed a request.");

            dynamic data;
            try
            {
                data = JsonConvert.DeserializeObject(new StreamReader(req.Body).ReadToEnd());
            }
            catch (Exception ex)
            {
                return IrdetoHelpers.ReturnErrorException(log, ex);
            }

            var prefixAssetName = (string)data.prefixAssetName ?? "asset-";
            var assetName = (string)data.assetName ?? prefixAssetName + Guid.NewGuid().ToString().Substring(0, 13);
            List<string> containers = new List<string>();
            List<string> containerPaths = new List<string>();
            string assetId = null;

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


            foreach (var region in azureRegions)
            {
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

                MediaServicesHelpers.LogInformation(log, "asset name : " + assetName, region);

                string storageName = (!string.IsNullOrWhiteSpace((string)data.baseStorageName)) ? (string)data.baseStorageName + config.AzureRegionCode : null;

                Asset asset = new Asset() { StorageAccountName = storageName, AlternateId = data.alternateId, Description = data.description };

                try
                {
                    asset = client.Assets.CreateOrUpdate(config.ResourceGroup, config.AccountName, assetName, asset);
                    asset = client.Assets.Get(config.ResourceGroup, config.AccountName, assetName);

                    // Access to container
                    ListContainerSasInput input = new ListContainerSasInput()
                    {
                        Permissions = AssetContainerPermission.Read,
                        ExpiryTime = DateTime.Now.AddMinutes(5).ToUniversalTime()
                    };

                    var responseListSas = await client.Assets.ListContainerSasAsync(config.ResourceGroup, config.AccountName, asset.Name, input.Permissions, input.ExpiryTime);
                    string uploadSasUrl = responseListSas.AssetContainerSasUrls.First();
                    var sasUri = new Uri(uploadSasUrl);
                    var container = new CloudBlobContainer(sasUri);

                    containerPaths.Add(container.Uri.ToString());
                    containers.Add(asset.Container);
                    assetId = asset.AssetId.ToString();
                }
                catch (Exception ex)
                {
                    return IrdetoHelpers.ReturnErrorException(log, ex);
                }
            }

            var response = new JObject
            {
                {"success", true},
                {"assetName",  assetName},
                {"assetId",  assetId},
                {"containerPath", new JArray(containerPaths)},
                {"container", new JArray(containers)},
                {
                    "operationsVersion",
                    AssemblyName.GetAssemblyName(Assembly.GetExecutingAssembly().Location).Version.ToString()
                }
            };

            return new OkObjectResult(
                response
            );
        }
    }
}