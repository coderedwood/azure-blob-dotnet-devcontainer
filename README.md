# azure_blob_dotnet_devcontainer
## Commands
```
az login
```
```
az group create --location eastus2 --name az204-storageblobs-dotnet
```
```
az storage account create --location eastus2 --name az204blobaccount24 --resource-group az204-storageblobs-dotnet --sku Standard_RAGRS
```
Retrieve the connection string from the portal store it somewhere 

### In the terminal create a new dotnet console application with the following command
```
dotnet new console -n az204-blob
```
### Change into the directory
### Build the application
```
dotnet build
```
### Create a data directory
```
mkdir data
```
### Add the nuget package Azure.Storage.Blobs with the command below
```
dotnet add package Azure.Storage.Blobs
```
### Update/Replace the contents of the Program.cs file with the one contained in this repo and replace the connection string placeholder with the one you copied earlier
### Build the application
```
dotnet build
```
### Run the application and follow the steps in the console
```
dotnet run
```
### Delete the resource group when finished to avoid any charges.

```
az group delete --name az204-storageblobs-dotnet --no-wait -y
```