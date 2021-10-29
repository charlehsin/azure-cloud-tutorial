# Tutorial for Azure Cloud

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

## References

- https://azure.microsoft.com/en-us/get-started/#explore-azure
- https://docs.microsoft.com/en-us/azure/architecture/
