@description('The name prefix for the APIM service')
param namePrefix string = 'aoai'

@description('The location for the API Management service')
param location string = resourceGroup().location

@description('The publisher email for the API Management service')
param publisherEmail string

@description('The publisher name for the API Management service')
param publisherName string

@description('The Application Insight name')
param appInsightName string 

var apimName = '${namePrefix}-${location}-${uniqueString(resourceGroup().id,subscription().id)}'

// Reference the Application Insights resource
resource appInsights 'Microsoft.Insights/components@2020-02-02' existing = {
  name: appInsightName
}

// Create APIM service
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

// Create products in APIM
var products = [
  {
    name: 'starter'
    displayName: 'Starter'
    description: 'Starter product with limited quota'
    terms: 'Terms of use for the starter product.'
    subscriptionRequired: true
    approvalRequired: false
    subscriptionsLimit: 1
    state: 'published'
  }
  {
    name: 'unlimited'
    displayName: 'Unlimited'
    description: 'Unlimited product with no quota'
    terms: 'Terms of use for the unlimited product.'
    subscriptionRequired: true
    approvalRequired: false
    subscriptionsLimit: 100
    state: 'published'
  }
]

resource apimProducts 'Microsoft.ApiManagement/service/products@2021-08-01' = [for product in products: {
  parent: apim
  name: product.name
  properties: {
    displayName: product.displayName
    description: product.description
    terms: product.terms
    subscriptionRequired: product.subscriptionRequired
    approvalRequired: product.approvalRequired
    subscriptionsLimit: product.subscriptionsLimit
    state: product.state
  }
}]

output apimResourceId string = apim.id
