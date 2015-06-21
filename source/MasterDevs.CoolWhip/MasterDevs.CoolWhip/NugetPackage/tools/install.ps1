param($installPath, $toolsPath, $package, $project)
#https://github.com/jonnii/BuildDeploySupport/blob/master/package/tools/Init.ps1
#https://robdmoore.id.au/blog/2013/08/07/test-harness-for-nuget-install-powershell-scripts-init-ps1-install-ps1-uninstall-ps1/
# AKA https://github.com/robdmoore/NuGetCommandTestHarness

#TODOs -> Add Nuspec File

$packagePath = Join-Path $toolsPath ".."
$projectPath = Split-Path -Parent $project.FullName

$solutionFolder = Split-Path $dte.Solution.FullName
$appveyorPath = Join-Path $packagePath "content\appveyor.yml"
$appVeyorOutputPath = Join-Path $solutionFolder "appveyor.yml"
$appVeyorContent = Get-Content $appveyorPath
$fullProjectOutputPath = Join-Path $projectPath (Get-Project).ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value

#Replace Artifact Output Paths
$relativeProjectOutputPath = $fullProjectOutputPath.Replace($solutionFolder, "")
$appVeyorContent = $appVeyorContent.Replace("{{relateProjectOutputPath}}", $relativeProjectOutputPath)
$appVeyorContent = $appVeyorContent.Replace("{{artifactName}}", $project.Name)

#Replace AssemblyInfo path
$appVeyorContent = $appVeyorContent.Replace("{{assemblyInfoRelativePath}}", $projectPath.Replace($solutionFolder, ""))

#Write AppVeyor file (if it doesn't exist)
if(Test-Path $appVeyorOutputPath) 
{
	Write-Host "File Exists!" -ForegroundColor Red
}
else 
{
	Set-Content $appVeyorOutputPath $appVeyorContent
	Write-Host "Created " $appVeyorOutputPath -ForegroundColor DarkCyan
}
