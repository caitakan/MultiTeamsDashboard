Login-AzureRmAccount
    # Login as wusun
Get-AzureRmSubscription
    # Name     : DeepCrm
    # Id       : d97ab7c4-de41-42d6-bba0-d6a89f4b59e0
    # TenantId : 72f988bf-86f1-41af-91ab-2d7cd011db47
    # State    : Enabled

Set-AzureRmContext -SubscriptionId d97ab7c4-de41-42d6-bba0-d6a89f4b59e0

#Download from https://github.com/ChackDan/Service-Fabric
$psModulePath = "C:\Users\wusun\Desktop\Service-Fabric\Scripts\ServiceFabricRPHelpers\ServiceFabricRPHelpers.psm1"
Unblock-File -Path $psModulePath
Import-Module $psModulePath

$ResourceGroup = "BizDashboard"
$vaultName = "BizDashboardKeyVault"
$SubID = "d97ab7c4-de41-42d6-bba0-d6a89f4b59e0" #use the result from Get-AzureRmSubscription
$locationRegion = "southcentralus"
$newCertName = "dashboardservicefabric"
$dnsName = "dashboard-servicecluster.westus2.cloudapp.azure.com" #The certificate's subject name must match the domain used to access the Service Fabric cluster.
$localCertPath = "C:\MyCertificates" # location where you want the .PFX to be stored

 #Invoke-AddCertToKeyVault -SubscriptionId $SubID -ResourceGroupName $ResourceGroup -Location $locationRegion -VaultName $vaultName -CertificateName $newCertName -CreateSelfSignedCertificate -DnsName $dnsName -OutputPath $localCertPath

$cert_password = 'deepcrm#123'
Invoke-AddCertToKeyVault -SubscriptionId $SubID -ResourceGroupName $ResourceGroup -Location $locationRegion -VaultName $vaultName  -CertificateName $newCertName -Password $cert_password -CreateSelfSignedCertificate -DnsName $dnsName -OutputPath $localCertPath
                #Switching context to SubscriptionId d97ab7c4-de41-42d6-bba0-d6a89f4b59e0
                #Ensuring ResourceGroup BizDashboard in southcentralus
                #Using existing valut BizDashboardKeyVault in eastus
                #Creating new self signed certificate at C:\MyCertificates\dashboardservicefabric.pfx
                #Reading pfx file from C:\MyCertificates\dashboardservicefabric.pfx
                #Writing secret to dashboardservicefabric in vault BizDashboardKeyVault
                #
                #Name                           Value                                                                                                                                                                                                      
                #----                           -----                                                                                                                                                                                                      
                #CertificateThumbprint          6DF1B2CCEBD9F90D9E09C6CA272007481C29F3EB                                                                                                                                                                   
                #SourceVault                    /subscriptions/d97ab7c4-de41-42d6-bba0-d6a89f4b59e0/resourceGroups/BizDashboard/providers/Microsoft.KeyVault/vaults/BizDashboardKeyVault                                                                   
                #CertificateURL                 https://bizdashboardkeyvault.vault.azure.net:443/secrets/dashboardservicefabric/9136f8cbf37848b5a057c0bbf0972845   
