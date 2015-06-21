#https://github.com/jonnii/BuildDeploySupport/blob/master/package/tools/Init.ps1
#https://robdmoore.id.au/blog/2013/08/07/test-harness-for-nuget-install-powershell-scripts-init-ps1-install-ps1-uninstall-ps1/
# AKA https://github.com/robdmoore/NuGetCommandTestHarness


param($installPath, $toolsPath, $package, $project)

#Bring in some helpers
. "$($toolsPath)\FindGitRoot.ps1"


#TODOs -> Nuspec File



$packagePath = Join-Path $toolsPath ".."
$projectPath = Split-Path -Parent $project.FullName

Write-Host $project.Name -ForegroundColor DarkYellow


$solutionFolder = Split-Path $dte.Solution.FullName
$gitRoot = findGitRoot -pathInGit $solutionFolder
$appVeyorOutputPath = Join-Path $gitRoot "appveyor.yml"
$appVeyorTemplatePath = Join-Path $packagePath "content\appveyor.yml"
$appVeyorContent = Get-Content $appVeyorTemplatePath
$fullProjectOutputPath = Join-Path $projectPath (Get-Project).ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value


#Replace Artificat Output Paths
$relativeProjectOutputPath = $fullProjectOutputPath.Replace($solutionFolder, "")
$appVeyorContent = $appVeyorContent.Replace("{{relateProjectOutputPath}}", $relativeProjectOutputPath)
$appVeyorContent = $appVeyorContent.Replace("{{artifactName}}", $project.Name)

#Replace AssemblyInfo path
$appVeyorContent = $appVeyorContent.Replace("{{assemblyInfoRelativePath}}", $projectPath.Replace($solutionFolder, ""))

#Write AppVeyor file (if it doesn't exist)
if(Test-Path $appVeyorOutputPath) {
	Write-Host "Not overwriting File Exists!" -ForegroundColor Red
	Write-Error "Could not overwrite file"
}
else {
	Set-Content $appVeyorOutputPath $appVeyorContent
	Write-Host "Created " $appVeyorOutputPath -ForegroundColor DarkCyan
}
