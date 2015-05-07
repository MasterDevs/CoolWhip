$assemblyInfoFile = "./AssemblyInfo.cs"

$body = (Get-Content $assemblyInfoFile)

#-- Turn a string array into 1 big string (this way we can catch multi-line regular expressions just in case)
$body = [string]::Join("
", $body)

$body = $body -replace '\[assembly: AssemblyVersion', '

//These values will be replaced by the custom MS Build Task (Whippet) 
//Please see https://github.com/MasterDevs/CoolWhip for more info.

//$&'
$body = $body -replace '\[assembly: AssemblyFileVersion', '//$&'

Set-Content $assemblyInfoFile $body

echo "Commented out AssemblyVersion & AssemblyFileVersion"