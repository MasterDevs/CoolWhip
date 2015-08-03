Function findGitRoot([string]$pathInGit)
{
	while(![string]::IsNullOrEmpty($pathInGit)) 
	{
		#Check if .git folder exists
		if(Test-Path "$($pathInGit)\.git") 
		{
			Return $pathInGit
		}

		$pathInGit =  Split-Path -Path $pathInGit -Parent;
	}

	Return ""
}

Function findProjectUrl()
{
	try
	{
		$origin = git config --get remote.origin.url
		if ($origin -like "*.git")
		{
			$projectUrl = $origin.TrimEnd(".git")
			return $projectUrl
		}
	}
	catch
	{
	}

	return ""
}

Function findProjectLicense([string]$gitRoot, [string] $projectUrl)
{
	if ($projectUrl -eq "") { return "" }

	$localLicensePath = $gitRoot + "\LICENSE"

	if (Test-Path $localLicensePath)
	{
		$projectLicense = $projectUrl + "/blob/master/LICENSE"

		return $projectLicense
	}
	else
	{
		$localLicensePath = "" 
	}
}