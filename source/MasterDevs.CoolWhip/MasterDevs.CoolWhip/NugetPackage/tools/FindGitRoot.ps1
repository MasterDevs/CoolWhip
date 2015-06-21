Function findGitRoot([string]$pathInGit){

	while(![string]::IsNullOrEmpty($pathInGit)) {

		#Check if .git folder exists
		if(Test-Path "$($pathInGit)\.git") {
			Write-Host $pathInGit -ForegroundColor DarkCyan
			Return $pathInGit
		}

		$pathInGit =  Split-Path -Path $pathInGit -Parent;
	}

	Return "Ooops"
}