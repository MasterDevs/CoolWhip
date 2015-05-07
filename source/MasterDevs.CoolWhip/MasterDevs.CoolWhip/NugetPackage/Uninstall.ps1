$assemblyInfoFile = "./AssemblyInfo.cs"

$body = (Get-Content $assemblyInfoFile)

#-- Turn a string array into 1 big string (this way we can catch multi-line regular expressions just in case)
$body = [string]::Join("
", $body)

$body = $body -replace '//These values will be replaced by the custom MS Build Task \(Whippet\)', ''
$body = $body -replace '//Please see https://github.com/MasterDevs/CoolWhip for more info.', ''
$body = $body -replace '(//)\s*(\[assembly: AssemblyVersion)', '$2'
$body = $body -replace '(//)\s*(\[assembly: AssemblyFileVersion)', '$2'

Set-Content $assemblyInfoFile $body

echo "Commented back in AssemblyVersion & AssemblyFileVersion"
