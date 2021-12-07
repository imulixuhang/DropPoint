using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;

namespace BackendFunctions
{
    public static class SpatialAnchorFunction
    {

        [FunctionName("SpatialAnchor")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string connectionString = Environment.GetEnvironmentVariable("AzureStorageAccountConnectionString");
            string containerName = Environment.GetEnvironmentVariable("AzureStorageAccountContainerName");
            string fileName = Environment.GetEnvironmentVariable("CurrentAnchorIdBlobName");

            // Setup the connection to the storage account
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            // Connect to the blob storage
            CloudBlobClient serviceClient = storageAccount.CreateCloudBlobClient();
            // Connect to the blob container
            CloudBlobContainer container = serviceClient.GetContainerReference($"{containerName}");
            // Connect to the blob file
            CloudBlockBlob blob = container.GetBlockBlobReference($"{fileName}");
            blob.Properties.ContentType = "application/json";
            if (req.Method == "POST")
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                await blob.UploadTextAsync(requestBody);
                return (ActionResult)new OkObjectResult("");
            }

            // otherwise, drop through to a default "GET" behaviour
            return (ActionResult)new OkObjectResult(await blob.DownloadTextAsync());
            

        }
    }
}
