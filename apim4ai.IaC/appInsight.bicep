@description('The name prefix for the Application Insight service')
param namePrefix string = 'appinsight'

@description('The location for Application Insights')
param location string = resourceGroup().location

var appInsightsName = '${namePrefix}-${location}-${uniqueString(resourceGroup().id,subscription().id)}'

resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: appInsightsName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
  }
}

output appInsightsName string = appInsights.name
