@description('The name prefix for the Azure OpenAI services')
param namePrefix string = 'aoai'

@description('The SKU for the Azure OpenAI service')
param skuName string = 'S0'

var regions = [
  'eastus'
  'francecentral'
  'westus'
  'swedencentral'
]

resource openAiServices 'Microsoft.CognitiveServices/accounts@2023-05-01' = [for region in regions: {
  name: '${namePrefix}-${region}-${uniqueString(resourceGroup().id,subscription().id)}'
  location: region
  kind: 'OpenAI'
  sku: {
    name: skuName
    tier: 'Standard'
  }
  properties: {
    apiProperties: {
      enableDynamicThrottling: true
    }
    networkAcls: {
      defaultAction: 'Allow'
    }
  }
}]

// Deploy GPT-4o model in each OpenAI service
resource gpt4oDeployments 'Microsoft.CognitiveServices/accounts/deployments@2023-05-01' = [for (region, i) in regions: {
  name: 'GPT-4o'
  parent: openAiServices[i]
  properties: {
    model: {
      format: 'OpenAI'
      name: 'gpt-4o'
      version: '2024-11-20'
    }
    versionUpgradeOption: 'OnceCurrentVersionExpired'
    raiPolicyName: 'Microsoft.Default'
  }
  sku: {
    name: 'Standard'
    capacity: 1
  }
}]

output deployedAOAIs array = [for (region, i) in regions: {
  name: openAiServices[i].name
  location: region
  resourceId: openAiServices[i].id
}]