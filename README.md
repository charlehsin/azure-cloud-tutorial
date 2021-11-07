# Tutorial for Azure Cloud

## Overview

- Azure load balancer is at load-balancer folder.
- Azure Kubernetes Service is at kubernetes-service folder.
- Azure Functions is at functions and functions-cosmosdb and functions-storage folder.

## PowerShell Commands

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

## Installed SDKs and Tools

- https://dotnet.microsoft.com/download (both 5.0 and Core 3.1)
- https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=v3%2Cwindows%2Ccsharp%2Cportal%2Cbash%2Ckeda#install-the-azure-functions-core-tools

## Visual Studio Code extensions

- https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp

## References

- https://azure.microsoft.com/en-us/get-started/#explore-azure
- https://docs.microsoft.com/en-us/azure/architecture/
- https://www.strathweb.com/2019/04/roslyn-analyzers-in-code-fixes-in-omnisharp-and-vs-code/
