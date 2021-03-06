# Tutorial and sample codes for Azure Kubernetes service

## Overview

- General concept
   - https://docs.docker.com/get-started/orchestration/
   - https://docs.microsoft.com/en-us/learn/paths/intro-to-kubernetes-on-azure/
   - https://docs.microsoft.com/en-us/azure/aks/intro-kubernetes
- Getting started guide
   - https://docs.microsoft.com/en-us/azure/aks/kubernetes-walkthrough-powershell

## PowerShell Commands

### Create AKS cluster

- ssh-keygen -m PEM -t rsa -b 4096
- New-AzAksCluster -ResourceGroupName azureTutorialResourceGroup -Name myAKSCluster -NodeCount 1

### Connect to the cluster

- Install-AzAksKubectl
- Import-AzAksCredential -ResourceGroupName azureTutorialResourceGroup -Name myAKSCluster
- .\kubectl get nodes

### Run the applicaion

- .\kubectl apply -f azure-vote.yaml
- .\kubectl get service azure-vote-front --watch

