# Tutorial and sample codes for Azure Functions

## Overview

This is a basic Azure Function with HTTP trigger and connecting to Azure Cosmos DB, by following the Azure getting started guide.

This can be run locally and also can be deployed to Azure.

- General tutorial
   - https://docs.microsoft.com/en-us/azure/azure-functions/
   - https://docs.microsoft.com/en-us/azure/azure-functions/functions-reference?tabs=blob 
- Getting started guide
   - https://docs.microsoft.com/en-us/azure/azure-functions/functions-get-started?pivots=programming-language-csharp
   - https://docs.microsoft.com/en-us/azure/azure-functions/create-first-function-vs-code-csharp?tabs=isolated-process&pivots=programming-runtime-functions-v3
      - Use https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs-code?tabs=csharp#enable-publishing-with-advanced-create-options to publish
   - https://docs.microsoft.com/en-us/azure/azure-functions/functions-add-output-binding-cosmos-db-vs-code?pivots=programming-language-csharp&tabs=isolated-process

## Extra packages

- dotnet add package Microsoft.Azure.Functions.Worker.Extensions.CosmosDB

## Installed SDKs and Tools

- https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=v3%2Cwindows%2Ccsharp%2Cportal%2Cbash%2Ckeda#install-the-azure-functions-core-tools

## Visual Studio Code extensions

- https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azurefunctions
- https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-cosmosdb
