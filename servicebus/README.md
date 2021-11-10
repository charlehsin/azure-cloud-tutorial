# Tutorial and sample codes for Azure Service Bus

## Overview

This includes the tutorial and the sample codes by following the Azure getting started guide.

You need to run the PowerShell commands first to create the required Azure objects and to get the required information.
Then you can modify the Program.cs codes with the correct information.

If you need to run topics and subscriptions sample, follow the link below to create the required topics and subscriptions first.

- General tutorial
   - https://docs.microsoft.com/en-us/azure/event-grid/compare-messaging-services
   - https://docs.microsoft.com/en-us/azure/service-bus-messaging/
- Getting started guide: basic
   - https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-quickstart-powershell
   - https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dotnet-get-started-with-queues
- Getting started guide: topics and subscriptions
   - https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-quickstart-topics-subscriptions-portal 
   - https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dotnet-how-to-use-topics-subscriptions
- Other samples
   - https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/servicebus/Azure.Messaging.ServiceBus/samples

## PowerShell Commands

### Create Service Bus messaging namespace

- New-AzResourceGroup -Name ContosoRG -Location westus2
- New-AzServiceBusNamespace -ResourceGroupName ContosoRG -Name CfTestContosoSBusNS -Location westus2

### Create a queue

- New-AzServiceBusQueue -ResourceGroupName ContosoRG -NamespaceName CfTestContosoSBusNS -Name ContosoOrdersQueue

### Get primary connection string

- Get-AzServiceBusKey -ResourceGroupName ContosoRG -Namespace CfTestContosoSBusNS -Name RootManageSharedAccessKey

## Extra packages

- dotnet add package Azure.Messaging.ServiceBus