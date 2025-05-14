@description('The name prefix for the APIM service')
param namePrefix string = 'aoai'

@description('The location for the API Management service')
param location string = resourceGroup().location

@description('The publisher email for the API Management service')
param publisherEmail string

@description('The publisher name for the API Management service')
param publisherName string

var apimName = '${namePrefix}-${location}-${uniqueString(resourceGroup().id,subscription().id)}'

resource apim 'Microsoft.ApiManagement/service@2023-03-01-preview' = {
  name: apimName
  location: location
  sku: {
    name: 'Consumption' // Serverless tier
    capacity: 0
  }
  properties: {
    publisherEmail: publisherEmail
    publisherName: publisherName
  }
}

output apimResourceId string = apim.id
