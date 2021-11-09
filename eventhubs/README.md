# Tutorial for Azure Event Hubs

## Overview

This is the tutorial codes by following the Azure getting started guide.

You need to run the PowerShell commands first to create the required Azure objects and to get the required information.
Then you can modify the Program.cs codes with the correct information.

## PowerShell Commands

### Create Event Hub namespace

- New-AzEventHubNamespace -ResourceGroupName azureTutorialResourceGroup -NamespaceName cftutorialtest -Location westus2

### Create Event Hub

- New-AzEventHub -ResourceGroupName azureTutorialResourceGroup -NamespaceName cftutorialtest -EventHubName cftutorialtesteventhub -MessageRetentionInDays 3

### Get Event Hub connection string

- Get-AzEventHubKey -ResourceGroupName azureTutorialResourceGroup -NamespaceName cftutorialtest -AuthorizationRuleName RootManageSharedAccessKey

### Create a storage account

- New-AzStorageAccount -ResourceGroupName azureTutorialResourceGroup `
  -Name cftutorialteststorage `
  -Location westus2 `
  -SkuName Standard_RAGRS `
  -Kind StorageV2

### Create a blob container in storage account

- $storage = Get-AzStorageAccount -ResourceGroupName azureTutorialResourceGroup -Name cftutorialteststorage
- New-AzStorageContainer -Name cftutorialblob -Context $storage.Context -Permission blob

### Get Storage account connection string

- $storage = Get-AzStorageAccount -ResourceGroupName azureTutorialResourceGroup -Name cftutorialteststorage
- $saKey = (Get-AzStorageAccountKey -ResourceGroupName azureTutorialResourceGroup -Name cftutorialteststorage)[0].Value
- $connString = 'DefaultEndpointsProtocol=https;AccountName=cftutorialteststorage;AccountKey=' + $saKey + ';EndpointSuffix=core.windows.net'

## Extra packages

### For sender

- dotnet add package Azure.Messaging.EventHubs

### For receiver

- dotnet add package Azure.Messaging.EventHubs
- dotnet add package Azure.Messaging.EventHubs.Processor

## References

### General

- https://docs.microsoft.com/en-us/azure/event-grid/compare-messaging-services
- https://docs.microsoft.com/en-us/azure/event-hubs/

### Getting started

- https://docs.microsoft.com/en-us/azure/event-hubs/event-hubs-quickstart-powershell
- https://docs.microsoft.com/en-us/azure/event-hubs/event-hubs-get-connection-string#get-connection-string-from-the-portal 
- https://docs.microsoft.com/en-us/azure/event-hubs/event-hubs-dotnet-standard-getstarted-send
- https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/eventhub/Azure.Messaging.EventHubs/samples
- https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/eventhub/Azure.Messaging.EventHubs.Processor/samples