# Tutorial and sample codes for Azure Event Grid

## Overview

This includes the tutorial and the sample codes by following the Azure getting started guide.

You need to run the PowerShell commands first to create the required Azure objects and to get the required information.

- General tutorial
   - https://docs.microsoft.com/en-us/azure/event-grid/compare-messaging-services
   - https://docs.microsoft.com/en-us/azure/event-grid/
- Getting started guide
   - https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blob-event-quickstart-powershell?toc=/azure/event-grid/toc.json
   - https://docs.microsoft.com/en-us/azure/event-grid/custom-event-quickstart-powershell   
- Other samples
   - https://docs.microsoft.com/en-us/samples/azure/azure-sdk-for-net/azure-event-grid-sdk-samples/

## PowerShell Commands

### Create a resource group

- $resourceGroup = "cftestresourcegroup"
- New-AzResourceGroup -Name $resourceGroup -Location westus2

### Create a message endpoint using pre-built web app

- $sitename="cftesteventgridviewer"
- New-AzResourceGroupDeployment `
  -ResourceGroupName $resourceGroup `
  -TemplateUri "https://raw.githubusercontent.com/Azure-Samples/azure-event-grid-viewer/master/azuredeploy.json" `
  -siteName $sitename `
  -hostingPlanName viewerhost
- Open our web app https://cftesteventgridviewer.azurewebsites.net to check.

### Enable Event Grid resource provider

- Register-AzResourceProvider -ProviderNamespace Microsoft.EventGrid
- Get-AzResourceProvider -ProviderNamespace Microsoft.EventGrid

### For system topic - storage account

#### Create a storage account

- $storageName = "cftestgridstorage"
- $storageAccount = New-AzStorageAccount -ResourceGroupName $resourceGroup `
  -Name $storageName `
  -Location westus2 `
  -SkuName Standard_LRS `
  -Kind BlobStorage `
  -AccessTier Hot
- $ctx = $storageAccount.Context

#### Subscribe to storage account

- $storageId = (Get-AzStorageAccount -ResourceGroupName $resourceGroup -AccountName $storageName).Id
- $endpoint="https://cftesteventgridviewer.azurewebsites.net/api/updates"
- New-AzEventGridSubscription `
  -EventSubscriptionName gridBlobQuickStart `
  -Endpoint $endpoint `
  -ResourceId $storageId
- Open our web app https://cftesteventgridviewer.azurewebsites.net to check.

#### Trigger an event from Blob storage by uploading an object into container

- $containerName = "gridcontainer"
- New-AzStorageContainer -Name $containerName -Context $ctx
- echo $null >> gridTestFile.txt
- Set-AzStorageBlobContent -File gridTestFile.txt -Container $containerName -Context $ctx -Blob gridTestFile.txt
- Open our web app https://cftesteventgridviewer.azurewebsites.net to check.

### For custom topic

#### Create a custom topic

- $topicname="cfcustomtopic"
- New-AzEventGridTopic -ResourceGroupName $resourceGroup -Location westus2 -Name $topicname

#### Subscribe to a topic

- $endpoint="https://cftesteventgridviewer.azurewebsites.net/api/updates"
- New-AzEventGridSubscription `
  -EventSubscriptionName gridTopicQuickStart `
  -Endpoint $endpoint `
  -ResourceGroupName $resourceGroup `
  -TopicName $topicname
- Open our web app https://cftesteventgridviewer.azurewebsites.net to check.

#### Send an event to the topic

- $endpoint = (Get-AzEventGridTopic -ResourceGroupName $resourceGroup -Name $topicname).Endpoint
- $keys = Get-AzEventGridTopicKey -ResourceGroupName $resourceGroup -Name $topicname
- Check https://docs.microsoft.com/en-us/azure/event-grid/custom-event-quickstart-powershell to create the $body data.
- Invoke-WebRequest -Uri $endpoint -Method POST -Body $body -Headers @{"aeg-sas-key" = $keys.Key1}