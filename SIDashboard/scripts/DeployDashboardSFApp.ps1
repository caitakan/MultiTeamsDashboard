Login-AzureRmAccount
# Login as wusun
Get-AzureRmSubscription
# Name     : DeepCrm
# Id       : d97ab7c4-de41-42d6-bba0-d6a89f4b59e0
# TenantId : 72f988bf-86f1-41af-91ab-2d7cd011db47
# State    : Enabled

Set-AzureRmContext -SubscriptionId d97ab7c4-de41-42d6-bba0-d6a89f4b59e0
$repoRootDir = (get-item . )
$relativeDir = $repoRootDir.FullName + "\SIDashboard\BizDashboardBackEnd";
$sfProjectPath = Join-Path $relativeDir "\BizDashboardBackEnd.sfproj"

#================================================================================================================================================
# Build ServiceManager SF win app and deploy to windows cluster 
#================================================================================================================================================

# Build a package
Write-Output "Building BizDashboardBackEnd SF application"
& msbuild  /t:Package /p:Configuration=Release $sfProjectPath


#================================================================================================================================================
# Deploy ServiceManger SF win app to windows cluster
#================================================================================================================================================
Import-Module "$ENV:ProgramFiles\Microsoft SDKs\Service Fabric\Tools\PSModule\ServiceFabricSDK\ServiceFabricSDK.psm1"
$path = $relativeDir + '\pkg\Release'


#================================================================================================================================================
# Connect to Windows Cluster
#================================================================================================================================================
$ClientCertThumbprint = '6DF1B2CCEBD9F90D9E09C6CA272007481C29F3EB'
$ServerCertThumbprint = '6DF1B2CCEBD9F90D9E09C6CA272007481C29F3EB'
Connect-ServiceFabricCluster -ConnectionEndpoint 'bizdashboardsfcluster.eastus.cloudapp.azure.com:19000' `
    -KeepAliveIntervalInSec 10 `
    -X509Credential `
    -ServerCertThumbprint $ServerCertThumbprint `
    -FindType FindByThumbprint `
    -FindValue $ClientCertThumbprint `
    -StoreLocation CurrentUser `
    -StoreName My

$appString = 'fabric:/BizDashboardBackEndType_Instance'
$appType = 'BizDashboardBackEndType'
$appVersion = '1.0.0'


#================================================================================================================================================
# For debugging purpose, if app is already registered. Need to remove it from SF first
#================================================================================================================================================
# Remove-ServiceFabricApplication $appString -Force
# Unregister-ServiceFabricApplicationType ServiceManagerFabricClientSFAppType $appVersion -Force
#================================================================================================================================================

# Forcefully remove existing Application Image. Otherwise the old image will be used
$appList = Get-ServiceFabricApplication -ApplicationName $appString    
$appNameMatch = $appList.ApplicationName -eq $appString
$appTypeMatch = $appList.ApplicationTypeName -eq $appType
$appVersionMath = $appList.ApplicationTypeVersion -eq $appVersion

$appAlreadyExist = ($appNameMatch -and $appTypeMatch -and $appVersionMath )
if ($appAlreadyExist) {
    Remove-ServiceFabricApplication -ApplicationName $appString -Force
}

# Unregister ServiceManager application type
$registerAppType = Get-ServiceFabricApplicationType -ApplicationTypeName $appType
$registered = ($registerAppType.ApplicationTypeName -eq $appType)
if ( $registered ) {
    Unregister-ServiceFabricApplicationType $appType $appVersion -Force
}

Remove-ServiceFabricApplicationPackage -ApplicationPackagePathInImageStore $appType -ImageStoreConnectionString (Get-ImageStoreConnectionStringFromClusterManifest(Get-ServiceFabricClusterManifest))

# Redeploy service manager application
Copy-ServiceFabricApplicationPackage -ApplicationPackagePath $path -ApplicationPackagePathInImageStore $appType -ImageStoreConnectionString (Get-ImageStoreConnectionStringFromClusterManifest(Get-ServiceFabricClusterManifest)) -TimeoutSec 1800
Register-ServiceFabricApplicationType $appType

$solutionArgs= @{}
$solutionArgs.SolutionName = 'SalesIntelligence'

New-ServiceFabricApplication `
    -ApplicationName $appString `
    -ApplicationTypeName $appType `
    -ApplicationTypeVersion $appVersion `
    -ApplicationParameter $solutionArgs


# $nodes = Get-ServiceFabricNode
# foreach($node in $nodes)
# {
#     $replicas = Get-ServiceFabricDeployedReplica -NodeName $node.NodeName -ApplicationName $appString
#     foreach ($replica in $replicas)
#     {
#         Remove-ServiceFabricReplica -ForceRemove -NodeName $node.NodeName -PartitionId $replica.Partitionid -ReplicaOrInstanceId $replica.ReplicaOrInstanceId
#     }
# }
