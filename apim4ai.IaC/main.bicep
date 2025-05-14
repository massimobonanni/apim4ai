targetScope = 'subscription'

@description('The resource group name')
param resourceGroupName string= 'Apim4AI-rg'

@description('The location for the apim')
param location string = deployment().location

//create a resource group
resource resourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: resourceGroupName
  location: location
}

module aoaiServices 'aiServices.bicep' = {
  name: 'aoaiServices'
  scope: resourceGroup
  params: {
    namePrefix: 'aoai'
    skuName: 'S0'
  }
}