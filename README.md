# Tutorial and sample codes for Azure Cloud

## Overview

- General tutorial
   - https://azure.microsoft.com/en-us/get-started/#explore-azure
   - https://docs.microsoft.com/en-us/azure/architecture/
   - https://docs.microsoft.com/en-us/powershell/azure/install-az-ps?view=azps-6.6.0
- Azure load balancer is at load-balancer folder.
- Azure Kubernetes Service is at kubernetes-service folder.
- Azure Functions is at functions and functions-cosmosdb and functions-storage folder.
- Azure Event Hubs is at eventhubs folder.
- Azure Service Bus is at servicebus folder.

## General PowerShell Commands

### Install Azure Powershell module

- $PSVersionTable.PSVersion
- Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
- Install-Module -Name Az -Scope CurrentUser -Repository PSGallery -Force

### Sign in to Azure

- Connect-AzAccount
- Set-AzContext -SubscriptionId 00000000-0000-0000-0000-000000000000

### Create a resource group

- New-AzResourceGroup -Name azureTutorialResourceGroup -Location westus2
- Get-AzResourceGroup

### Remove a resource group

- Remove-AzResourceGroup -Name azureTutorialResourceGroup

## Installed SDKs and Tools for sample codes

- https://dotnet.microsoft.com/download (both 5.0 and Core 3.1)

## Useful Visual Studio Code extensions

- https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp

## Useful references

- https://www.strathweb.com/2019/04/roslyn-analyzers-in-code-fixes-in-omnisharp-and-vs-code/
