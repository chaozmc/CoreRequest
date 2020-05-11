#Generate a DNSRequest and save it
Add-Type -Path "C:\Temp\CoreRequest.dll"
$aDnsReq = New-Object -TypeName CoreRequest.DNSRequest
$aRec = New-Object -TypeName CoreRequest.DNSRecord("ADD", "A", "test1.home.local", "127.0.0.1", "home.local")
$aRec2 = New-Object -TypeName CoreRequest.DNSRecord ("ADD", "A", "test2.home.local", "127.0.0.1", "home.local")
$aDnsReq.AddRecordToCollection($aRec)
$aDnsReq.AddRecordToCollection($aRec2)
$aDnsReq.RequestReference = "Test1"
$aDnsReq.SaveTo("C:\Temp\Test")

#Load a DNSRequest from a XML-File
Add-Type -Path "C:\Temp\CoreRequest.dll"
$aDnsReqRestored = [CoreRequest.DNSRequest]::Open("C:\Temp\Test\", "TEST1.xml")
$aDnsReqRestored

#Generate a KeyTabRequest and save it
Add-Type -Path "C:\Temp\CoreRequest.dll"
$aKeyTabReq = New-Object -TypeName CoreRequest.KeyTabRequest
#Keytabrequest with unknown password
$aKeyTabConfigWKP = New-Object -TypeName CoreRequest.KeyTabConfig($true, 'svc_ledframe', [CoreRequest.KeyTabConfig+KeyTabEncryption]::Aes128Aes256)
#KeyTabRequest with known password
$aKeyTabConfigWOKP = New-Object -TypeName CoreRequest.KeyTabConfig($true, 'svc_ledframe', [CoreRequest.KeyTabConfig+KeyTabEncryption]::Aes128Aes256, $true)
#KeyTabRequest with unknown password and a SPN
$aKeyTabConfigWithSPN = New-Object -TypeName CoreRequest.KeyTabConfig($true, 'svc_ledframe', [CoreRequest.KeyTabConfig+KeyTabEncryption]::Aes128Aes256, $false, $true, 'HTTP/ledframe.is-jo.org')
#KeyTabRequest with known password and an UPN but without SPN
$aKeyTabConfigWithUPN = New-Object -TypeName CoreRequest.KeyTabConfig($true, 'svc_ledframe', [CoreRequest.KeyTabConfig+KeyTabEncryption]::Aes128Aes256, $false, $false, $null, $true, 'HTTP/ledframe.is-jo.org@HOME.LOCAL')

$aKeyTabReq.AddConfigToCollection($aKeyTabConfigWKP)
$aKeyTabReq.AddConfigToCollection($aKeyTabConfigWOKP)
$aKeyTabReq.AddConfigToCollection($aKeyTabConfigWithUPN)
$aKeyTabReq.AddConfigToCollection($aKeyTabConfigWithSPN)
$aKeyTabReq.SaveTo("C:\Temp", "KTRequest.xml")

#Load a KeyTabRequest from a XML-File
Add-Type -Path "C:\Temp\CoreRequest.dll"
$aKeyTabReqRestored = [CoreRequest.KeyTabRequest]::Open("C:\Temp\", "KTRequest.xml")
$aKeyTabReqRestored

