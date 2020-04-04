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
$aKeyTabConfig = New-Object -TypeName CoreRequest.KeyTabConfig($true, "svc_ledfreame", [CoreRequest.KeyTabConfig+KeyTabEncryption]::Aes128Aes256, $false, "", $false, "", "/tmp/ktexports/")
$aKeyTabConfig2 = New-Object -TypeName CoreRequest.KeyTabConfig($true, "svc_markerctl", [CoreRequest.KeyTabConfig+KeyTabEncryption]::Aes128Aes256, $false, "", $false, "", "/tmp/ktexports/")
$aKeyTabReq.AddConfigToCollection($aKeyTabConfig)
$aKeyTabReq.AddConfigToCollection($aKeyTabConfig2)
$aKeyTabReq.SaveTo("C:\Temp\Test\", "Test2.xml")

#Load a KeyTabRequest from a XML-File
Add-Type -Path "C:\Temp\CoreRequest.dll"
$aKeyTabReqRestored = [CoreRequest.KeyTabRequest]::Open("C:\Temp\Test\", "Test2.xml")
$aKeyTabReqRestored

