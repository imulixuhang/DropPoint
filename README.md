# use the Azure Functions Core Tools to create a new local Azure Functions project in the current directory using the dotnet runtime:
- % cd dropPoint
- $ func init --worker-runtime dotnet

# Update the value of the AzureWebJobsStorage by setting it to the connection string of the storage account that you recorded earlier in this lab. Save the local.settings.json file.
- or download remote setting using ctrl+shift+P

# build
- $ dotnet build

# Create an HTTP-triggered function
- $ func new --template "HTTP trigger" --name "SpatialAnchorFunction"
- $ func new --template "HTTP trigger" --name "SceneryFunction"
    
# test on local
- $ func start --build

# create a new function named Recurring, using the Timer trigger template
- $ func new --template "Timer trigger" --name "Recurring"


# Deploy a local function project to an Azure Functions app
- $ az login
- $ func azure functionapp publish fllfunctionapp21-22
- or cmd+shift+p


# create a new App Service plan
- $ az functionapp plan create --name <NEW_PREMIUM_PLAN_NAME> --resource-group <MY_RESOURCE_GROUP> --location <REGION> --sku EP1
- $ az functionapp update --name <MY_APP_NAME> --resource-group <MY_RESOURCE_GROUP> --plan <NEW_PREMIUM_PLAN>
- $ az functionapp plan list --resource-group <MY_RESOURCE_GROUP> --query "[?sku.family=='Y'].{PlanName:name,Sites:numberOfSites}" -o table
- $ az functionapp plan delete --name <CONSUMPTION_PLAN_NAME> --resource-group <MY_RESOURCE_GROUP>

- $ az functionapp create --resource-group <MY_RESOURCE_GROUP> --name <NEW_CONSUMPTION_APP_NAME> --consumption-plan-location <REGION> --runtime dotnet --functions-version 3 --storage-account <STORAGE_NAME>
- $ az functionapp update --name <MY_APP_NAME> --resource-group <MY_RESOURCE_GROUP> --plan <NEW_CONSUMPTION_PLAN>
- $ az functionapp delete --name <NEW_CONSUMPTION_APP_NAME> --resource-group <MY_RESOURCE_GROUP>


# get a container instance
- // Setup the connection to the storage account
- CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
- // Connect to the blob storage
- CloudBlobClient serviceClient = storageAccount.CreateCloudBlobClient();
- // Connect to the blob container
- CloudBlobContainer container = serviceClient.GetContainerReference($"{containerName}");
- // Connect to the blob file
- CloudBlockBlob blob = container.GetBlockBlobReference($"{fileName}");
- blob.Properties.ContentType = "application/json";

# test
- on local, when you using func start --built command and open in web browser, it will GET
- on server, you need to choose one of the two functions and click "Code + Test" -> "Test/Run" -> POST or GET

# git commanda
git config --global user.name "Alex Li"
git config --global user.email "imulixuhang@gmail.com"